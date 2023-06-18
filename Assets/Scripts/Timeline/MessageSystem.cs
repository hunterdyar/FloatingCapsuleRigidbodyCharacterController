using System;
using System.Collections.Generic;
using Ship;
using UnityEngine;

namespace Timeline
{
	public class MessageSystem : MonoBehaviour
	{
		public static Action OnNewMessage;
		private GameTimeline _gameTimeline => _ship.GameTimeline;
		private Ship.Ship _ship;
		private readonly List<MessageInfo> _allMessages = new List<MessageInfo>();


		private void Awake()
		{
			_ship = GetComponent<Ship.Ship>();
		}

		private void OnEnable()
		{
			_gameTimeline.OnTimelineStarted += Init;
			_gameTimeline.OnShipEvent += ProcessShipEvent;
			_ship.OnStatusEffectGained += OnGainStatusEffect;
		}

		private void OnDisable()
		{
			_gameTimeline.OnTimelineStarted -= Init;
			_gameTimeline.OnShipEvent -= ProcessShipEvent;
			_ship.OnStatusEffectGained -= OnGainStatusEffect;
		}

		private void OnGainStatusEffect(StatusEffect effect)
		{
			BroadcastMessage(effect.GetMessage());
		}

		private void ProcessShipEvent(ShipEvent shipEvent)
		{
			BroadcastMessage(shipEvent.GetMessage());
		}

		private void BroadcastMessage(MessageInfo message)
		{
			Debug.Log("Info: "+message.Message);
			_allMessages.Add(message);
		}

		private void Init()
		{
			_allMessages.Clear();
		}
	}
}