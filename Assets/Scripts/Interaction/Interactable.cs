using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    //Tracks players interacting with it.
    public Action OnInteractStart;
    public Action OnInteractStop;

    public bool CanBeDirectInteracted => _canBeDirectInteracted;
    
    
    [Header("Interaction Configuration")] 
    private bool _canBeDirectInteracted;//can you interact with this by pressing a button?
    public UnityEvent OnInteractionStartEvent;
    public UnityEvent OnInteractionStopEvent;

    private bool _interacting;

    public void SetInteracting(bool interacting)
    {
        if (interacting != _interacting)
        {
            _interacting = interacting;
            if (_interacting)
            {
                StartInteraction();
            }
            else
            {
                StartInteraction();
            }
        }
    }

    private void StartInteraction()
    {
        OnInteractStart?.Invoke();
        OnInteractionStartEvent.Invoke();
    }

    private void StopInteraction()
    {
        OnInteractStop?.Invoke();
        OnInteractionStopEvent.Invoke();
    }

    //player pressed the button.
    public void DirectInteract()
    {
        if (_canBeDirectInteracted)
        {
            StartInteraction();
            StopInteraction();
        }
    }
}
