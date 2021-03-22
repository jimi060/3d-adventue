using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Base class for storing crafting recepie information.
/// </summary>
[CreateAssetMenu]
[System.Serializable]
public class Recepie: ScriptableObject
{
    /// <summary>
    ///     The Item that this recepie produces
    /// </summary>
    public Item Produces;

    /// <summary>
    ///     List of the items that this recepie requires in order to craft
    /// </summary>
    public List<ItemStack> RequiredItems = new List<ItemStack>();
}
