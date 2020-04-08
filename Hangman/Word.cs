using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
	public class Word
	{
		private List<char> _answer;
		private List<char> _guesses;
		private List<char> _solution;
		private int _length;	// there are a couple of cases where I need to do something based off of how long the _solution is. Does it make more sense to have a variable to store that value up top, or to use _solution.Count each time?
		
		public Word(string word)
		{
			_solution = new List<char>(word);
			_answer = new List<char>();
			_guesses = new List<char>();
			_length = _solution.Count;	//doesn't take too much time to calculate each time we need it because it's a local property
			
			MaskSolution(_length);
		}


		public bool CheckForWin()
		{
			return _solution.SequenceEqual(_answer);
		}


		public string GetSolution()
		{
			return String.Join("", _solution);
		}


		public bool MakeGuess(char guess)
		{
			// Can/should this method be more abstract? I want the Word class to handle all of the behind the scenes functionality for testing guess letters, and I guess there has to be something that calls all those little bits.
			if(CheckForRepeat(guess))
			{
				Console.WriteLine($"You've already guessed {guess}");
				System.Threading.Thread.Sleep(500);
				return true;
			}

			_guesses.Add(guess);

			List<int> indices = FindLetters(guess);
			if (indices.Count == 0) { return false; }

			RevealLetter(indices);
			return true;
		}


		public void PrintPuzzle()
		{
			Console.WriteLine(FormatAnswer());
			Console.WriteLine(FormatGuessList());
		}


		private bool CheckForRepeat(char guess)
		{
			return (_guesses.IndexOf(guess) != -1) ? true : false;
		}


		private List<int> FindLetters(char guess)
		{
			List<int> indices = new List<int> { };
			int index = -1;
			while (true)
			{
				index = _solution.IndexOf(char.ToUpper(guess), index + 1);
	

					
				if (index == -1) { return indices; }

				indices.Add(index);
			}
		}


		private string FormatGuessList()
		{
			return String.Concat("Previous guesses: ", String.Join(", ", _guesses));
		}


		private string FormatAnswer()
		{ 
			List<char> paddedAnswer = new List<char>();

			foreach (char thing in _answer)
			{
				paddedAnswer.Add(thing);
				paddedAnswer.Add(' ');
			}

			return String.Concat(GetPadding(), String.Concat(paddedAnswer));
			// a part of me really wants to write statements like this out on differnet lines as seperate statements
			
		}
		

		private string GetPadding()
		{
			int paddingCharacters = (_length < 7) ? 8 - _length : 1;
			return new String(' ', paddingCharacters);
		}


		private void MaskSolution(int length)
		{
			for (int i = 0; i < length; i++)
			{
				_answer.Add('_');
			}
		}


		private void RevealLetter(List<int> indices)
		{
			foreach (int index in indices)
			{
				_answer[index] = _solution[index];
			}
		}

	}
}
