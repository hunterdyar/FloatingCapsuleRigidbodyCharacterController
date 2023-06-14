using System;
using Interaction;
using UnityEngine;

namespace Character_Controller
{
	[RequireComponent(typeof(RBCharacterController))]
	public class CharacterControllerInputSample : MonoBehaviour
	{
		private RBCharacterController _characterController;
		private PlayerInteractionHandler _interactionHandler;

		private void Awake()
		{
			_characterController = GetComponent<RBCharacterController>();
			_interactionHandler = GetComponent<PlayerInteractionHandler>();
		}

		private void Update()
		{
			_characterController.Move(new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")));
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
			{
				_interactionHandler.Interact();
			}
		}
	}
}