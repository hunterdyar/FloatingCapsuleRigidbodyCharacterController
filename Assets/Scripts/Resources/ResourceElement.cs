using System;
using UnityEngine;

namespace Resources
{
	public class ResourceElement : MonoBehaviour
	{
		[SerializeField] private int count = 1;
		[SerializeField] private ShipResource _resource;

		public int Count => count;
		public ShipResource Resource => _resource;
	}
}