using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Class to store and handle Hitboxes and hitbox data
/// </summary>
public class Hitbox : MonoBehaviour
{
    /// <summary>
    ///     Event handler definition for Hitbox collisions
    /// </summary>
    /// <param name="other"> The other collider that was collided with </param>
    public delegate void EventHandler(Collider other);

    /// <summary>
    ///     The collision event handler to subscribe to for collision events
    /// </summary>
    public event EventHandler CollideWithObject;

    /// <summary>
    ///     The colider this hitbox is meant to handle
    /// </summary>
    public Collider Collider;

    public void Start()
    {
        Collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CollideWithObject(other);
    }

    /// <summary>
    ///     Start listening for collisions
    /// </summary>
    public void StartListening()
    {
        Collider.enabled = true;
    }

    /// <summary>
    ///     Stop listening for collisions
    /// </summary>
    public void StopListening()
    {
        Collider.enabled = false;
    }
}
