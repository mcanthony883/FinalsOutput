using System;
using System.Text;

namespace FinalsOutput
{

    static class UI
    {
        public static int ArrowMenu(string title, string[] options, ConsoleColor color)
        {
            int selected = 0;
            ConsoleKey key;
            Console.CursorVisible = false;

            do
            {
                Console.Clear();
                PrintBanner(title, color);

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selected)   // highlighted option
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("   > " + options[i] + "   ");
                        Console.ResetColor();
                    }
                    else
                        Console.WriteLine("     " + options[i]);
                }

                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                    selected = (selected == 0) ? options.Length - 1 : selected - 1;
                else if (key == ConsoleKey.DownArrow)
                    selected = (selected == options.Length - 1) ? 0 : selected + 1;

            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;
            return selected; 
        }
        public static void RunLoop(Action app)
        {
            string choice;
            do
            {
                Console.Clear();
                app();

                while (true)
                {
                    Console.Write("\n   [T] Try Again   [Q] Quit  : ");
                    choice = Console.ReadLine().Trim().ToUpper();
                    if (choice == "T" || choice == "Q") break;
                    InvalidSelectionInline();
                }
            } while (choice == "T");
        }

        public static void InvalidSelection()
        {
            PrintColored("\n   Invalid Selection", ConsoleColor.Red);
            Beep(300, 250);
            Pause();
        }

        public static void InvalidSelectionInline()
        {
            PrintColored("   Invalid Selection", ConsoleColor.Red);
            Beep(300, 250);
        }

        public static void PrintBanner(string title, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            string line = new string('═', 36);
            Console.WriteLine("\n   ╔" + line + "╗");
            Console.WriteLine("   ║" + title.PadLeft((36 + title.Length) / 2).PadRight(36) + "║");
            Console.WriteLine("   ╚" + line + "╝\n");
            Console.ResetColor();
        }

        public static void PrintColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static int ReadInt(string prompt)
        {
            int value;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                PrintColored("   Invalid input. Numbers only.", ConsoleColor.Red);
                Console.Write(prompt);
            }
            return value;
        }

        public static double ReadDouble(string prompt)
        {
            double value;
            Console.Write(prompt);
            while (!double.TryParse(Console.ReadLine(), out value))
            {
                PrintColored("   Invalid input. Numbers only.", ConsoleColor.Red);
                Console.Write(prompt);
            }
            return value;
        }

        public static string ReadPassword()
        {
            StringBuilder sb = new StringBuilder();
            ConsoleKeyInfo key;
            while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.Backspace && sb.Length > 0)
                {
                    sb.Length--;
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    sb.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            return sb.ToString();
        }

        public static void Pause()
        {
            Console.Write("\n   Press any key to continue...");
            Console.ReadKey(true);
        }


        public static void Beep(int freq, int ms)
        {
            try { Console.Beep(freq, ms); } catch { }
        }

        public static void Loading(string text)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("\n   " + text);
            Console.Write("   [");
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < 30; i++)
            {
                Console.Write("█");
                Thread.Sleep(60);      
            }
            Console.ResetColor();
            Console.WriteLine("] 100%");
            Console.CursorVisible = true;
            Thread.Sleep(300);
        }
    }
}