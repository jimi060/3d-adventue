using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealable
{
    /// <summary>
    ///     The maximum health to have, is assigned to CurrentHealth at Start
    /// </summary>
    public int MaxHealth;

    /// <summary>
    ///     The current health, if it gets to zero, death
    /// </summary>
    public int CurrentHealth;

    /// <summary>
    ///     Reference to the currently equipped gear, used to calculate resistances
    /// </summary>
    public Equipped Equipped;

    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    /// <summary>
    ///     Receive damage, toned down by equipment resistances
    /// </summary>
    /// <param name="damage"> The raw, typed damage to recieve </param>
    /// <param name="source"> The source responsible for the damage </param>
    /// <returns> whether the damage was handled effectively </returns>
    public bool ReceiveDamage(Damage damage, GameObject source)
    {
        var resistance = Equipped.GetResistance(damage.Type);

        CurrentHealth -= (damage.Value - resistance);

        if(CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        
        return true;
    }

    public bool ReceiveHealth(int amount, GameObject source)
    {
        CurrentHealth += amount;
       
        if(CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        return true;
    }
}
