using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Base class for storing material information
/// </summary>
[CreateAssetMenu]
public class CraftingMaterial : Item
{
    /// <summary>
    ///     How rare the material is
    /// </summary>
    public Rarity Rarity;

    public CraftingMaterial()
    {
        Stackable = true;
    }
}