using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Base class for storing Item information
/// </summary>
public class Item : ScriptableObject
{
    /// <summary>
    ///     The reference used to uniquely identify this Item
    /// </summary>
    public string UniqueReference;

    /// <summary>
    ///     Whether the item can be stacked
    /// </summary>
    public bool Stackable;
}
