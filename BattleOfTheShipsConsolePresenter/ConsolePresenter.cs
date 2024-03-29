﻿using BattleOfTheShipsData;
using BattleOfTheShipsData.Exceptions;
using BattleofTheShipsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShipsConsolePresenter
{
	public class ConsolePresenter : IPresenter
	{
		private const int START_CHAR = (int)'A';
		public void ShowMap(IGameMap map)
		{
			if (!Console.IsOutputRedirected)
				Console.Clear();

			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			
			Console.Write("  ");
			for (int i = 0; i < map.MaxX;i++)
			{
				
				Console.Write(" " + Convert.ToChar(START_CHAR + i) + " ");
			}

			Console.WriteLine("");
			
			for (int y = 0; y < map.MaxY; y++)
			{
				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Write((y + 1).ToString("D2"));

				Console.BackgroundColor = ConsoleColor.Gray;
				Console.ForegroundColor = ConsoleColor.Black;
				for (int x = 0; x < map.MaxX; x++)
				{
					if (map[x, y].IsHidden)
						Console.Write("   ");
					else
					if (map[x, y].WasHit)
					{
						if (map[x, y].IsShip)
						{
							Console.ForegroundColor = map[x, y].Ship.WasSank ? ConsoleColor.Red : ConsoleColor.Yellow;
							if (map[x - 1, y].IsShip)
								Console.Write("█");
							else
								Console.Write(" ");

							Console.Write("█");

							if (map[x + 1, y].IsShip)
								Console.Write("█");
							else
								Console.Write(" ");
						}
						else
						{
							Console.ForegroundColor = ConsoleColor.Black;
							Console.Write(" o ");
						}
					}
				}
				Console.WriteLine();
			}			
		}

		public void ShowHitResult(IMapPoint target, bool wasHit, bool wasSank)
		{
			var strBuilder = new StringBuilder();
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;

			strBuilder.Append($"Your shot at {Convert.ToChar(START_CHAR + target.X)}{target.Y+1} ");
			if (!wasHit)
				strBuilder.Append("missed");
			else
			{
				if (wasSank)
					strBuilder.Append("hit and sank a ship!");
				else
					strBuilder.Append("hit but did not sink a ship!");
			}		

			Console.WriteLine(strBuilder.ToString());
		}

		public IMapPoint ConvertUserInputToMapPoint(string coordinates)
		{
			int x=0,y=0;
			
			if (coordinates.Length <2 || coordinates.Length > 3)
				throw new MapPointException(x, y, $"Invalit target coordinates: {coordinates}");

			char cX = coordinates.ToUpper().ToCharArray()[0];
			if (Char.IsLetter(cX))
			{
				x = (int)cX - START_CHAR;
			}
			else
				throw new MapPointException(x, y, $"Invalit target coordinates: {coordinates}");

			if (x< 0 || x > 20)
				throw new MapPointException(x, y, $"Invalit target coordinates: {coordinates}");

			if (!int.TryParse(coordinates.Substring(1), out y) || y < 1 || y > 20)
				throw new MapPointException(x, y, $"Invalit target coordinates: {coordinates}");
			else
				y = y - 1;


			return new MapPoint(x, y);
		}

		public void ShowMessage(string message, bool wait)
		{
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(message);

			if (wait)
				Console.ReadKey();
		}
	}
}
