using System;


namespace Hangman
{
	public class Gallows
	{
		private readonly char[] _partsList = { 'O', '/', '|', '\\', '/', '\\' };
		private char[] _hangedParts = { ' ', ' ', ' ', ' ', ' ', ' ' };
		private int _strikes = 0;

		public bool addStrike()
		{
			_hangedParts[_strikes] = _partsList[_strikes];
			_strikes++;

			if (_strikes == 6) { return false; }

			return true;
		}


		public char getChar(int i)
		{
			return _partsList[i];
		}


		public void PrintGallows()
		{
			string indent = "    ";
			Console.WriteLine($"\n{indent},--.");
			Console.WriteLine($"{indent}|  {_hangedParts[0]}");
			Console.WriteLine($"{indent}| {_hangedParts[1]}{_hangedParts[2]}{_hangedParts[3]}");
			Console.WriteLine($"{indent}| {_hangedParts[4]} {_hangedParts[5]}");
		}
	}
}
