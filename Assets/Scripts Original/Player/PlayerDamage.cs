using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : Damage
{
    // Point where the player will be respawned after death
    public Vector2 startPoint;
    // Speed the player will be pushed with after getting damaged
    public float damagePushForce = 1000f;

    private Rigidbody2D playerRigidbody;

    new void Awake()
    {
        base.Awake();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void Die(GameObject damager)
    {
        Die();
    }

    private void Die()
    {
        Respawn();
    }

    protected override void RespondToDamage(GameObject damager)
    {
        float positionDifference = gameObject.transform.position.x - damager.transform.position.x;
        float direction = Mathf.Sign(positionDifference);
        playerRigidbody.AddForce(new Vector2(damagePushForce * direction, playerRigidbody.velocity.y));
    }

    private void Respawn()
    {
        gameObject.transform.Translate(transform.InverseTransformPoint(startPoint));
        currentLifeCount = lifeCount;
    }


    public void OnTriggerExit2D(Collider2D collider)
    {

        if (collider.tag == "LevelBound")
            Die();
    }
}
