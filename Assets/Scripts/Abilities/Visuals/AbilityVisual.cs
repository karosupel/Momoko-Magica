using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class AbilityVisual : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector2 mousePosition;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    public void ShowRange(float range)
    {
        spriteRenderer.enabled = true;
        float diameter = range * 2f;

        spriteRenderer.size = new Vector2(
            diameter,
            diameter
        );
    }

    public void HideRange()
    {
        spriteRenderer.enabled = false;
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(
            Mouse.current.position.ReadValue()
        );
        transform.position = mousePosition;
    }
}
