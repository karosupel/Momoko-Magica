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

        targetingVisualSprite.transform.localScale = new Vector3(range, range, range);
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
