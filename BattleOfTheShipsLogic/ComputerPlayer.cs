using BattleOfTheShipsData;
using BattleOfTheShipsData.Exceptions;
using BattleofTheShipsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShipsLogic
{
    public class ComputerPlayer
    {
		
		private IGameMap _map;
		public IList<IShip> Ships { get; set; }
		public bool IsGameOver { get => Ships.All(s => s.WasSank); }
		private bool CanShipBePlacedHorizontally(IMapPoint origin, int size)
		{
			if (origin.X + size >= _map.MaxX)
				return false;

			for (int i=0;i<size;i++)
			{
				if (_map[origin.X + i, origin.Y].IsBlocked)
					return false;
			}
			return true;
		}

		private bool CanShipBePlacedVertically(IMapPoint origin, int size)
		{

			if (origin.Y + size >= _map.MaxX)
				return false;

			for (int i = 0; i < size; i++)
			{
				if (_map[origin.X , origin.Y+i].IsBlocked)
					return false;
			}
			return true;
		}

		private void PlaceShipHorizontally(IMapPoint origin, int size)
		{
			IMapPoint[] shipArea = new IMapPoint[size];
			//mark marigin
			for (int x=0;x<size+1;x++)
			{
				for (int y=0;y<3;y++)
				{
					_map[origin.X - 1 + x, origin.Y - 1 + y].IsBlocked = true;
					if (y == 1 && (x > 0 && x <= size))
						shipArea[x - 1] = _map[origin.X - 1 + x, origin.Y - 1 + y];
				}
			}
			var ship = _map.PlaceShip(shipArea);
			Ships.Add(ship);
			
		}

		private void PlaceShipVertically(IMapPoint origin, int size)
		{
			IMapPoint[] shipArea = new IMapPoint[size];
			//mark marigin and collect ship's map points
			for (int x = 0; x < 3 + 1; x++)
			{
				for (int y = 0; y < size +1; y++)
				{
					_map[origin.X - 1 + x, origin.Y - 1 + y].IsBlocked = true;
					if (x == 1 && (y > 0 && y <= size))
						shipArea[y - 1] = _map[origin.X - 1 + x, origin.Y - 1 + y];
				}
			}
			var ship = _map.PlaceShip(shipArea);
			Ships.Add(ship);
		}

		
		public ComputerPlayer(IGameMap gameMap)
		{
			if (gameMap == null || gameMap.MapArea == null)
				throw new ArgumentException("Cannot create computer player without a map","gameMap");
			_map = gameMap;
			Ships = new List<IShip>();
		}

		public void PlaceShips(int shipCount, int shipSize)
		{
			if (shipSize > _map.MaxX - 2 && shipSize > _map.MaxY -2)
			{
				throw new ShipException(shipSize, _map.MaxX, _map.MaxY);
			}

			var rng = new Random(DateTime.UtcNow.Millisecond);
			bool shipPlaced;
			bool canBeHorizontal;
			bool canBeVertical;
			int x, y;

			for (int s=0;s<shipCount; s++)
			{
				shipPlaced = false;
				var sanityCounter = 0;
				while (!shipPlaced && sanityCounter < 1000)
				{
					//get possible starting point range
					x = rng.Next(1, _map.MaxX - 2);
					y = rng.Next(1, _map.MaxY - 2);

					canBeHorizontal = CanShipBePlacedHorizontally(_map[x, y], shipSize);
					canBeVertical = CanShipBePlacedVertically(_map[x, y], shipSize);

					if (canBeHorizontal && canBeVertical)
					{
						if (rng.Next() % 2 == 0)
						{
							PlaceShipHorizontally(_map[x, y], shipSize);
							shipPlaced = true;
						}
						else
						{
							PlaceShipVertically(_map[x, y], shipSize);
							shipPlaced = true;
						}
					}
					else if (canBeHorizontal)
					{
						PlaceShipHorizontally(_map[x, y], shipSize);
						shipPlaced = true;
					}
					else if (canBeVertical)
					{
						PlaceShipVertically(_map[x, y], shipSize);
						shipPlaced = true;
					}

					sanityCounter++;
				}
				if (!shipPlaced)
				{
					throw new ShipException(shipSize, s, _map.MaxX, _map.MaxY);
				}
			}
		}

		public ShotResult CheckShot(IMapPoint shotPlace)
		{
			var sr = new ShotResult();

			if (shotPlace.X > _map.MaxX || shotPlace.Y > _map.MaxY || shotPlace.X < 0 || shotPlace.Y < 0)
			{
				throw new MapPointException(shotPlace.X, shotPlace.Y, "Invalid shot target");
			}

			_map[shotPlace.X, shotPlace.Y].WasHit = true;
			_map[shotPlace.X, shotPlace.Y].IsHidden = false;

			if (_map[shotPlace.X, shotPlace.Y].IsShip)
			{
				_map[shotPlace.X, shotPlace.Y].WasHit = true;
				sr.WasHit = true;
				sr.WasSank = _map[shotPlace.X, shotPlace.Y].Ship.WasSank;
			}
			return sr;
		}

		
    }
}