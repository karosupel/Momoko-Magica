using UnityEngine;

public class AbilityInstance
{
    public AbilityData Data { get; private set; }

    public int Level { get; private set; } = 1;

    public AbilityInstance(AbilityData data)
    {
        Data = data;
    }

    public AbilityLevelData CurrentStats =>
        Data.GetLevelData(Level);

    public void LevelUp()
    {
        if (Level >= Data.levels.Length)
            return;

        Level++;
    }

    public void ResetLevel()
    {
        Level = 1;
    }
}
