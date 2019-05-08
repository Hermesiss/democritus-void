using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Input;

public class Vector2Event : UnityEvent<Vector2> {
    
}

public class GameInput : MonoBehaviour {
    public static Vector3 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    public static Vector2Event OnMovement = new Vector2Event();
    private GameControls _controls;

    void Awake() {
        _controls = new GameControls();
        
    }

    public void OnMove(InputAction.CallbackContext context) {
        var direction = context.ReadValue<Vector2>();
        OnMovement.Invoke(direction);
        Debug.Log($"Moving to {direction}");
    }
}
