using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipped : MonoBehaviour
{
    /// <summary>
    ///     The equipped helmet
    /// </summary>
    public Armour HeadSlot;

    /// <summary>
    ///     The equipped body armour
    /// </summary>
    public Armour BodySlot;

    /// <summary>
    ///     The equipped leg armour
    /// </summary>
    public Armour LegSlot;

    /// <summary>
    ///     The equipped shield
    /// </summary>
    public Armour ShieldSlot;

    /// <summary>
    ///     The equipped weapon
    /// </summary>
    public Weapon WeaponSlot;

    /// <summary>
    ///     Equip an armour piece. Picks the right slot depending on armour type.
    /// </summary>
    /// <param name="armour"> The armour to equip </param>
    public void EquipArmour(Armour armour)
    {
        switch(armour.ArmourType)
        {
            case ArmourType.HEAD: HeadSlot = armour; break;
            case ArmourType.BODY: BodySlot = armour; break;
            case ArmourType.LEG: LegSlot = armour; break;
            case ArmourType.SHIELD: ShieldSlot = armour; break;
        }
    }

    /// <summary>
    ///     Equip a weapon
    /// </summary>
    /// <param name="weapon"> The weapon to equip </param>
    public void EquipWeapon(Weapon weapon)
    {
        WeaponSlot = weapon;
    }

    /// <summary>
    ///     Remove an armour type
    /// </summary>
    /// <param name="armourType"> The type of armour to remove </param>
    public void UnequipArmour(ArmourType armourType)
    {
        switch (armourType)
        {
            case ArmourType.HEAD: HeadSlot = null; break;
            case ArmourType.BODY: BodySlot = null; break;
            case ArmourType.LEG: LegSlot = null; break;
            case ArmourType.SHIELD: ShieldSlot = null; break;
        }
    }

    /// <summary>
    ///     Remove the equipped weapon
    /// </summary>
    public void UnequipWeapon()
    {
        WeaponSlot = null;
    }

    /// <summary>
    ///     Get the cumulative resistances for a given type
    /// </summary>
    /// <param name="type"> The type to get resistances for </param>
    /// <returns> The total resistance value added up among equipped armour </returns>
    public int GetResistance(DamageType type)
    {
        int resistance = 0;
        foreach(Armour armour in new List<Armour> { HeadSlot, BodySlot, LegSlot, ShieldSlot })
        {
            if(armour != null && armour.ResistanceType == type)
            {
                resistance += armour.Resistance;
            }
        }

        return resistance;
    }

    public Damage GetDamage()
    {
        if(WeaponSlot == null)
        {
            return new Damage { Value = 1, Type = DamageType.GENERIC };
        }
        else
        {
            return new Damage { Value = WeaponSlot.Damage, Type = WeaponSlot.DamageType };
        }
    }
}
