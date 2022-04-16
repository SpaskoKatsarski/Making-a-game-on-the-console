﻿using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Hanger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int triesLeft = 10;

            string[] allWords = { "army", "arrow", "school", "building", "article", "answer", "animal", "bedroom", "mammal", "bowl", "championship", "commander", "customer", "programmer", "developer", "development", "studio", "employee", "interview", "environment", "episode", "firecracker", "harbor", "damnation", "abyss", "battleship", "storm", "fearless", "sergeant", "heart", "headline", "retreat", "highway", "horizon", "" };

            Random random = new Random();
            int index = random.Next(0, allWords.Length - 1);

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
            bool hintIteration = false;

            while (decryptedWord.ToString().Contains('_'))
            {
                if (triesLeft == 3)
                {
                    hintIteration = true;

                    Console.Write("Do you want a hint? -> ");

                    string answer = Console.ReadLine();

                    Console.WriteLine();

                    if (answer.ToLower() == "yes")
                    {
                        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray();

                        Console.Write($"The word contains one of these letters: ");

                        int indexOfCorrectLetter = random.Next(0, flagWord.Length);

                        while (WordIsInvalid(flagWord, indexOfCorrectLetter))
                        {
                            indexOfCorrectLetter = random.Next(0, flagWord.Length - 1);
                        }

                        char correctLetter = flagWord.ToString()[indexOfCorrectLetter];

                        List<char> hintLetters = new List<char>
                        {
                            correctLetter
                        };

                        int count = 0;

                        while (count < 2)
                        {
                            int indexOfCurrentIncorrectWord = random.Next(0, alphabet.Length - 1);

                            // Check if letter is actually invalid:

                            char currChar = alphabet[indexOfCurrentIncorrectWord];

                            if (currChar != correctLetter && !(flagWord.ToString().Contains(currChar)) && !(hintLetters.Contains(currChar)))
                            {
                                count++;

                                hintLetters.Add(currChar);
                            }
                        }

                        List<char> usedLetters = new List<char>();

                        for (int i = 0; i < 3; i++)
                        {
                            int randomIndex = random.Next(0, hintLetters.Count);

                            char currChar = hintLetters[randomIndex];

                            if (!usedLetters.Contains(currChar))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkCyan;

                                Console.Write($"{currChar}, ");

                                usedLetters.Add(currChar);
                            }
                            else
                            {
                                i--;
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                    }
                    else if (answer.ToLower() == "no")
                    {
                        Console.WriteLine("Sure, good luck!");
                    }

                    Console.WriteLine();
                    Console.Write("Write your answer: ");
                }

                if (!isFirstIteration && !hintIteration)
                {
                    Console.Write("Try again: ");
                }
                else if (!hintIteration)
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

                if (hintIteration)
                {
                    hintIteration = false;
                }
            }

            if (hasLost)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("You lost!");
                Console.WriteLine($"The word was '{wordToGuess}'");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Good job! You have correctly guessed the word and escaped your death! :D");
            }
        }

        static bool WordIsInvalid(StringBuilder flagWord, int indexOfCorrectLetter)
        {
            if (flagWord.ToString()[indexOfCorrectLetter] == '_' || flagWord.ToString()[indexOfCorrectLetter] == flagWord.ToString()[0] || flagWord.ToString()[indexOfCorrectLetter] == flagWord.ToString()[flagWord.ToString().Length - 1])
            {
                return true;
            }

            return false;
        }
    }
}
