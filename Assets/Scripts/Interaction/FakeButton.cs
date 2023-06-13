using System;
using UnityEngine;

namespace Interaction
{
	public class FakeButton : MonoBehaviour
	{
		private bool pressed;
		private Vector3 home;

		private void Awake()
		{
			home = transform.position;
		}

		private void Update()
		{

		}

		private void OnTriggerEnter(Collider other)
		{
			pressed = true;
		}

		private void OnTriggerExit(Collider other)
		{
			//we don't actually know this.
			pressed = false;
		}
	}
	
}