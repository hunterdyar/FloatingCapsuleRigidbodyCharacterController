using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timeline
{
	//Timeline fires of events into the scene, can be paused, etc.

	[CreateAssetMenu(fileName = "Game Timeline", menuName = "Ship/Game Timeline", order = 0)]
	public class GameTimeline : ScriptableObject
	{
		//todo: import state machine... subscribe to game state events.

		public Action<ShipEvent> OnShipEvent;
		public float TimeBetweenBeats => _timeBetweenBeats;
		[SerializeField] private float _timeBetweenBeats;

		[SerializeField]
		private ShipBeat[] _shipBeats;//this is the actual timeline.

		private int round = 0;
		public IEnumerator RunTimeline()
		{
			for (int i = 0; i < _shipBeats.Length; i++)
			{
				round = i;

				float t = _timeBetweenBeats;
				while (t > 0)
				{
					t -= Time.deltaTime;
					yield return null;
				}

				foreach (var sEvent in _shipBeats[i].ShipEvents)
				{
					StartShipEvent(sEvent);
					yield return null;
				}
			}
		}

		private void StartShipEvent(ShipEvent sEvent)
		{
			Debug.Log("Ship Event: " + sEvent.displayName);
			OnShipEvent?.Invoke(sEvent);
		}
	}
}