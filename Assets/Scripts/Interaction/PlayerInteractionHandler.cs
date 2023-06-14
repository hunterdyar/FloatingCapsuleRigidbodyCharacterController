using System;
using UnityEngine;

namespace Interaction
{
	public class PlayerInteractionHandler : MonoBehaviour
	{
		[SerializeField] private InteractionZone _zone;
		[SerializeField] private GrabHandler _grabHandler;
		private void Awake()
		{
			_zone.SetPlayer(this);
		}

		public void Interact()
		{
			//First, we check if we are holding something. If so, we call interact on the grabber, which throws.
			if (_grabHandler.IsHolding)
			{
				//todo: throwForce
				_grabHandler.Throw(transform.forward);
				return;
			}
			//Then, we look for things to pick up, which we prioritize over the closest thing, and interact with it.
			if (_zone.TryGetClosestInteractableOfType<Grabbable>(out var grabbable))
			{
				if (_grabHandler.TryGrab(grabbable))
				{
					Debug.Log("Picked up "+grabbable.gameObject.name);
					return;
				}
			}
			//then, we interact with anything else.
			if (_zone.TryGetClosestInteractable(out var interactable))
			{
				if (interactable.CanInteract)
				{
					interactable.DirectInteract();
					return;
				}
			}
		}
	}
}