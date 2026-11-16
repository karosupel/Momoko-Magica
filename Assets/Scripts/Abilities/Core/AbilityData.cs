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

    public abstract void Activate(
        AbilityContext context,
        AbilityLevelData stats,
        Vector2 targetPosition
    );
}