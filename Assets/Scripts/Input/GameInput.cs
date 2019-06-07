using UnityEngine;

public class GameInput{
    public static Vector3 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition); //TODO handle windowed mode
}
