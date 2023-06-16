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
			//beat = [wait... all events] in that order. So on round0 is likely waiting for shipbeat[0].
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

		public bool TryScanForShipEvents(float scanLocation, out ScanResults scanResults)
		{
			Debug.Log($"Scanning Sector {scanLocation}");
			float angleRange = 45f;
			int maxBeatDistance = 100;

			for (int i = round; i < _shipBeats.Length; i++)
			{
				for (int j = 0; j < _shipBeats[i].ShipEvents.Length; j++)
				{
					float loc = _shipBeats[i].ShipEvents[j].eventLocation;
					if (Mathf.Abs(scanLocation - loc) < angleRange)
					{
						scanResults = new ScanResults()
						{
							AnythingScanned = true,
							BeatsUntilEvent = i - round,
							ScannedEvent = _shipBeats[i].ShipEvents[j]
						};
						return true;
					}
				}	
			}

			scanResults = new ScanResults()
			{
				AnythingScanned = false,
				BeatsUntilEvent = 0,
				ScannedEvent = null
			};
			return false;
		}
	}
}