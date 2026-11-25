using UnityEngine;

[CreateAssetMenu(
    fileName = "New Whirl Ability",
    menuName = "Abilities/Whirl"
)]
public class WhirlAbilityScript : AbilityData
{
    protected override void OnValidate()
    {
        base.OnValidate();
        if (levels == null) return;

        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i] == null || levels[i].GetType() != typeof(WhirlLevelData))
            {
                levels[i] = new WhirlLevelData();
            }
        }
    }

    // Opcjonalna właściwość pomocnicza, żeby w kodzie wygodnie odwoływać się do pól Whirl
    public WhirlLevelData GetWhirlLevel(int index)
    {
        return levels[index] as WhirlLevelData;
    }

    protected override AbilityLevelData CreateDefaultLevelData()
    {
        return new WhirlLevelData();
    }

    [SerializeField] GameObject whirlPrefab;
    WhirlScript whirlScript;
    public override void Activate(AbilityContext context, AbilityLevelData stats, Vector2 targetPosition, GameObject abilityVisualPrefab)
    {
        WhirlLevelData whirlStats = stats as WhirlLevelData;
        if(whirlStats == null)
        {
            Debug.LogError("Requires whirl level data");
            return;
        }
        GameObject whirl = Instantiate(whirlPrefab, targetPosition, Quaternion.identity);
        whirl.transform.localScale = new Vector3(whirlStats.range, whirlStats.range, whirlStats.range);
        whirlScript = whirl.GetComponent<WhirlScript>();
        whirlScript.SetStats(whirlStats.range, whirlStats.pullAcceleration, whirlStats.pullAccelerationRate, whirlStats.damage, whirlStats.damageInterval);
    }
}
