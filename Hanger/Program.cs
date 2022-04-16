using System;
using System.Text;

namespace Hanger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int triesLeft = 10;

            string[] allWords = { "army", "arrow", "school", "building" };

            Random random = new Random();
            int index = random.Next(0, allWords.Length);

            string wordToGuess = allWords[index];

            StringBuilder flagWord = new StringBuilder(wordToGuess);

            StringBuilder decryptedWord = new StringBuilder();

            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (i == 0)
                {
                    decryptedWord.Append(wordToGuess[i]);
                }
                else if (i == wordToGuess.Length - 1)
                {
                    decryptedWord.Append(wordToGuess[i]);
                }
                else
                {
                    decryptedWord.Append('_');
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"The game has started and you have a total of {triesLeft} tries.");
            Console.WriteLine($"Try to guess the word: '{decryptedWord}'");

            Console.WriteLine();
            
            bool hasLost = false;
            bool isFirstIteration = true;

            while (decryptedWord.ToString().Contains('_'))
            {
                if (!isFirstIteration)
                {
                    Console.Write("Try again: ");
                }
                else
                {
                    Console.Write("Write your guess: ");
                }

                char tryLetter = char.Parse(Console.ReadLine());
                tryLetter = Char.ToLower(tryLetter);

                Console.WriteLine();

                if (flagWord.ToString().Contains(tryLetter) && !(decryptedWord.ToString().Contains(tryLetter)))
                {
                    // The user has guessed a letter which is not represented in the decrypted word.
                    // We have to replace all '_' where we have its occurrence.

                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine($"The word contains '{tryLetter}'!");

                    while (flagWord.ToString().Contains(tryLetter))
                    {
                        int indexOfGuessedLetter = flagWord.ToString().IndexOf(tryLetter);
                        flagWord.Remove(indexOfGuessedLetter, 1);
                        flagWord.Insert(indexOfGuessedLetter, '_');

                        // school
                        
                        decryptedWord.Remove(indexOfGuessedLetter, 1);
                        decryptedWord.Insert(indexOfGuessedLetter, tryLetter);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine($"Tries left {{{--triesLeft}}}");
                }

                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine($">> {decryptedWord} <<");
                Console.WriteLine();

                if (triesLeft == 0)
                {
                    hasLost = true;
                    break;
                }

                if (isFirstIteration)
                {
                    isFirstIteration = false;
                }
            }

            if (hasLost)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("You lost!");
                Console.WriteLine($"The word was {wordToGuess}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Good job! You have correctly guessed the word and escaped your death! :D");
            }
        }
    }
}
