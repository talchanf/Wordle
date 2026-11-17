using System;

class Program
{
    static void Main()
    {
        string[] words =
        {
            "apple", "house", "money", "music", "water", "light", "table", "plant", "smile", "train",
            "beach", "bread", "chair", "dream", "earth", "fruit", "green", "heart", "horse", "lemon",
            "magic", "night", "ocean", "paper", "piano", "queen", "river", "robot", "snake", "sugar",
            "tiger", "watch", "world", "zebra", "cloud", "candy", "grape", "honey", "juice", "mouse"
        };

        string secret = words[new Random().Next(words.Length)].ToUpper();
        Console.WriteLine("WORDLE - guess the 5-letter word (6 attempts)\n");

        for (int attempt = 1; attempt <= 6; attempt++)
        {
            Console.Write($"Attempt {attempt}/6 > ");
            string guess = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (guess.Length != 5)
            {
                Console.WriteLine("The word must have 5 letters.");
                attempt--; // this attempt does not count
                continue;
            }

            ShowResult(guess, secret);

            if (guess == secret)
            {
                Console.WriteLine($"\nWell done! Found in {attempt} attempt(s).");
                return;
            }
        }

        Console.WriteLine($"\nLost! The word was {secret}.");
    }

    static void ShowResult(string guess, string secret)
    {
        ConsoleColor[] colors = new ConsoleColor[5];
        char[] remaining = secret.ToCharArray();

        // 1) Green: right letter, right place
        for (int i = 0; i < 5; i++)
        {
            colors[i] = ConsoleColor.DarkGray;
            if (guess[i] == secret[i])
            {
                colors[i] = ConsoleColor.DarkGreen;
                remaining[i] = '*'; // this letter is used
            }
        }

        // 2) Yellow: right letter, wrong place (only if still available)
        for (int i = 0; i < 5; i++)
        {
            if (colors[i] == ConsoleColor.DarkGreen) continue;

            int pos = Array.IndexOf(remaining, guess[i]);
            if (pos != -1)
            {
                colors[i] = ConsoleColor.DarkYellow;
                remaining[pos] = '*';
            }
        }

        // Display
        Console.Write("              ");
        for (int i = 0; i < 5; i++)
        {
            Console.BackgroundColor = colors[i];
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($" {guess[i]} ");
            Console.ResetColor();
            Console.Write(" ");
        }
        Console.WriteLine();
    }
}