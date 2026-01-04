using UnityEngine;
using UnityEngine.InputSystem;

public class AimIndicator : MonoBehaviour
{
    [SerializeField]
    private GameObject spriteObject;

    private Vector2 aimDirection;

    private bool aiming = false;

    public void OnAim(InputValue value)
    {
        Vector2 aim = value.Get<Vector2>();
        if (aim.magnitude > 0.1f)
        {
            aimDirection = aim;
            aiming = true;
        } 
        else
        {
            aiming = false;
        }
    }

    public void OnMove(InputValue value)
    {
        if (!aiming)
        {
            Vector2 aim = value.Get<Vector2>();
            if (aim.magnitude > 0.1f)
            {
                aimDirection = aim;
            }
        }
    }

    private void Update()
    {
        float angleRads = Mathf.Atan2(aimDirection.y, aimDirection.x);
        float angleDegrees = angleRads * Mathf.Rad2Deg;

        spriteObject.transform.rotation = Quaternion.Euler(0f, 0f, angleDegrees);
    }

    public float GetAngle()
    {
        float rads = Mathf.Atan2(aimDirection.y, aimDirection.x);
        float degs = rads * Mathf.Rad2Deg;
        return degs;
    }
}
