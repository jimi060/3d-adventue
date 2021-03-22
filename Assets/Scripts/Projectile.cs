using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Lifetime = 1f;
    public Vector3 Direction;
    public float Speed = 1;
    [SerializeField]
    public Damage damage;
    public DamageType DamageType;

    private float deathTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        deathTime = Time.time + Lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Direction * Speed * Time.deltaTime);

        if(Time.time >= deathTime)
        {
            Die();
        }
    }
    
    public void Configure(float lifetime, Vector3 direction, int speed)
    {
        Lifetime = lifetime;
        Direction = direction;
        Speed = speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var damageComponent = collision.gameObject.GetComponent<IDamageable>();
        if (damageComponent != null)
        {
            damageComponent.ReceiveDamage(damage, gameObject);
        }
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
