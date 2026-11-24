using UnityEngine;

[CreateAssetMenu(
    fileName = "New Whirl Ability",
    menuName = "Abilities/Whirl"
)]
public class WhirlAbilityScript : AbilityData
{
    public override void Activate(AbilityContext context, AbilityLevelData stats, Vector2 targetPosition, GameObject abilityVisualPrefab)
    {
        Debug.Log("boop");
    }
}
