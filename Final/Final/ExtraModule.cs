using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static FinalsOutput.UI;

namespace FinalsOutput
{
    class Question
    {
        public string Text { get; }
        public string[] Choices { get; }
        public int Answer { get; }   // 1-based index

        public Question(string text, string[] choices, int answer)
        {
            Text = text; Choices = choices; Answer = answer;
        }
    }

    static class ExtraModule
    {
        public static void Menu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                int choice = ArrowMenu("BASIC MENU",
                            new[] { "MAIN ENTERTAINMENT", "NUMBER GUESSING GAME", "TRIVIA QUIZ", "Back to Main Menu" },
                            ConsoleColor.Green);

                switch (choice)

                {
                    case 0: RunLoop(FrogGame); break;
                    case 1: RunLoop(GuessingGame); break;
                    case 2: RunLoop(TriviaQuiz); break;
                    case 3: back = true; break;
                }
            }
        }

        static void GuessingGame()
        {
            PrintBanner("NUMBER GUESSING GAME", ConsoleColor.Magenta);
            int secret = new Random().Next(1, 101), tries = 0, guess;
            Console.WriteLine("   I'm thinking of a number from 1 to 100.\n");
            do
            {
                guess = ReadInt("   Your guess: ");
                tries++;
                if (guess < secret) PrintColored("   Too LOW!", ConsoleColor.Blue);
                else if (guess > secret) PrintColored("   Too HIGH!", ConsoleColor.Red);
            } while (guess != secret);
            PrintColored($"\n   Correct! You got it in {tries} tries!", ConsoleColor.Green);
            Beep(700, 120); Beep(900, 120); Beep(1200, 250);
        }

        static void TriviaQuiz()
        {
            PrintBanner("TRIVIA QUIZ", ConsoleColor.Magenta);
            var questions = new List<Question>
            {
                new Question("What keyword is used to create a class in C#?", new[] { "object", "class", "struct", "new" }, 2),
                new Question("Which symbol ends a statement in C#?", new[] { ":", ".", ";", "," }, 3),
                new Question("Which method is the entry point of a C# program?", new[] { "Start()", "Run()", "Begin()", "Main()" }, 4),
                new Question("Which data type stores true/false?", new[] { "bool", "int", "string", "char" }, 1),
                new Question("Which loop runs at least once?", new[] { "for", "while", "do-while", "foreach" }, 3)
            };

            int score = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                Question q = questions[i];
                Console.WriteLine($"\n   Q{i + 1}. {q.Text}");
                for (int c = 0; c < q.Choices.Length; c++)
                    Console.WriteLine($"      {c + 1}) {q.Choices[c]}");

                if (ReadInt("   Your answer: ") == q.Answer)
                {
                    PrintColored("   Correct!", ConsoleColor.Green);
                    Beep(1000, 120);
                    score++;
                }
                else
                {
                    PrintColored($"   Wrong! Answer: {q.Answer}) {q.Choices[q.Answer - 1]}", ConsoleColor.Red);
                    Beep(300, 200);
                }
            }
            PrintColored($"\n   Final Score: {score} / {questions.Count}", ConsoleColor.Yellow);
        }

        // Frog
        // Jump: Reverse Mode - the OBSTACLE jumps over the Dino (SPACE).
        static void FrogGame()
        {
            Console.WriteLine("\n   FROG JUMP");
            Console.WriteLine("   SPACE = makes it jump. 😆");
            Console.Write("\n   Press SPACE to start...");
            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) { }

            int x = 25, jump = 0, score = 0, speed = 100;   // x counts emoji cells (2 columns each)
            Console.Clear();
            Console.CursorVisible = false;

            while (true)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar && jump == 0) jump = 1;
                int y = jump == 0 ? 0 : (jump <= 3 ? jump : 7 - jump);    // obstacle height: 0 = ground
                if (jump > 0) jump = jump == 6 ? 0 : jump + 1;

                if (--x < 0) { x = 25; score += 10; speed = Math.Max(40, speed - 5); }
                if (y == 0 && x >= 3 && x <= 5) break;                    // obstacle hits the Dino

                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"\n   SCORE: {score}   SPEED: {130 - speed}   \n");
                for (int h = 3; h >= 0; h--)
                {
                    var cells = Enumerable.Repeat("  ", 30).ToArray();
                    if (h == 0) cells[4] = "🐸";
                    if (h == y) cells[x] = "🌵";
                    Console.WriteLine(string.Concat(cells));
                }
                Console.WriteLine(new string('-', 60));
                Thread.Sleep(speed);
            }

            Console.CursorVisible = true;
            while (Console.KeyAvailable) Console.ReadKey(true);
            Console.Clear();
            PrintColored($"\n   GAME OVER!  Final score: {score}", ConsoleColor.Red);
            Beep(400, 200); Beep(250, 400);
        }
    }
}