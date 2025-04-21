using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCursor : MonoBehaviour
{

    public static PlayerCursor Instance;
    public Image image;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Cursor.visible = false;

        Instance = this;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
        Vector2 view = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1) Cursor.visible = true;
        else Cursor.visible = false;
    }
}
