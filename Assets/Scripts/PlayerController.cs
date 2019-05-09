using UnityEngine;
using UnityEngine.Experimental.Input;

public class PlayerController : MonoBehaviour, GameControls.IShipActions {

    [Range(0, 360)] [Tooltip("Angles per second")] [SerializeField]
    private float rotationSpeed;
    [Range(0, 10)] [SerializeField]
    private float acceleration;
    [Range(0, 1)] [SerializeField]
    private float motionDamping;
    
    [Range(0, 50)] [SerializeField]
    private float maximumSpeed;

    private Transform _t;
    private ShipController _shipController;
    private GameControls _controls;
    private Vector3 _direction;
    
    private void Awake() {
        _t = transform;
        _controls = new GameControls();
        _controls.Ship.SetCallbacks(this);
        _shipController = new ShipController(rotationSpeed, acceleration, motionDamping, _t, maximumSpeed);
    }

    void Update() {
        _shipController.RotateAt(GameInput.MousePosition);
        _shipController.MoveToDirection(_direction);
        _direction = Vector3.zero;
        
        foreach (var lineRenderer in GetComponentsInChildren<LineRenderer>()) {
            var position = lineRenderer.transform.position;
            var z = position.z;
            var mousePosition = GameInput.MousePosition;
            mousePosition.z = z;
            lineRenderer.SetPositions(new[] {
                position,
                position + (mousePosition-position).normalized
            });
        }
        

        LegacyScroll();
    }

    /// <summary>
    /// TODO remove when Unity's UnityEngine.Experimental.Input.InputSystem.GetDevice<Mouse>().scroll is fixed
    /// </summary>
    private static void LegacyScroll() {
        var scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > float.Epsilon) 
            CameraController.Instance.Scroll(Vector2.up * scroll);
    }

    public void OnMove(InputAction.CallbackContext context) {
        _direction = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context) {
        Debug.Log("Shoot");
    }

    public void OnZoom(InputAction.CallbackContext context) {
        var scroll = context.ReadValue<float>();
        Debug.Log($"Zoom: {scroll}");
        CameraController.Instance.Scroll(Vector2.up * scroll * Time.deltaTime);
    }

    public void OnScroll(InputAction.CallbackContext context) {
        //Debug.Log($"Scroll: {context.ReadValue<Vector2>()}"); BUG scroll input is broken
    }

    public void OnEnable()
    {
        _controls.Enable();
    }

    public void OnDisable()
    {
        _controls.Disable();
    }
}