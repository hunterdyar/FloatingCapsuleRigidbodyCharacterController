using System;
using System.Collections;
using HDyar.SimpleSOStateMachine;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timeline
{
	//Timeline fires of events into the scene, can be paused, etc.

	[CreateAssetMenu(fileName = "Game Timeline", menuName = "Ship/Game Timeline", order = 0)]
	public class GameTimeline : ScriptableObject
	{
		//todo: import state machine... subscribe to game state events.

		public Action OnBeat;//before events

		public Action<ShipEvent> OnShipEvent;

		[SerializeField] private State timelineActiveState;
		public float TimeBetweenBeats => _timeBetweenBeats;
		[SerializeField] private float _timeBetweenBeats;

		[SerializeField]
		private ShipBeat[] _shipBeats;//this is the actual timeline.

		private ShipBeat[] activeShipBeats;
		private int round = 0;
		
		public float CurrentCountdownInBeat { get; private set; }
		public IEnumerator RunTimeline()
		{
			for (int i = 0; i < _shipBeats.Length; i++)
			{
				round = i;
				CurrentCountdownInBeat = _timeBetweenBeats;
				while (CurrentCountdownInBeat > 0)
				{
					if (timelineActiveState.IsCurrentState)
					{
						CurrentCountdownInBeat -= Time.deltaTime;
					}
					yield return null;
				}

				CurrentCountdownInBeat = 0;

				OnBeat?.Invoke();
				foreach (var sEvent in _shipBeats[i].ShipEvents)
				{
					StartShipEvent(sEvent);
					yield return null;//to wait for animations, ship events could return coroutines.
				}
			}
		}

		private void StartShipEvent(ShipEvent sEvent)
		{
			Debug.Log("Ship Event: " + sEvent.displayName);
			//clone the ship event so we don't modify the scriptableObject's timeline.
			OnShipEvent?.Invoke(new ShipEvent(sEvent));
		}
	}
}