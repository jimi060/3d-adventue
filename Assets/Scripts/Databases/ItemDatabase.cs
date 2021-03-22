using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDatabase
{
    public List<Item> itemList = new List<Item>();

    public ItemDatabase()
    {
        // Searches assembly for any classes extending the Item class, instantiates and adds them to the item
        // database
        foreach (var itemClass in AppDomain.CurrentDomain.GetAssemblies()
                                         .SelectMany(assembly => assembly.GetTypes())
                                         .Where(type => type.IsSubclassOf(typeof(Item))))
        {
            itemList.Add((Item)Activator.CreateInstance(itemClass));
        }
    }

    /// <summary>
    ///     Retrieve an instantiated item that matches the provided item type. It is expected that the item type is a
    ///     class that inherits from "Item"
    /// </summary>
    /// <param name="itemType"> The Item type to find an instantiated class for </param>
    /// <returns> 
    ///     The instanitated Item object that has a matching type to the type given. Null if the type isn't 
    ///     valid or the item doesn't exist. 
    /// </returns>
    public Item GetItem(Type itemType)
    {
        if(!itemType.IsSubclassOf(typeof(Item)))
        {
            Debug.LogWarning("Tried to get an item of type " + itemType.FullName + " but it doesn't subclass Item");
            return null;
        }

        return itemList.FirstOrDefault(item => item.GetType() == itemType);
    }

}
