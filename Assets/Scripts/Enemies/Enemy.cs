using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Base class for enemy NPC's
/// </summary>
public class Enemy : MonoBehaviour, IDamageable, IHealable
{
    // Public

    /// <summary>
    ///     The active health of the Enemy
    /// </summary>
    public int Health;

    /// <summary>
    ///     The Maximum health this Enemy can have
    /// </summary>
    public int MaxHealth;

    /// <summary>
    ///     The type of damage that gets resisted
    /// </summary>
    public DamageType ResistanceType;

    /// <summary>
    ///     The type that this is weak to.
    /// </summary>
    public DamageType WeaknessType;

    /// <summary>
    ///     The amount of damage dealt
    /// </summary>
    public int Damage;

    /// <summary>
    ///     The type of damage that is dealt
    /// </summary>
    public DamageType DamageType;

    /// <summary>
    ///     The loot to drop when this Enemy is defeated
    /// </summary>
    public List<ItemStack> Drops = new List<ItemStack>();

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    /// <summary>
    ///     Base method to call in order to run any "on death" routines.
    /// </summary>
    /// <param name="source"> The GameObject that has "killed" this object </param>
    public void Die(GameObject source)
    {
        var inventory = source.GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.LogError(string.Format("Failed to find an inventory attached to {0}", source.name));
        }

        inventory.Add(Drops);
        gameObject.SetActive(false);
    }

    /// <summary>
    ///     Base method to deal damage to this Enemy. Will just decrease health and call the base Die method.
    /// </summary>
    /// <param name="damage"> The damage to deal </param>
    /// <param name="source"> The GameObject dealing damage to this object </param>
    /// <returns> true if damage was dealt successfully, false if not </returns>
    public bool ReceiveDamage(Damage damage, GameObject source)
    {
        // Apply weakness/ resistances to incoming damage
        if(damage.Type == WeaknessType)
        {
            Health -= damage.Value * 2;
        }
        else if(damage.Type == ResistanceType)
        {
            Health -= damage.Value / 2;
        }
        else
        {
            Health -= damage.Value;
        }

        if (Health <= 0)
        {
            Die(source);
        }

        return true;
    }

    /// <summary>
    ///     Base method to heal this Enemy. Will increase health up to the maximum health value, provided the source
    ///     is not tagged as the player.
    /// </summary>
    /// <param name="amount"> How much health to heal </param>
    /// <param name="source"> The GameObject healing this object </param>
    /// <returns> true if health was restored successfully, false if not </returns>
    public bool ReceiveHealth(int amount, GameObject source)
    {
        if(source.tag != "Player")
        {
            Health += amount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }

        return true;
    }
}
