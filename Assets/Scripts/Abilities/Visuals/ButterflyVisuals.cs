using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButterflyVisuals : AbilityVisual
{
    public GameObject explosionVFXPrefab;

    protected override void Awake()
    {
        base.Awake();
        explosionVFXPrefab = Instantiate(explosionVFXPrefab, base.mousePosition, quaternion.identity);
        HideExplosionVFX();
    }
    public void ShowExplosionVFX(float range)
    {
        explosionVFXPrefab.transform.position = mousePosition;
        explosionVFXPrefab.GetComponent<ExplosionVFXScript>().SetRadius(range);
        explosionVFXPrefab.gameObject.SetActive(true);
        //tutaj potem bedziesz inputowac w to range
        Destroy(explosionVFXPrefab, 10f); //podmienić troche na cooldown
    }

    public void HideExplosionVFX()
    {
        explosionVFXPrefab.SetActive(false);
    }
}
