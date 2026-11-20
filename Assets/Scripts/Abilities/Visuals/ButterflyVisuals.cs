using UnityEngine;

public class ButterflyVisuals : AbilityVisual
{
    public GameObject explosionVFXPrefab;

    protected override void Awake()
    {
        base.Awake();
        explosionVFXPrefab = GetComponentInChildren<ParticleSystem>().gameObject;
        HideExplosionVFX();
    }
    public void ShowExplosionVFX(float range)
    {
        explosionVFXPrefab.gameObject.SetActive(true);
        //tutaj potem bedziesz inputowac w to range
    }

    public void HideExplosionVFX()
    {
        explosionVFXPrefab.SetActive(false);
    }
}
