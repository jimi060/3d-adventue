using System.Collections.Generic;
using UnityEngine;

public class BasicInteraction : MonoBehaviour
{

    public List<GameObject> InteractablesInRange;

    public GameObject ActiveInteractable;

    // Update is called once per frame
    void Update()
    {
        GameObject interactableToSet = null;

        // Go though interactibles and pick closest
        foreach(var interactable in InteractablesInRange)
        {
            if(ActiveInteractable == null)
            {
                interactableToSet = interactable;
            }
            else
            {
                // If the interactable is closer to the player than the active interactable
                if(Vector3.Distance(interactable.transform.position, gameObject.transform.position) <=
                    Vector3.Distance(ActiveInteractable.transform.position, gameObject.transform.position))
                {
                    interactableToSet = interactable;
                }
            }
        } 

        // Tell the active interactible if we stopped looking at it, or if we started
        if(interactableToSet != ActiveInteractable)
        {
            if (ActiveInteractable != null)
            {
                ActiveInteractable.GetComponent<IInteractable>().OnHighlightExit(this.gameObject);
            }

            if (interactableToSet != null)
            {
                interactableToSet.GetComponent<IInteractable>().OnHighlightEnter(this.gameObject);
            }

            ActiveInteractable = interactableToSet;
        }

        // Interact with the interactable
        if(Input.GetKeyDown(KeyCode.F) && ActiveInteractable != null)
        {
            var interactable = ActiveInteractable.GetComponent<IInteractable>();

            if(interactable != null)
            {
                interactable.Interact(this.gameObject);

                // If the interactable turned itself off, remove from the list
                if(!interactable.CanInteract(this.gameObject))
                {
                    InteractablesInRange.Remove(ActiveInteractable);
                    ActiveInteractable = null;
                }
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        var interactable = other.gameObject.GetComponent<IInteractable>();
        if(interactable != null)
        {
            if(interactable.CanInteract(this.gameObject))
            {
                InteractablesInRange.Add(other.gameObject);
            }            
        }
    }

    void OnTriggerExit(Collider other)
    {
        var interactable = other.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            InteractablesInRange.Remove(other.gameObject);
        }
    }
}
