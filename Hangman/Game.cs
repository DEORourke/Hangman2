using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Hangman
{
	class Game
	{
		private Gallows _game;
		private Word _word;

		public Game(string word)
		{
			_game = new Gallows();
			_word = new Word(word);
		}


		public void GameLoop()
		{
			bool playing = true;

			while (playing)
			{
				Console.Clear();
				PrintGameboard();

				Console.WriteLine("Enter a letter to guess");
				char guess = Console.ReadKey(true).KeyChar;

				if (!ValidateInput(guess.ToString()))
				{
					Console.WriteLine("Invalid input".PadLeft(16));
					System.Threading.Thread.Sleep(500);
					continue;
				}

				if (_word.MakeGuess(guess))
				{
					if (_word.CheckForWin())
					{
						GameOver(true);
						playing = false;
					}
				}
				else
				{
					if (!_game.addStrike())
					{
						GameOver(false);
						playing = false;
					}
				}
			}
		}


		private void GameOver(bool correct)
		{
			Console.Clear();
			PrintGameboard();
			string message = correct ? " * Correct! * " : " >.< Game Over.";
			Console.WriteLine($"\n{message}\nThe answer was {_word.GetSolution()}.");
		}


		private void PrintGameboard()
		{
			_game.PrintGallows();
			_word.PrintPuzzle();
		}


		private bool ValidateInput(string guess)
		{
			string pattern = "[a-z,A-Z]{1}";	//Should work in the match expression without the variable
			return Regex.Match(guess, pattern).Success;
		}
	}
}

