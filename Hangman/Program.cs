using System;
using System.Text.RegularExpressions;

namespace Hangman
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.SetWindowSize(50, 20);
			TitleScrean();
			string path = WordListPrompt();
			WordBank wordBank = new WordBank(path);
			DificultyPrompt(wordBank);

			bool playing = true;
			while (playing)
			{
				Game game = new Game(wordBank.PickWord());
				game.GameLoop();
				playing = ContinuePlaying();
			}
		}


		static bool ContinuePlaying()
		{
			Console.WriteLine("Press Esc to exit, or any other key to play again.");
			ConsoleKey prompt = Console.ReadKey(true).Key;
			return (prompt == ConsoleKey.Escape) ? false : true;
		}


		static void DificultyPrompt(WordBank wordBank)
		{
			string yPattern = "[y,Y]";

			Console.WriteLine("Specify dificulty? (y/N)");
			Char input = Console.ReadKey(true).KeyChar;
			if (Regex.Match(input.ToString(), yPattern).Success)
			{
				string nPattern = "[0-9]";
				int[] minMax = new int[2];
				string[] prompts = new string[] { "short", "long" };

				for (int i = 0; i < 2; i++)
				{
					while(true)
					{
						Console.Write($"How {prompts[i]} can the word be? 1-9: ");
						char response = Console.ReadKey().KeyChar;
						if (Regex.Match(response.ToString(), nPattern).Success)
						{
							minMax[i] = int.Parse(response.ToString());
							Console.WriteLine("");
							break;
						}
						InvalidEntry(response);
					}
				}
				wordBank.SetDificulty(minMax);
			}
		}


		static void EraseLines(int numLines)
		{
			var linePosition = Console.CursorTop;
			var width = Console.WindowWidth;
			Console.SetCursorPosition(0, linePosition - (numLines - 1));
			Console.Write(new String(' ', width * numLines));
			Console.SetCursorPosition(0, linePosition - (numLines - 1));
		}


		static void InvalidEntry(char entry)
		{
			Console.WriteLine($"\n{entry} is invalid. Enter a number from 1-9.");
			System.Threading.Thread.Sleep(500);
			EraseLines(3);
		}


		static void TitleScrean()
		{
			string p = "    ";
			Console.WriteLine($"\n{p},--.\n{p}|  O\n{p}| /|\\\n{p}| / \\\n H _ N G M _ N");
			Console.WriteLine("\nPress the any key to play.\n");
			Console.ReadKey(true);
		}


		static string WordListPrompt()
		{
			string pattern = "[y,Y]";
			Console.WriteLine("Do you have a word list to use? (y/N)");
			Char prompt = Console.ReadKey(true).KeyChar;
			if (Regex.Match(prompt.ToString(), pattern).Success)
			{
				Console.WriteLine("Enter the path to the text file: ");
				return Console.ReadLine();
			}
			return null;
		}
	}
}