using UnityEngine;

public class GhostSkill : Skill
{
    private int playerLayer;
    private int obstacleLayer;

    void Awake()
    {
        DoDeactivate();
        cooldown = 1f;
        duration = 2f;
        playerLayer = gameObject.layer;
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
    }
    
    protected override void DoActivate()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, true);
    }
    
    protected override void DoDeactivate()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, false);
    }

}
