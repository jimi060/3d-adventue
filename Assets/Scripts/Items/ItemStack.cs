using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Class to store a stack of material.
/// </summary>
[System.Serializable]
public class ItemStack
{
    /// <summary>
    ///     The material that this is a stack of
    /// </summary>
    public Item Item;

    /// <summary>
    ///     The number of materials in this stack
    /// </summary>
    public int Count;

    public ItemStack(Item item, int count)
    {
        Item = item;
        Count = count;
    }
}
