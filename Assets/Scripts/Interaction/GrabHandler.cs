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

		[SerializeField] private SpringJoint jointPreset;

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

			//apply preset
			//todo move to extension method or copy component.
			_grabJoint.autoConfigureConnectedAnchor = jointPreset.autoConfigureConnectedAnchor;
			_grabJoint.enableCollision = jointPreset.enableCollision;
			_grabJoint.spring = jointPreset.spring;
			_grabJoint.damper = jointPreset.damper;
			_grabJoint.tolerance = jointPreset.tolerance;
			_grabJoint.minDistance = jointPreset.minDistance;
			_grabJoint.maxDistance = jointPreset.maxDistance;
			_grabJoint.breakForce = jointPreset.breakForce;
			_grabJoint.breakTorque = jointPreset.breakTorque;
			_grabJoint.enablePreprocessing = jointPreset.enablePreprocessing;
			_grabJoint.massScale = jointPreset.massScale;
			_grabJoint.connectedMassScale = jointPreset.connectedMassScale;
			
			//set grabbing.
			//this is player position
			_grabJoint.connectedAnchor = transform.localPosition;
			_grabJoint.anchor = grabbable.GetAnchorPosition(transform.position);
			_grabJoint.connectedBody = playerBody; //this should snap to our hands now.
			
			grabbable.Grabbed(this);
			_holdingGrabbable = grabbable;
		}

		public void Throw(Vector3 throwForce)
		{
			_holdingGrabbable.Released();
			Destroy(_grabJoint);
			_holdingGrabbable.Rigidbody.AddForce(throwForce,ForceMode.Impulse);
			_holdingGrabbable = null;
		}

		//so the grabbable gets the event for joints breaking, but we want to handle releasing
		//so it calls this, then we tell it "yeah okay you release". which, yes, is dumb.
		public void ForceRelease()
		{
			Destroy(_grabJoint);
			_holdingGrabbable.Released();
			_holdingGrabbable = null;
		}
	}
}