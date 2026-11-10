using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private float maxHp = 100;
    [SerializeField] private float currentHp;
    public float CurrentHp
    {
        get {return currentHp;} 
        set {currentHp = value;}
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHp = maxHp;
    }

    public void GetHit(float amount)
    {
        currentHp -= amount;
        Debug.Log("currenthp: " + currentHp);
    }
}
