using UnityEngine;

/// <summary>
///     Interface for objects that can be dealt damage
/// </summary>
public interface IDamageable
{
    /// <summary>
    ///     Interface method for dealing damage to an object
    /// </summary>
    /// <param name="damage"> The raw damage to deal to an object </param>
    /// <param name="source"> The gameobject that can be considered the "source" of the damage </param>
    /// <returns> true if the damage dealing was successfully handled, false if not </returns>
    public bool ReceiveDamage(Damage damage, GameObject source);
}
