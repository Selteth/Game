using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents base class for components that have
// lives and might get damaged
public abstract class Damage : MonoBehaviour
{
    // Count of lives at the beginning
    public int lifeCount;
    // Time the object is immortal after getting damaged
    public float immortalTime;
    
    protected int currentLifeCount;
    protected float immortalTimeCounter = 0f;

    protected void Awake()
    {
        currentLifeCount = lifeCount;
    }

    void Update()
    {
        if (IsImmortal())
            immortalTimeCounter -= Time.deltaTime;
    }

    // Applies damage to the object
    public void ApplyDamage(GameObject damager)
    {
        if (!IsImmortal())
        {
            --currentLifeCount;
            if (currentLifeCount <= 0)
            {
                Debug.Log("I am dying...");

                Die(damager);
            }
            else
            {
                Debug.Log("I am damaged!");

                RespondToDamage(damager);
                immortalTimeCounter = immortalTime;
            }
        }
        else
            Debug.Log("I am immortal!");
    }

    private bool IsImmortal()
    {
        return immortalTimeCounter > 0;
    }

    protected abstract void Die(GameObject killer);
    protected abstract void RespondToDamage(GameObject damager);

}
