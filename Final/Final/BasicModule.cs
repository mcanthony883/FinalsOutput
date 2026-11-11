using System;
using static FinalsOutput.UI;

namespace FinalsOutput
{
  
    static class BasicModule
    {
        public static void Menu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                int choice = ArrowMenu("BASIC MENU",
                            new[] { "Simple Calculator", "Even or Odd Checker", "Multiplication Table", "Back to Main Menu" },
                            ConsoleColor.Green);

                switch (choice)

                {
                    case 0: RunLoop(Calculator); break;
                    case 1: RunLoop(EvenOdd); break;
                    case 2: RunLoop(MultiplicationTable); break;
                    case 3: back = true; break;
                }
            }
        }

        static void 
            Calculator()
        {
            PrintBanner("SIMPLE CALCULATOR", ConsoleColor.Green);
            double a = ReadDouble("   Enter first number  : ");
            Console.Write("   Operator (+ - * / %) : ");
            string op = Console.ReadLine().Trim();
            double b = ReadDouble("   Enter second number : ");

            double result;
            switch (op)
            {
                case "+": result = a + b; break;
                case "-": result = a - b; break;
                case "*": result = a * b; break;
                case "/":
                    if (b == 0) { PrintColored("   Cannot divide by zero!", ConsoleColor.Red); return; }
                    result = a / b; break;
                case "%":
                    if (b == 0) { PrintColored("   Cannot divide by zero!", ConsoleColor.Red); return; }
                    result = a % b; break;
                default:
                    PrintColored("   Invalid operator.", ConsoleColor.Red);
                    return;
            }
            PrintColored($"\n   {a} {op} {b} = {result:F3}", ConsoleColor.Yellow);
        }

        static void EvenOdd()
        {
            PrintBanner("EVEN OR ODD", ConsoleColor.Green);
            int n = ReadInt("   Enter an integer: ");
            string kind = (n % 2 == 0) ? "EVEN" : "ODD";
            PrintColored($"\n   {n} is an {kind} number.", ConsoleColor.Yellow);
        }

        static void MultiplicationTable()
        {
            PrintBanner("MULTIPLICATION TABLE", ConsoleColor.Green);
            int n = ReadInt("   Enter a number: ");
            Console.WriteLine();
            for (int i = 1; i <= 12; i++)
            {
                Console.WriteLine($"   {n} x {i,2} = {n * i}");
            }
        }
    }
}