using System;
using Interaction;
using TiltFive;
using UnityEngine;
using Input = UnityEngine.Input;

namespace Character_Controller
{
	[RequireComponent(typeof(RBCharacterController))]
	public class CharacterControllerInput : MonoBehaviour
	{
		private RBCharacterController _characterController;
		private PlayerInteractionHandler _interactionHandler;
		public PlayerIndex TiltPlayerIndex { get; set; }
		public Transform ControllerForward { get; set; }//forward in world space.

		private void Awake()
		{
			_characterController = GetComponent<RBCharacterController>();
			_interactionHandler = GetComponent<PlayerInteractionHandler>();
		}

		private void Update()
		{
			if (TiltPlayerIndex != PlayerIndex.None)
			{
				//todo remap to forward
				_characterController.Move(TiltFive.Input.GetStickTilt(ControllerIndex.Right, TiltPlayerIndex));
				if (TiltFive.Input.GetButton(TiltFive.Input.WandButton.One,ControllerIndex.Right,TiltPlayerIndex))
				{
					_interactionHandler.Interact();
				}
			}
			else
			{
				_characterController.Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
				if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
				{
					_interactionHandler.Interact();
				}
			}
		}
	}
}