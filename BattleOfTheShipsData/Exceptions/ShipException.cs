using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShipsData.Exceptions
{
	public class ShipException : Exception
	{
		public int ShipSize { get; set; }
		public int MapMaxX { get; set; }
		public int MapMaxY { get; set; }
		public int MapX { get; set; }
		public int MapY { get; set; }
		public int ShipCount { get; set; }
		public ShipException(int shipSize, int mapMaxX, int mapMaxY) : base($"Ship size {shipSize} too big for map {mapMaxX}x{mapMaxY}")
		{
			ShipSize = shipSize;
			MapMaxX = mapMaxX;
			MapMaxY = mapMaxY;
		}

		public ShipException(int shipSize, int mapX, int mapY, string message) : base(message)
		{
			ShipSize = shipSize;
			MapX = mapX;
			MapY = mapY;
		}

		public ShipException(int shipSize,int shipCount, int mapMaxX, int mapMaxY) : base($"Can't fit ship no {shipCount} with size {shipSize} on map {mapMaxX}x{mapMaxY}")
		{
			ShipSize = shipSize;
			MapMaxX = mapMaxX;
			MapMaxY = mapMaxY;
			ShipCount = shipCount;
		}
	}
}