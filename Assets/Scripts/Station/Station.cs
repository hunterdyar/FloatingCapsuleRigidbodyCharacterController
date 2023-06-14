using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Station
{
	public class Station : MonoBehaviour
	{
		public Action<bool> OnIsPoweredChange;
		[SerializeField] private bool requiresEnergyToInteract = true;
		[SerializeField]
		ResourceAreaMonitor energyBank;
		public int minimumResourcesToInteract;
		public int energyToBurnOnInteract;
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
			if (energyToBurnOnInteract > 0)
			{
				if (!energyBank.TryBurnResources(energyToBurnOnInteract))
				{
					//failed to burn enough! We wasted resources!
					return;
				}
			}
			
			Debug.Log($"Station Action! {gameObject.name}");
		}
	}
}