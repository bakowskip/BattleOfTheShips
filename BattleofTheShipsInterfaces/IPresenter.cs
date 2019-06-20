namespace BattleofTheShipsInterfaces
{
	public interface IPresenter
	{
		void ShowMap(IGameMap map);
		void ShowHitResult(IMapPoint target, bool wasHit, bool wasSank);
		IMapPoint ConvertUserInputToMapPoint(string coordinates);
		void ShowMessage(string message, bool wait);
	}
}