using UnityEngine;
using UnityEngine.InputSystem;


public class AbilityVisual : MonoBehaviour
{
    protected SpriteRenderer targetingVisualSprite;
    protected Vector2 mousePosition;

    protected virtual void Awake()
    {
        targetingVisualSprite = GetComponentInChildren<SpriteRenderer>();
        targetingVisualSprite.enabled = false;
    }

    public void ShowRange(float range)
    {
        targetingVisualSprite.enabled = true;
        float diameter = range * 2f;

        targetingVisualSprite.size = new Vector2(
            diameter,
            diameter
        );
    }

    public void HideRange()
    {
        targetingVisualSprite.enabled = false;
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(
            Mouse.current.position.ReadValue()
        );
        targetingVisualSprite.transform.position = mousePosition;
    }
}
