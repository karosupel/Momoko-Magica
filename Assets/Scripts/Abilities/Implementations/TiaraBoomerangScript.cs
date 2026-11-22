using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(
    fileName = "New Tiara Ability",
    menuName = "Abilities/Tiara"
)]
public class TiaraBoomerangScript : AbilityData
{
    [SerializeField] GameObject TiaraPrefab;
    TiaraScript tiaraScript;
    public override void Activate(AbilityContext context, AbilityLevelData stats, Vector2 targetPosition, GameObject abilityVisualPrefab)
    {
        GameObject tiara = Instantiate(TiaraPrefab, context.player.position, quaternion.identity);
        tiaraScript = tiara.GetComponent<TiaraScript>();
        tiaraScript.SetStats(context.player, targetPosition, stats.range, 3f);
    }
}
