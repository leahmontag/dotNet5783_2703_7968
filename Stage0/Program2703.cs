partial class Program
{
    private static void Main(string[] args)
    {
        welcome2703();
        welcome7968();
        Console.ReadKey();
    }

    static partial void welcome7968();
    private static void welcome2703()
    {
        Console.Write("Enter your name: ");
        string userName;
        userName = Console.ReadLine();
        Console.Write("{0},welcome to my first console application", userName);
    }

}