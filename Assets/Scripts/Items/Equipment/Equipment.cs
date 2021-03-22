using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Base class for storing Equipment information
/// </summary>
public class Equipment : Item
{   
    /// <summary>
    ///     The "level" of the item
    /// </summary>
    public int Level;
    
    public Equipment()
    {
        Stackable = false;
    }
}
