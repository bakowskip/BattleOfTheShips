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
		public void ShowMap(IGameMap map)
		{
			Console.Clear();

			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WriteLine("   A  B  C  D  E  F  G  H  I  J ");
			
			for (int y = 0; y < map.MaxY; y++)
			{
				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Write((y + 1).ToString("D2"));

				Console.BackgroundColor = ConsoleColor.Gray;
				Console.ForegroundColor = ConsoleColor.Black;
				for (int x = 0; x < map.MaxX; x++)
				{
					if (map.MapArea[x, y].IsHidden)
						Console.Write("   ");
					else
					if (map.MapArea[x, y].WasHit)
					{
						if (map.MapArea[x, y].IsShip)
						{
							Console.ForegroundColor = map.MapArea[x, y].Ship.WasSank ? ConsoleColor.Red : ConsoleColor.Yellow;
							if (map.MapArea[x - 1, y].IsShip)
								Console.Write("█");
							else
								Console.Write(" ");

							Console.Write("█");

							if (map.MapArea[x + 1, y].IsShip)
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

		}

		public void ShowHitResult(int X, int Y, bool wasHit, bool wasSank)
		{

		}
	}
}
