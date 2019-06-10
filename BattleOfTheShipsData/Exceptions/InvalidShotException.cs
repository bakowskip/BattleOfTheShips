using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShipsData.Exceptions
{
	public class InvalidShotException : MapPointException
	{
		public InvalidShotException(int x, int y) : base(x,y,$"Invalid Shot at ({x},x{y}).")
		{

		}
	}
}
