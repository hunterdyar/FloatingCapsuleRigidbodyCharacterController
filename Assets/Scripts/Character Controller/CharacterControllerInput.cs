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
		private ControllerIndex _controllerIndex = ControllerIndex.Right;
		public PlayerIndex TiltPlayerIndex;
		public GameObject ControllerForward { get; set; }//forward in world space.

		public TiltFive.Input.WandButton interactButton;
		private float _trigger;
		[Range(0,1)]public float _triggerThreshold;
		private bool _triggerPressed;
		
		private void Awake()
		{
			_characterController = GetComponent<RBCharacterController>();
			_interactionHandler = GetComponent<PlayerInteractionHandler>();
		}

		private void Start()
		{
			TiltFive.Wand.TryCheckConnected(out var rightConnected, TiltPlayerIndex, ControllerIndex.Right);
			TiltFive.Wand.TryCheckConnected(out var leftConnected, TiltPlayerIndex, ControllerIndex.Left);

			if (rightConnected)
			{
				_controllerIndex = ControllerIndex.Right;
			}else if (leftConnected)
			{
				//left and not right. todo: just support both at the same time.
				_controllerIndex = ControllerIndex.Left;
			}
		}

		private void Update()
		{
			if (TiltPlayerIndex != PlayerIndex.None)
			{
				//todo remap to forward
				var tFiveInput = TiltFive.Input.GetStickTilt(_controllerIndex, TiltPlayerIndex);
				_characterController.Move(tFiveInput);
				_trigger = TiltFive.Input.GetTrigger(_controllerIndex, TiltPlayerIndex);
				if (_trigger > _triggerThreshold)
				{
					if (!_triggerPressed)
					{
						_triggerPressed = true;
						_interactionHandler.Interact();
					}
				}
				else
				{
					_triggerPressed = false;
				}
				
				
				//alt
				if (TiltFive.Input.GetButtonDown(interactButton,_controllerIndex,TiltPlayerIndex))
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