using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Interface used to interact with objects
/// </summary>
public interface IInteractable
{
    /// <summary>
    ///     Whether the interactable can be interacted with
    /// </summary>
    /// <param name="other"> The game object that would interact with this object </param>
    /// <returns> Whether this object can be interacted with </returns>
    bool CanInteract(GameObject other);

    /// <summary>
    ///     Interface method for a generic object that can be interacted with
    /// </summary>
    /// <param name="other"> The game object that is triggring this interaction, should be used to handle the 
    ///     consequences of the interaction </param>
    /// <returns> true if successful, false if not </returns>
    bool Interact(GameObject other);

    /// <summary>
    ///     Interface method to handle being looked at by something intending to interact with this object
    /// </summary>
    /// <param name="other"> The game object that is looking at this object </param>
    void OnHighlightEnter(GameObject other);

    /// <summary>
    ///     Interface method to handle being looked away from by something intending to interact with this object
    /// </summary>
    /// <param name="other"> The game object that was looking at this object </param>
    void OnHighlightExit(GameObject other);
}
