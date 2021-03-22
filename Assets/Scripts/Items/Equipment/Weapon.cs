using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Class for Weapon information
/// </summary>
[CreateAssetMenu]
public class Weapon : Equipment
{
    /// <summary>
    ///     The damage value for this weapon
    /// </summary>
    public int Damage;

    /// <summary>
    ///     The kind of damage this weapon deals
    /// </summary>
    public DamageType DamageType;

    /// <summary>
    ///     The type of weapon this is, categorizes how it works
    /// </summary>
    public WeaponType WeaponType;
}
