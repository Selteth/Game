﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : Damage
{
    // Impulse speed after killing enemy
    public float killImpulseSpeed = 10f;

    new void Awake()
    {
        base.Awake();
    }

    protected override void Die(GameObject killer)
    {
        Die();
        //Destroy(gameObject);
        PushGameObject(killer);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    protected override void RespondToDamage(GameObject damager)
    {
        PushGameObject(damager);
        // TODO. Freeze enemy if enemy is alive
    }

    private void PushGameObject(GameObject gameObject)
    {
        Rigidbody2D gameObjectRigidbody = gameObject.GetComponent<Rigidbody2D>();
        gameObjectRigidbody.velocity = new Vector2(gameObjectRigidbody.velocity.x, killImpulseSpeed);
    }


    public void OnTriggerExit2D(Collider2D collider)
    {

        if (collider.tag == "LevelBound")
            Die();
    }
}
