using System;
using UnityEngine;

namespace Station
{
	public class Station : MonoBehaviour
	{
		public Action<bool> OnIsPoweredChange;
		[SerializeField] private bool requiresEnergyToInteract = true;
		[SerializeField]
		ResourceAreaMonitor energyBank;
		public int minimumResourcesToInteract;
		public Interactable InteractWithStationInteractable;//ie: a button to FIRE ZE MISSILEZ
		private bool isPowered;

		private void Start()
		{
			//force call to update initial state.
			if (!requiresEnergyToInteract)
			{
				isPowered = true;
			}
			else
			{
				CheckIfPowered();
			}
			
			OnIsPoweredChange?.Invoke(isPowered);
		}

		private void OnEnable()
		{
			energyBank.OnResourcesChange += CheckIfPowered;
			InteractWithStationInteractable.OnInteractStart += TryStationAction;
		}

		private void OnDisable()
		{
			energyBank.OnResourcesChange -= CheckIfPowered;
			InteractWithStationInteractable.OnInteractStart -= TryStationAction;
		}

		private void CheckIfPowered()
		{
			var newIsPowered = true;
			
			if (requiresEnergyToInteract)
			{
				newIsPowered = energyBank.ResourceCount >= minimumResourcesToInteract;
			}

			if (newIsPowered != isPowered)
			{
				isPowered = newIsPowered;
				OnIsPoweredChange?.Invoke(isPowered);
			}
		}

		private void TryStationAction()
		{
			Debug.Log($"Station Action! {gameObject.name}");
		}
	}
}