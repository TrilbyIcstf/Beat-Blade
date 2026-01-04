using UnityEngine;

public class AimHelper : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!Input.GetKey("left shift"))
        {
            float mouseX = mousePos.x;
            float mouseY = mousePos.y;
            mouseX = mouseX * 2;
            mouseY = mouseY * 2;
            mouseX = Mathf.Round(mouseX) / 2;
            mouseY = Mathf.Round(mouseY) / 2;
            mousePos.x = mouseX;
            mousePos.y = mouseY;
        }
        transform.position = mousePos;
    }
}
