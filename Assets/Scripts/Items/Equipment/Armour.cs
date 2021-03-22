using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Class for Armour information
/// </summary>
[CreateAssetMenu]
public class Armour : Equipment
{
    /// <summary>
    ///     The resistance value for this armour
    /// </summary>
    public int Resistance;

    /// <summary>
    ///     The kind of damage this armour resists
    /// </summary>
    public DamageType ResistanceType;

    /// <summary>
    ///     The type of armour this is, governs which slot it goes into
    /// </summary>
    public ArmourType ArmourType;
}
