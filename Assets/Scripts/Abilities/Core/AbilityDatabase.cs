using UnityEngine;

[CreateAssetMenu(
    fileName = "Ability Database",
    menuName = "Abilities/Database"
)]
public class AbilityDatabase : ScriptableObject
{
    public AbilityData[] allAbilities;
}
