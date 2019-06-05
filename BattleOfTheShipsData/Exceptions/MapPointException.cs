using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShipsData.Exceptions
{
	public class MapPointException : Exception
	{
		public int X { get; set; }
		public int Y { get; set; }
		public MapPointException(int x, int y, string message) : base(message)
		{
			this.X = x;
			this.Y = y;			
		}

		public MapPointException(int x, int y, string message, Exception innerException) : base(message,innerException)
		{
			this.X = x;
			this.Y = y;
		}
	}
}
