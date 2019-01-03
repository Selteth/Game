using System.Collections;
using UnityEngine;

public enum SkillStateOld
{
    Ready, Casting, Activated, Cooldown
}

// Represents player skill base behaviour
public abstract class SkillOld : MonoBehaviour, ISkillOld {
    
    // Time during which player cannot activate the skill
    protected float cooldown;
    // Skill state
    protected SkillStateOld state;

    protected void Awake()
    {
        enabled = false;
    }

    // Activates the skill
    public void Activate()
    {
        if (state == SkillStateOld.Ready)
        {
            enabled = true;
            DoActivate();
            
            Debug.Log("Skill type: " + GetType().ToString() + ". Skill state: " + state.ToString());
        }
    }
    
    // Cancels skill casting
    public void CancelCast()
    {
        if (state == SkillStateOld.Casting)
        {
            Interrupt();
            enabled = false;
            state = SkillStateOld.Ready;

            Debug.Log("Skill type: " + GetType().ToString() + ". Skill state: " + state.ToString());
        }
    }

    // Cancels skill impact on the player
    public void CancelActivation()
    {
        if (state == SkillStateOld.Activated)
        {
            Interrupt();
            Cooldown();
            enabled = false;

            Debug.Log("Skill type: " + GetType().ToString() + ". Skill state: " + state.ToString());
        }
    }

    // Deactivates the skill
    protected void Deactivate()
    {
        DoDeactivate();
        Cooldown();
        enabled = false;

        Debug.Log("Skill type: " + GetType().ToString() + ". Skill state: " + state.ToString());
    }

    // Sets the skill on colldown
    protected void Cooldown()
    {
        state = SkillStateOld.Cooldown;
        StartCoroutine("WaitForCooldown");
    }

    // Activate implementation
    protected abstract void DoActivate();
    // Interrupt implementation
    protected abstract void Interrupt();
    // Deactivate implementation
    protected abstract void DoDeactivate();

    private IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        state = SkillStateOld.Ready;
    }
}