using System;
using Timeline;
using UnityEngine;

namespace Ship
{
	public class Ship : MonoBehaviour
	{
		public Action<int> OnHealthChanged;
		public int Health => _health;
		private int _health;

		[Header("Configuration")]
		[SerializeField] private int startingHealth = 3;

		//Station References
		[Header("Station References")] public DamageTypeDefenseStation[] DefenseStations;


		public void ProcessShipEvent(ShipEvent shipEvent)
		{
			//reduce any incoming damage.
			foreach (var defenseStation in DefenseStations)
			{
				defenseStation.ProcessShipEvent( ref shipEvent);
			}

			TakeDamage(ref shipEvent);
		}

		private void TakeDamage(ref ShipEvent shipEvent)
		{
			_health -= shipEvent.damage;
			if (_health < 0)
			{
				_health = 0;
				//change state machine to DED. (after we do the impact animations)
			}
			OnHealthChanged?.Invoke(_health);
			
			
		}
	}
}