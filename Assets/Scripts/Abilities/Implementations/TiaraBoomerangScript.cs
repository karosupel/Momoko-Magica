using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(
    fileName = "New Tiara Ability",
    menuName = "Abilities/Tiara"
)]
public class TiaraBoomerangScript : AbilityData
{
    //this is that i could have diffrent types of levels data
    protected override void OnValidate()
    {
        base.OnValidate();
        if (levels == null) return;

        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i] == null || levels[i].GetType() != typeof(TiaraLevelData))
            {
                levels[i] = new TiaraLevelData();
            }
        }
    }

    public TiaraLevelData GetTiaraLevel(int index)
    {
        return levels[index] as TiaraLevelData;
    }

    protected override AbilityLevelData CreateDefaultLevelData()
    {
        return new TiaraLevelData();
    }
    [SerializeField] GameObject TiaraPrefab;
    TiaraScript tiaraScript;
    public override void Activate(AbilityContext context, AbilityLevelData stats, Vector2 targetPosition, GameObject abilityVisualPrefab)
    {
        TiaraLevelData tiaraStats = stats as TiaraLevelData;
        if(tiaraStats == null)
        {
            Debug.LogError("Requires tiara level data");
            return;
        }
        GameObject tiara = Instantiate(TiaraPrefab, context.player.position, quaternion.identity);
        tiaraScript = tiara.GetComponent<TiaraScript>();
        tiaraScript.SetStats(context.player, targetPosition, tiaraStats.range, tiaraStats.startSpeed);
    }
}
