using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interaction;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractionZone : MonoBehaviour
{
    private Collider _interactionTrigger;
    private PlayerInteractionHandler _player;//injected.
    private List<Interactable> _interactables = new List<Interactable>();
    
    private void Awake()
    {
        _interactionTrigger = GetComponent<Collider>();
        if (!_interactionTrigger.isTrigger)
        {
            Debug.LogWarning("interaction zone must be a trigger. Setting now.");
            _interactionTrigger.isTrigger = true;
        }
    }

    public bool AnyInteractables()
    {
        return _interactables.Count > 0;
    }

    public bool TryGetClosestInteractableOfType<T>(out T interactable, bool forceSort = false) where T : Interactable
    {
        if (forceSort)
        {
            SortInteractables();
        }
        
        if (_interactables.Count > 0)
        {
            foreach (var i in _interactables)
            {
                if (i is T j)//if eye is tee jay is nonsense and i am sorry for writing it.
                {
                    interactable = j;
                    return true;
                }
            }
        }

        interactable = null;
        return false;
    }

    public bool GetAllInteractablesOfType<T>(out List<T> interactables) where T : Interactable
    {
        interactables = _interactables.Cast<T>().ToList();
        return interactables.Count > 0;
    }

    public bool TryGetClosestInteractable(out Interactable interactable)
    {
        if (_interactables.Count > 0)
        {
            interactable = _interactables[0];
            return true;
        }

        interactable = null;
        return false;
    }
    /// <summary>
    /// Reorders the internal list so that the interactable closest to the player is closest.
    /// This way, getting first interactable returns the closest one.
    /// </summary>
    private void SortInteractables()
    {
        if (_interactables.Count > 1)//which it usually won't be.
        {
            _interactables = _interactables.OrderBy(x => Vector3.Distance(x.transform.position, _player.transform.position)).ToList();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            if (!_interactables.Contains(interactable))
            {
                _interactables.Add(interactable);
                SortInteractables();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            if (_interactables.Contains(interactable))
            {
                _interactables.Remove(interactable);
                SortInteractables();
            }
        }
    }

    public void SetPlayer(PlayerInteractionHandler player)
    {
        _player = player;
    }
}
