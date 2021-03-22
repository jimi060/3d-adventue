using UnityEngine;

/// <summary>
///     A basic attack behaviour, will activate a target gameobject for a time, which should handle triggers and 
///     damage dealing
/// </summary>
public class BasicAttack : MonoBehaviour
{
    // Public

    /// <summary>
    ///     The equipped items
    /// </summary>
    public Equipped Equipped;
    
    /// <summary>
    ///     The gameobject to activate and detect collisions
    /// </summary>
    public Hitbox Hitbox;

    /// <summary>
    ///     How long for the attack period to last, in seconds
    /// </summary>
    public float AttackPeriod = 1;

    /// <summary>
    ///     Whether an attack is currently being carried out
    /// </summary>
    public bool IsAttacking = false;

    // Private

    /// <summary>
    ///     The timestamp for when the next attack is allowed 
    /// </summary>
    private float nextAttack = 0;

    private void Awake()
    {
        Hitbox.CollideWithObject += (Collider other) => AttackHit(other);
    }

    // Start is called before the first frame update
    void Start()
    {
        nextAttack = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // Set the damage zone to be disabled if the attack period has passed
        if (Time.time >= nextAttack)
        {
            IsAttacking = false;
            Hitbox.StopListening();
        }

        // If the player is attacking and the damage zone is not active, activate the damage zone and start the timeout
        if (Input.GetKeyDown(KeyCode.Mouse0) && IsAttacking == false)
        {
            IsAttacking = true;
            Hitbox.StartListening();
            nextAttack = Time.time + AttackPeriod;
        }
    }

    void AttackHit(Collider other)
    {
        var damageable = other.gameObject.GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.ReceiveDamage(Equipped.GetDamage(), gameObject);
        }
    }
}
