using System;
using System.Text;
using static FinalsOutput.UI;

namespace FinalsOutput
{
  
    class Program
    {
        const string USERNAME = "admin";
        const string PASSWORD = "admpass";
        const int MAX_ATTEMPTS = 3;

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "FINALS OUTPUT - C# System";

            if (!Login())
  
            {
                return; // Tatlong Attempt lang to sah
            }

            Loading("Loading system...");


            MainMenu();

            Console.Clear();
            PrintBanner("GOODBYE!", ConsoleColor.Magenta); //exit to sah
            Console.WriteLine("   Thank you for using the system.");
            Beep(800, 150); Beep(600, 150); Beep(400, 250);
        }

        static bool Login()
        {
            for (int attempt = 1; attempt <= MAX_ATTEMPTS; attempt++)
            {
                Console.Clear();
                int top = Console.CursorTop;      // row ng box to sah, para maitapat ng maayos cursor
                DrawLoginBox(attempt);

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(16, top + 5);   // Username to sah
                string user = Console.ReadLine();

                Console.SetCursorPosition(16, top + 7);   // eto naman Password to sah
                string pass = ReadPassword();

                Console.SetCursorPosition(0, top + 11);  

                if (user == USERNAME && pass == PASSWORD)
                {
                    PrintColored("  Login successful! Welcome, " + user + ".", ConsoleColor.Green);
                    Beep(900, 150); Beep(1200, 200);
                    Pause();
                    return true;
                }

                int left = MAX_ATTEMPTS - attempt;
                PrintColored("  Incorrect username or password. Attempts left: " + left, ConsoleColor.Red);
                Beep(300, 300);
                Pause();
            }

            Console.Clear();
            PrintColored("  Too many failed attempts. Access denied.", ConsoleColor.Red);
            return false;
        }

        static void DrawLoginBox(int attempt)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  ╔════════════════════════════════════════╗");
            Console.WriteLine("  ║              LOGIN  PAGE               ║");
            Console.WriteLine("  ╠════════════════════════════════════════╣");
            Console.WriteLine("  ║  Attempt " + attempt + " of " + MAX_ATTEMPTS + "                        ║");
            Console.WriteLine("  ║                                        ║");
            Console.WriteLine("  ║  Username :                            ║");  
            Console.WriteLine("  ║                                        ║");
            Console.WriteLine("  ║  Password :                            ║");   
            Console.WriteLine("  ║                                        ║");
            Console.WriteLine("  ╚════════════════════════════════════════╝");
            Console.ResetColor();
        }

        // ===================== MAIN MENU =====================
        static void MainMenu()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                PrintBanner("MAIN MENU", ConsoleColor.Yellow);
                Console.WriteLine("   ┌──────────────────────────────┐");
                Console.WriteLine("   │  [1]  Basic                  │");
                Console.WriteLine("   │  [2]  Intermediate           │");
                Console.WriteLine("   │  [3]  Extra                  │");
                Console.WriteLine("   │  [4]  EXIT                   │");
                Console.WriteLine("   └──────────────────────────────┘");
                Console.Write("\n   Enter your choice: ");

                switch (Console.ReadLine().Trim().ToLower())
                {
                    case "1": case "basic": BasicModule.Menu(); break;
                    case "2": case "intermediate": IntermediateModule.Menu(); break;
                    case "3": case "extra": ExtraModule.Menu(); break;
                    case "4": case "exit": running = false; break;
                    default: InvalidSelection(); break;
                }
            }
        }
    }
}