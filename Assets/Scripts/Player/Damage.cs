using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Class to hold the nature of damage being dealt
/// </summary>
[System.Serializable]
public class Damage
{
    /// <summary>
    ///     The raw damage value, absent of any resistences applied
    /// </summary>
    public int Value;

    /// <summary>
    ///     The type of the damage to deal
    /// </summary>
    public DamageType Type;
}
