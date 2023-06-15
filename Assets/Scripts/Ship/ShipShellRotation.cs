using System;
using System.Diagnostics;
using UnityEngine;

namespace Ship
{
	public class ShipShellRotation : MonoBehaviour
	{
		[SerializeField] private Interactable ClockwiseInput;
		[SerializeField] private Interactable CounterClockwiseInput;

		public float currentTarget;
		[SerializeField] private float rotationSpeed;
		[SerializeField] private float degreeSnap;

		void Start()
		{
			currentTarget = GetCurrentRotationAsDegrees();
		}

		public float GetCurrentRotationAsDegrees()
		{
			var degrees = transform.eulerAngles.y;
			degrees = degrees.RoundAndNormalizeDegrees360();
			return degrees;
		}
		public void Update()
		{
			float diff = GetCurrentRotationAsDegrees() - currentTarget;
			if (Mathf.Abs(diff)>0.5f)//shouldnt be too needed because we round both.
			{
				//rotate
				transform.Rotate(0,rotationSpeed*-Mathf.Sign(diff)*Time.deltaTime,0);
			}
			else
			{
				//check buttons
				int cw = ClockwiseInput.Interacting ? 1 : 0;
				int ccw = CounterClockwiseInput.Interacting ? -1 : 0;
				int direction = cw + ccw;//0 when neither or both, 1 or -1 when just one.
				currentTarget = currentTarget + degreeSnap*direction;
				currentTarget = currentTarget.RoundAndNormalizeDegrees360();
				
			}
		}
	}
}