using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Input;


public class Vector2Event : UnityEvent<Vector2> {
    
}

public class PlayerController : MonoBehaviour, GameControls.IShipActions {

    public enum Param {
        RotationSpeed,
        Aceleration,
        MotionDamping,
        MaximumSpeed,
        BrakingForce
    }

    public static PlayerController Instance { get; private set; }
        
    public static Vector2Event OnPositionChange = new Vector2Event();

    [Range(0, 360)] [Tooltip("Angles per second")] [SerializeField]
    private float rotationSpeed;

    [Range(0, 10)] [SerializeField] private float acceleration;
    [Range(0, 1)] [SerializeField] private float motionDamping;
    [Range(0, 50)] [SerializeField] private float maximumSpeed;
    [Range(0, 1000)] [SerializeField] private float inertiaMultiplier;

    private Transform _t;
    private ShipController _shipController;
    private GameControls _controls;
    private bool _isBrake;
    private Weapon[] _weapons;
    

    //TODO sliders for test
    /*private void OnGUI() {
        var acc = GUI.HorizontalSlider(new Rect(10, 10, 100, 50), acceleration, 0, 10);
        if (Math.Abs(acc - acceleration) > float.Epsilon) {
            acceleration = acc;
            _shipController.SetMovementSpeed(acc);
        }
    }*/

    private void SetShipParams() {
        _shipController.SetMaximumSpeed(maximumSpeed);
        _shipController.SetMovementSpeed(acceleration);
        _shipController.SetRotationSpeed(rotationSpeed);
        _shipController.SetInertiaMultiplier(inertiaMultiplier);
        _shipController.SetMovementDamping(motionDamping);
        
    }
	//TODO set this params to ship controller
    public void SetParam(Param param, float value) {
        switch (param) {
            case Param.RotationSpeed:
                rotationSpeed = value;
                break;
            case Param.Aceleration:
                acceleration = value;
                break;
            case Param.MotionDamping:
                motionDamping = value;
                break;
            case Param.MaximumSpeed:
                maximumSpeed = value;
                break;
            case Param.BrakingForce:
                inertiaMultiplier = value;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(param), param, null);
        }
        SetShipParams();
    }

    public float GetParam(Param param) {
        switch (param) {
            case Param.RotationSpeed:
                return rotationSpeed;
            case Param.Aceleration:
                return acceleration;
            case Param.MotionDamping:
                return motionDamping;
            case Param.MaximumSpeed:
                return maximumSpeed;
            case Param.BrakingForce:
                return inertiaMultiplier;
            default:
                throw new ArgumentOutOfRangeException(nameof(param), param, null);
        }
    }

    private void Awake() {
        _t = transform;
        _controls = new GameControls();
        _controls.Ship.SetCallbacks(this);
        _shipController = new ShipController(rotationSpeed,
            acceleration,
            motionDamping,
            _t,
            maximumSpeed);
        Instance = this;
    }

    private void Start() {
        _weapons = GetComponentsInChildren<Weapon>();
    }

    private void Update() {
        if (Time.timeScale < 0.1f) {
            _controls.Ship.Disable();
            return;
        }

        _controls.Ship.Enable();

        _shipController.RotateAt(GameInput.MousePosition);
        _shipController.MoveToDirection();

        LegacyScroll();
        OnPositionChange.Invoke(transform.position);
    }

    private void LateUpdate() {
        _isBrake = false;
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
        if (_isBrake) return;
        _shipController.SetDirection(context.ReadValue<Vector2>());
    }

    public void OnShoot(InputAction.CallbackContext context) {
        if (_weapons == null || _weapons.Length <= 0) return;
        foreach (var weapon in _weapons) {
            weapon.Fire();
        }
    }

    public void OnZoom(InputAction.CallbackContext context) {
        var scroll = context.ReadValue<float>();
        Debug.Log($"Zoom: {scroll}");
        CameraController.Instance.Scroll(Vector2.up * scroll * Time.deltaTime);
    }

    public void OnScroll(InputAction.CallbackContext context) {
        //Debug.Log($"Scroll: {context.ReadValue<Vector2>()}"); BUG scroll input is broken
    }

    public void OnBrake(InputAction.CallbackContext context) {
        _isBrake = true;
        _shipController.SetDirection(Vector3.zero);
        _shipController.SetInertiaMultiplier(inertiaMultiplier);
    }

    public void OnEnable() {
        _controls.Ship.Enable();
    }

    public void OnDisable() {
        _controls.Ship.Disable();
    }
}