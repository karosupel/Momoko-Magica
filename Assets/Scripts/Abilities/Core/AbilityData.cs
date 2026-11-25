using UnityEngine;

public enum TargetingType
{
    Instant,
    Ground,
    Enemy,
    Direction
}
public abstract class AbilityData : ScriptableObject
{
    public string abilityName;
    public Sprite icon;

    [SerializeReference]
    public AbilityLevelData[] levels;

    public AbilityLevelData GetLevelData(int level)
    {
        int index = Mathf.Clamp(
            level - 1,
            0,
            levels.Length - 1
        );

        return levels[index];
    }

    public TargetingType targetingType;

    public GameObject abilityVisualPrefab;

    public abstract void Activate(
        AbilityContext context,
        AbilityLevelData stats,
        Vector2 targetPosition,
        GameObject abilityVisualPrefab
    );

    protected virtual AbilityLevelData CreateDefaultLevelData()
    {
        return new AbilityLevelData();
    }

    protected virtual void OnValidate()
    {
        if (levels == null || levels.Length == 0)
            levels = new AbilityLevelData[] { CreateDefaultLevelData() };

        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i] == null)
                levels[i] = CreateDefaultLevelData();
        }
    }
}