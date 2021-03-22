using UnityEngine;

/// <summary>
///     Base class for a gameobject that can be broken via damage
/// </summary>
public class Breakable : MonoBehaviour, IDamageable
{
    // Public

    /// <summary>
    ///     How much damage this object can take before breaking
    /// </summary>
    public int Health = 10;

    /// <summary>
    ///     The cooldown in seconds before this object can be damaged again
    /// </summary>
    public float Cooldown = 1;

    // Private

    /// <summary>
    ///     Whether the damage cooldown has passed and this object can be damaged
    /// </summary>
    private bool Damageable = true;

    /// <summary>
    ///     The timestamp after which this object can be damaged 
    /// </summary>
    private float cooldownEnd;

    // Start is called before the first frame update
    void Start()
    {
        cooldownEnd = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // If the cooldown has passed, this object can be damaged
        if(cooldownEnd <= Time.time)
        {
            Damageable = true;
        }
    }

    /// <summary>
    ///     Implementation of the IDamageable interface. Takes damage if the damage cooldown has passed, and destroys 
    ///     this gameobject if Health is re
    /// </summary>
    /// <param name="damage"> The damage to deal to this object </param>
    /// <param name="source"> The gameobject dealing damage to this object </param>
    /// <returns> true if damage was successfully dealt, false if not </returns>
    public bool ReceiveDamage(Damage damage, GameObject source)
    {
        if(Damageable)
        {
            Health -= damage.Value;
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                Damageable = false;
                cooldownEnd = Time.time + Cooldown;
            }
        }

        return true;
    }
}
