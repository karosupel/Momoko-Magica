using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private int maxHp = 100;
    [SerializeField] private int currentHp;
    public int CurrentHp
    {
        get {return currentHp;} 
        set {currentHp = value;}
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHp = maxHp;
    }

    public void GetHit(int amount)
    {
        currentHp -= amount;
    }
}
