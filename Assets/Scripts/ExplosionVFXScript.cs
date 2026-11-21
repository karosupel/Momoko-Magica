using UnityEngine;

public class ExplosionVFXScript : MonoBehaviour
{
    [SerializeField] ParticleSystem Explosion;
    [SerializeField] ParticleSystem butterflySplash;
    [SerializeField] ParticleSystem butterflyOrbiting;
    [SerializeField] ParticleSystem sparkles;
    private float explosionRadius = 1f;
    private float butterflySplashRadius = 1f;
    private float butterflyOrbitingRadius = 1f;
    private float sparklesRadius = 1f;

    public void SetRadius(float radius)
    {
        explosionRadius = radius + 1f; //bazowa wartosc dla radius = 1, wynosi 2, wiec żeby sie skalowało
        butterflySplashRadius = radius / 5f; //bazowa wartość dla radius = 1, wynosi 0.2
        butterflyOrbitingRadius = radius; //bazowa wartosc dla radius = 1, wynosi 1
        sparklesRadius = radius * 1.5f; //bazowa wartosc dla radius =1, wynosi 1.5
        Explosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);

        var ButterflySplashShape = butterflySplash.shape;
        ButterflySplashShape.radius = butterflySplashRadius;

        var ButterflyOrbitingShape = butterflyOrbiting.shape;
        ButterflyOrbitingShape.radius = butterflyOrbitingRadius;

        var SparklesShape = sparkles.shape;
        SparklesShape.radius = sparklesRadius;
    }


}
