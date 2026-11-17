using UnityEngine;


public class AbilityController : MonoBehaviour
{
    public AbilityDatabase abilityDatabase; //to testowo tutaj, pozniej przeniesc to do day manager jak zaczniesz go robic
    public AbilityInstance[] abilities = new AbilityInstance[3];
    public AbilityInstance selectedAbility;
    private bool isTargeting;
    public bool IsTargeting => isTargeting;

    private AbilityContext context;

    private void Awake()
    {
        context = new AbilityContext(gameObject);
    }

    void Start()
    {
        EquipAbility(0, abilityDatabase.allAbilities[0]); //rowniez testowo
    }

    public void EquipAbility(int index, AbilityData data)
    {
        abilities[index] = new AbilityInstance(data);
    }

    public void ResetAbilities()
    {
        foreach (var ability in abilities)
        {
            if (ability != null)
                ability.ResetLevel();
        }
    }

    public void SelectAbility(int index)
    {
        if (index < 0 || index >= abilities.Length)
            return;

        selectedAbility = abilities[index];

        if(selectedAbility != null)
        {
            if(selectedAbility.Data.targetingType != TargetingType.Instant)
            {
                Debug.Log("I am targeting");
                isTargeting = true;
            }
            else
            {
                isTargeting = false;
                //selectedAbility.Data.Activate(context, selectedAbility.CurrentStats, Vector2.zero);
            }
        }
    }

    public void UnselectAbility()
    {
        selectedAbility = null;
        isTargeting = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectAbility(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectAbility(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectAbility(2);
        }
    }
}
