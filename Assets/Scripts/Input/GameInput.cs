using UnityEngine;

public class GameInput : MonoBehaviour {
    public static Vector3 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
}
