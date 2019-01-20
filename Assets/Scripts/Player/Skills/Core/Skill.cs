using System.Collections;
using UnityEngine;

public enum SkillState
{
    Ready, Casting, Activated, Cooldown
}

// Represents player skill base behaviour
public abstract class Skill : MonoBehaviour, ISkill {
    
    // Time during which player cannot activate the skill
    protected float cooldown;
    // Skill state
    protected SkillState state;

    protected void Awake()
    {
        enabled = false;
    }

    // Activates the skill
    public void Activate()
    {
        if (state == SkillState.Ready)
        {
            enabled = true;
            DoActivate();
            
            Debug.Log("Skill type: " + GetType().ToString() + ". Skill state: " + state.ToString());
        }
    }
    
    // Cancels skill casting
    public void CancelCast()
    {
        if (state == SkillState.Casting)
        {
            Interrupt();
            enabled = false;
            state = SkillState.Ready;

            Debug.Log("Skill type: " + GetType().ToString() + ". Skill state: " + state.ToString());
        }
    }

    // Cancels skill impact on the player
    public void CancelActivation()
    {
        if (state == SkillState.Activated)
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
        state = SkillState.Cooldown;
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
        state = SkillState.Ready;
    }
}