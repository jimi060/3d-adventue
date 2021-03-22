using UnityEngine;

/// <summary>
///     Interface used to heal objects
/// </summary>
public interface IHealable
{
    /// <summary>
    ///     Interface method for healing damage to an object
    /// </summary>
    /// <param name="amount"> The amount of damage to heal </param>
    /// <param name="source"> The gameobject that can be considered the "source" of the healing </param>
    /// <returns> true if the damage dealing was successfully handled, false if not </returns>
    public bool ReceiveHealth(int amount, GameObject source);
}
