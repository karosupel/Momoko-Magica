using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityVisual : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer targetingVisualSprite;
    protected Vector2 mousePosition;
    protected TargetingType targetingType;
    protected float rotationZ;
    Vector2 toMouse;

    protected virtual void Awake()
    {
        targetingVisualSprite = GetComponentInChildren<SpriteRenderer>();
        targetingVisualSprite.enabled = false;
    }

    public void ShowRange(float range, TargetingType targeting)
    {
        targetingVisualSprite.enabled = true;
        targetingType = targeting;
        switch(targetingType)
        {
            case TargetingType.Ground:
                targetingVisualSprite.transform.localScale = new Vector3(range, range, range);
                break;
            case TargetingType.Direction:
                targetingVisualSprite.transform.position = targetingVisualSprite.gameObject.transform.position + new Vector3(0f,0.5f,0f);
                break;
        }
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
        switch(targetingType)
        {
            case TargetingType.Ground:
                targetingVisualSprite.transform.position = mousePosition;
                break;
            case TargetingType.Direction:
                toMouse = mousePosition - (Vector2)targetingVisualSprite.transform.position;

                if (toMouse.sqrMagnitude > 0.0001f)
                {
                    // Gdy kursor jest pod graczem, celuj poziomo zamiast w dół.
                    if (toMouse.y < 0f)
                    {
                        toMouse.y = 0f;
                        if (Mathf.Abs(toMouse.x) < 0.0001f)
                            toMouse = Vector2.up;
                    }

                    float angleFromUp = Mathf.Atan2(toMouse.x, toMouse.y) * Mathf.Rad2Deg;
                    rotationZ = -Mathf.Clamp(angleFromUp, -90f, 90f);
                    targetingVisualSprite.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
                }
                break;
        }
    }
}
