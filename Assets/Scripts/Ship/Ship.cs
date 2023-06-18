using System;
using System.Collections.Generic;
using System.Diagnostics;
using Timeline;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Debug = UnityEngine.Debug;

namespace Ship
{
	public class Ship : MonoBehaviour
	{
		public GameTimeline GameTimeline => _gameTimeline;
		[SerializeField] private GameTimeline _gameTimeline;
		public Action<int> OnHealthChanged;
		public int Health => _health;
		public Action<StatusEffect> OnStatusEffectGained;

		private int _health;

		[Header("Configuration")]
		[SerializeField] private int startingHealth = 3;

		[SerializeField] private StatusEffect[] _startingStatusEffects;
		private readonly List<StatusEffect> _statusEffects = new List<StatusEffect>();

		//Station References
		[Header("Station References")] public DamageTypeDefenseStation[] DefenseStations;

		void Start()
		{
			_health = startingHealth;
			foreach(var effect in _startingStatusEffects)
			{
				GainStatusEffect(effect);
			}
		}

		private void GainStatusEffect(StatusEffect effect)
		{
			_statusEffects.Add(effect);
			effect.OnGain(this);
			OnStatusEffectGained?.Invoke(effect);
		}

		public void LoseStatusEffect(StatusEffect effect)
		{
			_statusEffects.Remove(effect);
			effect.OnLose();
		}


		private void OnEnable()
		{
			_gameTimeline.OnShipEvent += ProcessShipEvent;
		}

		private void OnDisable()
		{
			_gameTimeline.OnShipEvent -= ProcessShipEvent;
		}

		private void Update()
		{
			foreach (var effect in _statusEffects)
			{
				effect.Tick();
			}
		}

		public void ProcessShipEvent(ShipEvent shipEvent)
		{
			//reduce any incoming damage.
			foreach (var defenseStation in DefenseStations)
			{
				defenseStation.ProcessShipEvent( ref shipEvent);
			}

			//todo: status before or after damage?
			TakeDamage(ref shipEvent);

			//apply status effect
			if (shipEvent.StatusEffect != null)
			{
				GainStatusEffect(shipEvent.StatusEffect);
			}
			
		}

		private void TakeDamage(ref ShipEvent shipEvent)
		{
			_health -= shipEvent.damage;
			if (_health < 0)
			{
				_health = 0;
				//change state machine to DED. (after we do the impact animations)
			}

			Debug.Log("Ship Took " + shipEvent.damage + " damage!");
			OnHealthChanged?.Invoke(_health);
		}
	}
}