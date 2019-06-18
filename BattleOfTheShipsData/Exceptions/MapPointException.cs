using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShipsData.Exceptions
{
	public class MapPointException : Exception
	{
		public long X { get; set; }
		public long Y { get; set; }
		public MapPointException(long x, long y) : base("Invalid Map Point used.")
		{
			this.X = x;
			this.Y = y;			
		}
		public MapPointException(long x, long y, string message) : base(message)
		{
			this.X = x;
			this.Y = y;
		}

		public MapPointException(long x, long y, string message, Exception innerException) : base(message,innerException)
		{
			this.X = x;
			this.Y = y;
		}
	}
}
