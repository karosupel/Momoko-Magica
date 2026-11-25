using UnityEngine;

[System.Serializable]
public class AbilityLevelData
{
    public float damage;
    public float range;
    public float cooldown;
    public float duration;
}

[System.Serializable]
public class WhirlLevelData : AbilityLevelData
{
    public float pullAcceleration;
    public float pullAccelerationRate;
    public float damageInterval;
}