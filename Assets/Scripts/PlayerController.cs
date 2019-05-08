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
        _shipController.MoveToDirection(_direction);
        _direction = Vector3.zero;
    }
    
    public void OnMove(InputAction.CallbackContext context) {
        _direction = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context) {
        Debug.Log("Shoot");
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