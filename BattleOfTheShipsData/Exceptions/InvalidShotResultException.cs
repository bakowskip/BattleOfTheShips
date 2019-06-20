using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShipsData.Exceptions
{
	public class InvalidShotResultException : Exception
	{
		public InvalidShotResultException(string message) : base(message)
		{
			
		}

		public InvalidShotResultException() : base("This Shot Result is invalid")
		{
		}
	}
}
