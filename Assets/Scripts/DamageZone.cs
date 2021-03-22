using UnityEngine;

/// <summary>
///     Behaviour for a gameobject with an attacked trigger in order to deal damage to other objects
/// </summary>
public class DamageZone : MonoBehaviour
{
    // Public

    /// <summary>
    ///     The amount of damage to deal to objects in the damage zone
    /// </summary>
    public Damage Damage;

    /// <summary>
    ///     An alternate source gameobject to use instead of this one
    /// </summary>
    public GameObject AlternateSource;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has a IDamageable interface and deal damage via it
        var damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.ReceiveDamage(Damage, AlternateSource == null ? gameObject : AlternateSource);
        }
    }
}
