using UnityEngine;

namespace Timeline
{
	//Timeline fires of events into the scene, can be paused, etc.

	[CreateAssetMenu(fileName = "Game Timeline", menuName = "Ship/Game Timeline", order = 0)]
	public class GameTimeline : ScriptableObject
	{
		//todo: import state machine... subscribe to game state events.

		public float TimeBetweenBeats => _timeBetweenBeats;
		[SerializeField] private float _timeBetweenBeats;

		private ShipBeat[] _shipBeats;//this is the actual timeline.

	}
}