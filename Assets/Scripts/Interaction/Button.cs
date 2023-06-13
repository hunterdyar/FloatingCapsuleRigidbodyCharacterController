using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Button : MonoBehaviour
{
    [SerializeField] private SpringJoint _buttonJoint;
    public float pressDistance = 0.2f;
    public bool pressed;
    private void Awake()
    {
        _buttonJoint = GetComponentInChildren<SpringJoint>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        var offset = (_buttonJoint.anchor - _buttonJoint.transform.localPosition).magnitude;
        pressed =  offset >= pressDistance;
    }
}
