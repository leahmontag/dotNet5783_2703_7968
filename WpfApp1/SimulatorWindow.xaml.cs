﻿using Simulator;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        string nextStatus;
        string previousStatus;
        BackgroundWorker worker;
        Tuple<BO.Order, int, string, string> dcT;
        //====== disable the option of closing the window =======
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        //=====================================================
        private Stopwatch stopWatch;
        private bool isTimerRun;
        //=======progressBar variables
        Duration duration;
        DoubleAnimation doubleanimation;
        ProgressBar ProgressBar;
        //=======countdown timer variables
        DispatcherTimer _timer;
        TimeSpan _time;
        //=======
        public SimulatorWindow(BlApi.IBl? Bl)
        {
            InitializeComponent();
            bl = Bl;
            Loaded += ToolWindow_Loaded;
            TimerStart();
        }

        void countDownTimer(int sec)
        {
            _time = TimeSpan.FromSeconds(sec);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }
        void ProgressBarStart(int sec)
        {
            if (ProgressBar != null)
            {
                pBar.Items.Remove(ProgressBar);
            }
            ProgressBar = new ProgressBar();
            ProgressBar.IsIndeterminate = false;
            ProgressBar.Orientation = Orientation.Horizontal;
            ProgressBar.Width = 500;
            ProgressBar.Height = 60;
            duration = new Duration(TimeSpan.FromSeconds(sec * 2));
            doubleanimation = new DoubleAnimation(200.0, duration);
            ProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
            pBar.Items.Add(ProgressBar);
        }
        void TimerStart()
        {
            stopWatch = new Stopwatch();
            worker = new BackgroundWorker();
            worker.DoWork += TimerDoWork;
            worker.ProgressChanged += TimerProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            stopWatch.Restart();
            isTimerRun = true;
            worker.RunWorkerAsync();
        }
        void TimerDoWork(object sender, DoWorkEventArgs e)
        {
            Simulator.Simulator.ProgressChange += changeOrder;
            Simulator.Simulator.StopSimulator += Stop;
            Simulator.Simulator.run();
            while (isTimerRun)
            {
                worker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }
        private void changeOrder(object sender, EventArgs e)
        {
            if (!(e is DetailsSimulator))
                return;

            DetailsSimulator details = e as DetailsSimulator;
            previousStatus = (details.order.ShipDate == null) ? BO.Enums.OrderStatus.confirmed.ToString() : BO.Enums.OrderStatus.send.ToString();
            nextStatus = (details.order.ShipDate == null) ? BO.Enums.OrderStatus.send.ToString() : BO.Enums.OrderStatus.provided.ToString();
            dcT = new Tuple<BO.Order, int, string, string>(details.order, details.seconds / 1000, previousStatus, nextStatus);
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(changeOrder, sender, e);
            }
            else
            {
                countDownTimer(details.seconds / 1000);
                DataContext = dcT;
                ProgressBarStart(details.seconds / 1000);
            }
        }
        void TimerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            SimulatorTXTB.Text = timerText;
        }
        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void StopSimulatorBTN_Click(object sender, RoutedEventArgs e)
        {
            if (isTimerRun)
            {
                stopWatch.Stop();
                isTimerRun = false;
            }
            Simulator.Simulator.DoStop();
            this.Close();
        }
        public void Stop(object sender, EventArgs e)
        {
            Simulator.Simulator.ProgressChange -= changeOrder;
            Simulator.Simulator.StopSimulator -= Stop;
            if (!CheckAccess())
            {
                Dispatcher.BeginInvoke(Stop, sender, e);
            }
            else
            {
                MessageBox.Show("simulator is stopped");
                this.Close();
            }
        }

    }
}
