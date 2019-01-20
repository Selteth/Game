using UnityEngine;

// THIS CLASS SHOULD BE ABSTRACT
// IT IS NOT AT THE MOMENT ONLY FOR TESTING PURPOSES

// Represents base actions on MechanismControlSkill activation.
// To create script for some mechanism, it is needed to inherit this class
// and implement method ActivateMechanism()
public class ActivateOnSkill : MonoBehaviour
{
    // Player
    public GameObject player;

    // Mechanism control skill reference
    private MechanismControlSkill mechanismControlSkill;

    // Activates mechanism when player clicks on it
    void OnMouseDown()
    {
        // Initialize skill reference
        LazyInitSkill();

        // If the skill is set and active...
        if (mechanismControlSkill != null && mechanismControlSkill.enabled)
        {
            ActivateMechanism();
            // Deactivate skill after mechanism activation
            mechanismControlSkill.ActivateMechanism();
        }
    }

    // Debug only
    protected void ActivateMechanism()
    {
        Debug.Log("I am activated!");
        gameObject.GetComponent<MechanismInterface>().Activate();
    }
    // End debug only

    // Release
    // Activates a mechanism the script is attached to
    //protected abstract void ActivateMechanism();
    // End release

    // Performs lazy initialization of reference.
    // It is initialized lazy because player might not have the skill at the moment
    private void LazyInitSkill()
    {
        if (mechanismControlSkill == null)
            mechanismControlSkill = player.GetComponent<MechanismControlSkill>();
    }
}