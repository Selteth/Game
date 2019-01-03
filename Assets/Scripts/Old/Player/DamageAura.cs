using UnityEngine;

// Represents damage aura of the hero.
// Is increasing when hero is falling.
// The longer hero is falling the more damage enemies will take and
// the more distance is taking into account
public class DamageAura : MonoBehaviour
{
    [Header("Radius")]
    // Maximum radius of damage
    public float maxRadius;
    // The minimum radius that should exist
    // to create damage aura and affect other objects
    public float minRadius = 1f;
    // Radius step per fixed update
    public float radiusPerFUpdate;
    // Maximum push force applied to enemies
    [Header("Force")]
    public float maxPushForce;
    // Push force per fixed update
    public float forcePerFUpdate;
    
    // Radius counter.
    // Is increasing when hero is falling.
    // Cannot be greater than maxRadius
    private float radiusCounter = 0f;
    // Push force counter.
    // Is increasing when hero is falling.
    // Cannot be greater than maxPushForce
    private float pushForceCounter = 0f;
    // Player position
    private Transform playerTransofrm;
    // Player movement script
    private MovementControl playerMovement;
    // Whether hero is grounded
    private bool isGrounded = false;
    // Damage aura ground effect
    private ParticleSystem damageAuraEffect;

    void Awake()
    {
        playerTransofrm = GetComponent<Transform>();
        playerMovement = GetComponent<MovementControl>();
        damageAuraEffect = playerTransofrm.Find("Damage Aura Effect").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        isGrounded = playerMovement.IsGrounded();
    }

    void FixedUpdate()
    {
        IncreaseImpact();
        ApplyGroundDamage();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (playerMovement.IsFalling())
                collision.gameObject.GetComponent<Damage>().ApplyDamage(gameObject);
            else
                GetComponent<Damage>().ApplyDamage(collision.gameObject);
        }
    }

    private bool HasDamageAura()
    {
        return radiusCounter > minRadius;
    }

    private void IncreaseImpact()
    {
        // As long as player is falling...
        if (playerMovement.IsFalling())
        {
            /* ...increase his damage aura power */
            radiusCounter += radiusPerFUpdate;
            pushForceCounter += forcePerFUpdate;

            if (radiusCounter > maxRadius)
            {
                radiusCounter = maxRadius;
            }
            if (pushForceCounter > maxPushForce)
            {
                pushForceCounter = maxPushForce;
            }
        }
        else if (playerMovement.IsRising())
            ResetDamageAura();
    }

    private void ApplyGroundDamage()
    {
        if (isGrounded)
        {
            if (HasDamageAura())
            {
                // Find all enemies in given radius
                Collider2D[] enemies = Physics2D.OverlapCircleAll(playerTransofrm.position, radiusCounter, 1 << LayerMask.NameToLayer("Enemy"));
                // If there are any, show damage aura effect...
                if (enemies.Length != 0)
                {
                    ParticleSystem.MainModule main = damageAuraEffect.main;
                    main.startSize = radiusCounter * 3;
                    damageAuraEffect.Emit(1);

                    // ...and apply to them damage depending on their distance to the player
                    foreach (Collider2D enemy in enemies)
                    {
                        float distanceCoefficient = GetDistanceCoefficient(enemy);
                        float pushForce = pushForceCounter * (1 - distanceCoefficient);

                        PushEnemy(enemy, pushForce);

                        
                    }
                }
            }
            
            radiusCounter = 0f;
            pushForceCounter = 0f;
        }
    }

    // Returns ratio between player-enemy distance and auro radius distance.
    private float GetDistanceCoefficient(Collider2D enemy)
    {
        float distance = Vector2.Distance(playerTransofrm.position, enemy.transform.position);
        float distanceCoefficient = distance / radiusCounter;

        /* I DO NOT WHY BUT SOMETIMES RATIO IS GREATER THAN 1. WTF? */
        return distanceCoefficient > 1 ? 1 : distanceCoefficient;
    }

    // Pushes enemy with specified force
    private void PushEnemy(Collider2D enemy, float pushForce)
    {
        Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
        // Push direction
        float directionSign = Mathf.Sign(enemy.transform.position.x - playerTransofrm.position.x);
        enemyRigidbody.AddForce(Vector2.right * directionSign * pushForce);
    }

    private void ResetDamageAura()
    {
        radiusCounter = 0;
        pushForceCounter = 0;
    }

}
