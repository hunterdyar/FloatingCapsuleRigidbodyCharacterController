using System;
using System.Collections;
using System.Collections.Generic;
using Interaction;
using UnityEngine;

public class Grabbable : Interactable
{
	public Rigidbody Rigidbody => _rigidbody;
	private Rigidbody _rigidbody;

	public GrabHandler GrabHandler => _grabHandler;
	private GrabHandler _grabHandler;
	
	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}
	
	public void Grabbed(GrabHandler grabHandler)
	{
		_grabHandler = grabHandler;
		SetInteracting(true);
	}

	public void Released()
	{
		_grabHandler = null;
		SetInteracting(false);
	}

	private void OnJointBreak(float breakForce)
	{
		if (_grabHandler != null)
		{
			_grabHandler.ForceRelease();
		}
	}

	public Vector3 GetAnchorPosition(Vector3 grabPosition)
	{
		var c = GetComponent<Collider>();
		return transform.InverseTransformPoint(c.ClosestPoint(grabPosition));
	}
}
