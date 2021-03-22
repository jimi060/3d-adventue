using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Base class for objects designed to hold items and produce them on being interacted with
/// </summary>
public class Container : MonoBehaviour, IInteractable
{
    // Public

    /// <summary>
    ///     The items stored within this container
    /// </summary>
    public List<ItemStack> Contents;

    public bool Opened = false;

    public GameObject Highlighter;

    void Start()
    {
        Highlighter.SetActive(false);
    }

    /// <summary>
    ///     Give the interacting object, presumed to be the player, the contents of this container
    /// </summary>
    /// <param name="player"> gameobject interacting with this container </param>
    /// <returns> true if the interaction was handle successfully, false if not </returns>
    public bool Interact(GameObject source)
    {
        if(!Opened)
        {
            var inventory = source.GetComponent<Inventory>();
            if (inventory == null)
            {
                Debug.LogError("Container tried to add an item to an inventory, but failed");
                return false;
            }

            var addResponse = inventory.Add(Contents);
            Opened = true;
            Highlighter.SetActive(false);

            return addResponse;
        }

        return false;
    }

    /// <summary>
    ///     Whether this item can be interacted with
    /// </summary>
    /// <param name="source"> The Object that wants to interact with this </param>
    /// <returns> true if is interactable, false if not </returns>
    public bool CanInteract(GameObject source)
    {
        return !Opened;
    }

    /// <summary>
    ///     Activate the highlighter object when this item is up for interaction
    /// </summary>
    /// <param name="other"> The player gameobject </param>
    public void OnHighlightEnter(GameObject other)
    {
        if(CanInteract(other))
        {
            Highlighter.SetActive(true);
        }        
    }

    /// <summary>
    ///     Deactivate the highlighter object when the item is no longer up for interaction
    /// </summary>
    /// <param name="other"> The player gameobject </param>
    public void OnHighlightExit(GameObject other)
    {
        if(CanInteract(other))
        {
            Highlighter.SetActive(false);
        }        
    }
}
