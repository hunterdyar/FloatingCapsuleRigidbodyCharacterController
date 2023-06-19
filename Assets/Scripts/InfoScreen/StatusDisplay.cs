using Timeline;
using UnityEngine;

namespace Info
{
	public class StatusDisplay : MonoBehaviour
	{
		//Wrapper class for all children screens - is a state machine and data repo for screens.

		public GameTimeline Timeline;
		private InfoScreen _currentScreen;

		[SerializeField] private InfoScreen shipEventsScreen;

		
		void Start()
		{
			EnableInfoScreen(shipEventsScreen);
		}
		private void EnableInfoScreen(InfoScreen infoScreen)
		{
			if (_currentScreen != null)
			{
				_currentScreen.DisableScreen();
			}
			infoScreen.EnableScreen(this);
			_currentScreen = infoScreen;
		}
	}
	
	
}