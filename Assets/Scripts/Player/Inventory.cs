using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Class to handle storing items in an inventory and performing crafting operations.
/// </summary>
public class Inventory : MonoBehaviour
{
    /// <summary>
    ///     List to store stacks of materials, used to craft items or other materials
    /// </summary>
    public List<ItemStack> ItemList = new List<ItemStack>();

    /// <summary>
    ///     Maximum number of items a stack can have
    /// </summary>
    public int MaxStack = 999;


    /// <summary>
    ///     Reset the inventory instance
    /// </summary>
    public void ResetInventory()
    {
        ItemList = new List<ItemStack>();
    }

    /// <summary>
    ///     Add a list item stacks to the inventory
    /// </summary>
    /// <param name="itemStackList"> A list of item stacks to be added to the inventory </param>
    /// <returns> true if the items were successfully added, false if not </returns>
    public bool Add(List<ItemStack> itemStackList)
    {
        foreach (var itemStack in itemStackList)
        {
            if (!Add(itemStack))
            {
                Debug.LogError(string.Format("Tried to add a stack of {0} {1} to the player inventory, but failed",
                    itemStack.Count, itemStack.Item));
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Add a singular item stack to the inventory
    /// </summary>
    /// <param name="itemStack"> The item stack to add to the inventory </param>
    /// <returns> true if the items were successfully added, false if not </returns>
    public bool Add(ItemStack itemStack)
    {
        for (var i = 0; i < itemStack.Count; i++)
        {
            if (!Add(itemStack.Item))
            {
                Debug.LogError(string.Format("Tried to add {0} to the player inventory, but failed",
                    itemStack.Item));
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Add an Item to the inventory
    /// </summary>
    /// <param name="item"> The item to add </param>
    /// <returns> true if item could be added, false if not </returns>
    public bool Add(Item item)
    {
        if(item.Stackable)
        {
            var materialStackEntry = Find(item);

            if (materialStackEntry == null)
            {
                ItemList.Add(new ItemStack(item, 1));
            }
            else
            {
                if (materialStackEntry.Count == MaxStack)
                {
                    // Item count is already at the maximum allowed
                    Debug.LogWarning(string.Format("Tried to add item: {0} to inventory, but its stack was {1}",
                        item.UniqueReference, MaxStack));
                    return false;
                }
                else
                {
                    materialStackEntry.Count += 1;
                }
            }

            return true;
        }
        else
        {
            ItemList.Add(new ItemStack(item, 1));
            return true;
        }        
    }

    /// <summary>
    ///     Remove an item from the inventory
    /// </summary>
    /// <param name="item"> The item to remove </param>
    /// <returns> true if item could be removed, false if not </returns>
    public bool Remove(Item item)
    {
        var itemStackEntry = Find(item);

        if (item.Stackable)
        {
            if (itemStackEntry != null)
            {

                if (itemStackEntry.Count > 0)
                {
                    itemStackEntry.Count -= 1;

                    if (itemStackEntry.Count == 0)
                    {
                        ItemList.Remove(itemStackEntry);
                    }
                }
                else
                {
                    // Item is zero
                    Debug.LogWarning(string.Format("Failed to remove item: {0} from inventory - it had a stack of 0",
                        item.UniqueReference));
                    return false;
                }
            }
            else
            {
                // Item does not exist in dict
                Debug.LogWarning(string.Format("Failed to remove item: {0} from inventory - didn't exist",
                    item.UniqueReference));
                return false;
            }

            return true;
        }
        else
        {
            if (!ItemList.Remove(itemStackEntry))
            {
                Debug.LogWarning(string.Format("Failed to remove item: {0} from inventory", item));
                return false;
            }

            return true;
        }    
    }

    /// <summary>
    ///     Looks through the inventory to find if a specified item exists in a stack already
    /// </summary>
    /// <param name="material"> The item to look for </param>
    /// <returns> A reference to the stack in which the item exists, will be null if it doesn't exist </returns>
    public ItemStack Find(Item item)
    {
        foreach (var itemStack in ItemList)
        {
            if (itemStack.Item == item)
            {
               return itemStack;
            }
        }

        return null;
    }

    /// <summary>
    ///     Check whether the given recepie can be crafted by the inventory
    /// </summary>
    /// <param name="recepie"> The recepie to check </param>
    /// <returns> true if the recepie can be crafted, false if not </returns>
    public bool CanCraft(Recepie recepie)
    {
        foreach(var requiredItem in recepie.RequiredItems)
        {
            var materialStackEntry = Find(requiredItem.Item);

            if (materialStackEntry == null)
            {
                // Item isn't in inventory at all, can't craft
                Debug.LogWarning(string.Format("Can't craft {0}, {1} wasn't present at all",
                    recepie.GetType().FullName, requiredItem.Item.GetType().FullName));
                return false;
            }

            if (materialStackEntry.Count < requiredItem.Count)
            {
                // Don't have enough of that item to craft
                Debug.LogWarning(string.Format("Can't craft {0}, didnt have enough of {1}",
                    recepie.GetType().FullName, requiredItem.Item.GetType().FullName));
                return false;
            }

        }

        return true;
    }

    /// <summary>
    ///     Attempt to craft the given recepie
    /// </summary>
    /// <param name="recepie"> The recepie to craft </param>
    /// <returns> true if the recpie was crafted successfully, false if not </returns>
    public bool Craft(Recepie recepie)
    {
        if(!CanCraft(recepie))
        {
            Debug.LogWarning(string.Format("Tried to craft {0} but check failed", recepie.Produces.GetType().FullName));
            return false;
        }

        foreach(var requiredItem in recepie.RequiredItems)
        {
            for (int i = 0; i < requiredItem.Count; i++)
            {

                if (!Remove(requiredItem.Item))
                {
                    // Something bad happened
                    Debug.LogWarning(string.Format("While trying to craft {0}, failed to remove one {1} from inventory",
                        recepie.GetType().FullName, requiredItem.Item.GetType().FullName));
                    return false;
                }
            }
        }

        Add(recepie.Produces);
        return true;        
    }

    /// <summary>
    ///     Debug method to print out the contents of the inventory
    /// </summary>
    public void PrintContents()
    {
        string logString = "--- Inventory Contents ---\n";
        logString += "Reference \t\t Type \t\t Count\n";
        foreach(var itemStack in ItemList)
        {
            logString += string.Format("{0} \t\t {1} \t\t {2}\n",
                itemStack.Item.UniqueReference, itemStack.Item.GetType(), itemStack.Count);
        }

        Debug.Log(logString);

    }
}
