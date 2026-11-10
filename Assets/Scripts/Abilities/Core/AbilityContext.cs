using UnityEngine;

public class AbilityContext
{
    public Transform player;
    public Rigidbody2D playerRb;
    public PlayerStateManager playerStateManager;

    public AbilityContext(GameObject player)
    {
        this.player = player.transform;
        this.playerRb = player.GetComponent<Rigidbody2D>();
        this.playerStateManager = player.GetComponent<PlayerStateManager>();
    }
}
