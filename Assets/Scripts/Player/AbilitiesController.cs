using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityController : MonoBehaviour
{
    public AbilityDatabase abilityDatabase; //to testowo tutaj, pozniej przeniesc to do day manager jak zaczniesz go robic
    public AbilityInstance[] abilities = new AbilityInstance[3];
    public AbilityInstance selectedAbility;
    private bool isTargeting;
    public bool IsTargeting => isTargeting;

    public AbilityVisual abilityVisual;

    private AbilityContext context;

    private void Awake()
    {
        context = new AbilityContext(gameObject);
    }

    void Start()
    {
        EquipAbility(0, abilityDatabase.allAbilities[0]); //rowniez testowo
        EquipAbility(1, abilityDatabase.allAbilities[1]);
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
        InstantianeVisuals(selectedAbility);

        if(selectedAbility != null)
        {
            if(selectedAbility.Data.targetingType != TargetingType.Instant)
            {
                abilityVisual.ShowRange(selectedAbility.CurrentStats.range, selectedAbility.Data.targetingType);
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
        Destroy(abilityVisual.gameObject);
        isTargeting = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(selectedAbility != null && abilityVisual != null)
            {
                UnselectAbility();
                return;
            }
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

    private void InstantianeVisuals(AbilityInstance ability)
    {
        GameObject visualObject = Instantiate(ability.Data.abilityVisualPrefab, transform.position, Quaternion.identity, transform);
        abilityVisual = visualObject.GetComponent<AbilityVisual>();
    }
}
