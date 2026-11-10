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
    public float cooldown;

    public TargetingType targetingType;

    public abstract void Activate(
        AbilityContext context,
        Vector2 targetPosition
    );
}