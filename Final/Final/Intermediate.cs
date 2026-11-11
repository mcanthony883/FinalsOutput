using System;
using System.Linq;
using static FinalsOutput.UI;

namespace FinalsOutput
{
    // Class used by the Grade Computation app
    class Student
    {
        public string Name { get; }
        public double[] Grades { get; }

        public Student(string name, double[] grades)
        {
            Name = name;
            Grades = grades;
        }

        public double Average() => Grades.Average();

        public string Remark() => Average() >= 75 ? "PASSED" : "FAILED";
    }

    // IntermediateModule.cs : Intermediate menu and its applications
    static class IntermediateModule
    {
        public static void Menu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                int choice = ArrowMenu("INTERMEDIATE MENU",
                            new[] { "ARRAY STATISTICS", "TEMPERATURE CONVERTER", "STUDENT GRADE COMPUTATION", "Back to Main Menu" },
                            ConsoleColor.DarkRed);

                switch (choice)

                {
                    case 0: RunLoop(ArrayStatistics); break;
                    case 1: RunLoop(TemperatureConverter); break;
                    case 2: RunLoop(GradeComputation); break;
                    case 3: back = true; break;
                }
            }
        }

        static void ArrayStatistics()
        {
            PrintBanner("ARRAY STATISTICS", ConsoleColor.Cyan);
            int size = ReadInt("   How many numbers? ");
            if (size <= 0) { PrintColored("   Size must be greater than 0.", ConsoleColor.Red); return; }

            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = ReadInt($"   Number #{i + 1}: ");
            }

            int[] sorted = (int[])arr.Clone();
            Array.Sort(sorted);

            Console.WriteLine();
            Console.WriteLine("   Original : " + string.Join(", ", arr));
            Console.WriteLine("   Sorted   : " + string.Join(", ", sorted));
            Console.WriteLine("   Sum      : " + arr.Sum());
            Console.WriteLine("   Average  : " + arr.Average().ToString("0.00"));
            Console.WriteLine("   Highest  : " + arr.Max());
            Console.WriteLine("   Lowest   : " + arr.Min());
        }

        static void TemperatureConverter()
        {
            PrintBanner("TEMPERATURE CONVERTER", ConsoleColor.Cyan);
            Console.WriteLine("   [1] Celsius    -> Fahrenheit");
            Console.WriteLine("   [2] Fahrenheit -> Celsius");
            Console.Write("   Choose: ");
            string c = Console.ReadLine().Trim();

            if (c == "1")
            {
                double cel = ReadDouble("   Enter Celsius: ");
                PrintColored($"\n   {cel}°C = {CelsiusToF(cel):0.00}°F", ConsoleColor.Yellow);
            }
            else if (c == "2")
            {
                double f = ReadDouble("   Enter Fahrenheit: ");
                PrintColored($"\n   {f}°F = {FahrenheitToC(f):0.00}°C", ConsoleColor.Yellow);
            }
            else
            {
                InvalidSelectionInline();
            }
        }

        static double CelsiusToF(double c) => (c * 9 / 5) + 32;
        static double FahrenheitToC(double f) => (f - 32) * 5 / 9;

        static void GradeComputation()
        {
            PrintBanner("STUDENT GRADE COMPUTATION", ConsoleColor.Cyan);
            Console.Write("   Student name: ");
            string name = Console.ReadLine();

            double[] grades = new double[3];
            string[] subjects = { "Prelim", "Midterm", "Finals" };
            for (int i = 0; i < grades.Length; i++)
            {
                grades[i] = ReadDouble($"   {subjects[i]} grade: ");
            }

            Student s = new Student(name, grades);   // object of our class
            Console.WriteLine();
            Console.WriteLine("   ----------------------------");
            Console.WriteLine("   Name    : " + s.Name);
            Console.WriteLine("   Average : " + s.Average().ToString("0.00"));
            PrintColored("   Remarks : " + s.Remark(),
                s.Remark() == "PASSED" ? ConsoleColor.Green : ConsoleColor.Red);
        }
    }
}