using System;
using UnityEngine;

namespace Interaction
{
	public class GrabHandler : MonoBehaviour
	{
		public bool IsHolding => _holdingGrabbable != null;
		private Grabbable _holdingGrabbable;
		
		[SerializeField] private Rigidbody playerBody;
		private SpringJoint _grabJoint;

		public bool TryGrab(Grabbable grabbable)
		{
			if (IsHolding)
			{
				return false;
			}
			
			//other things preventing us from grabbing this object?

			Grab(grabbable);
			return true;
		}

		/// <summary>
		/// Will always try to grab.
		/// </summary>
		private void Grab(Grabbable grabbable)
		{
			//turn off auto-connected anchor?
			_grabJoint = grabbable.gameObject.AddComponent<SpringJoint>();
			_grabJoint.autoConfigureConnectedAnchor = false;
			_grabJoint.connectedAnchor = transform.localPosition;
			_grabJoint.enableCollision = true;
			_grabJoint.spring = 500;

			_grabJoint.damper = 20;
			_grabJoint.connectedBody = playerBody;//this should snap to our hands now.
			_holdingGrabbable = grabbable;
		}

		public void Throw(Vector3 throwForce)
		{
			Destroy(_grabJoint);
			_holdingGrabbable.Rigidbody.AddForce(throwForce,ForceMode.Impulse);
		}
	}
}