using PL.Products;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;


namespace PL.Simulator
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        public bool IDOrder

        {
            get { return (bool)GetValue(IDOrderProperty); }
            set { SetValue(IDOrderProperty, value); }
        }
        public static readonly DependencyProperty IDOrderProperty =
           DependencyProperty.Register(nameof(IDOrder), typeof(int), typeof(SimulatorWindow));

        BackgroundWorker worker;
        public SimulatorWindow()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync(); //(12) מהו ערך ברירת מחדל הזה?

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            // BackgroundWorker worker = sender as BackgroundWorker;
            int length = (int)e.Argument;
            for (int i = 1; i <= length; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    e.Result = stopwatch.ElapsedMilliseconds; // Unnecessary
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(500);
                    worker.ReportProgress(i * 100 / length);
                }
            }
            e.Result = stopwatch.ElapsedMilliseconds;
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            //resultLabel.Content = (progress + "%");
            //resultProgressBar.Value = progress;
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (e.Cancelled == true)
            //{
            //    // e.Result throw System.InvalidOperationException
            //    resultLabel.Content = "Canceled!";
            //}
            //else if (e.Error != null)
            //{
            //    // e.Result throw System.Reflection.TargetInvocationException
            //    resultLabel.Content = "Error: " + e.Error.Message; //Exception Message
            //}
            //else
            //{
            //    long result = (long)e.Result;
            //    if (result < 1000)
            //        resultLabel.Content = "Done after " + result + " ms.";
            //    else
            //        resultLabel.Content = "Done after " + result / 1000 + " sec.";
            //}
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (worker.WorkerSupportsCancellation == true)
                // Cancel the asynchronous operation.
                worker.CancelAsync();
        }


    }

}
