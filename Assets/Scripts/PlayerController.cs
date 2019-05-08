using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Range(0, 360)] [Tooltip("Angles per second")] [SerializeField]
    private float rotationSpeed;

    private Transform _t;
    private ShipController _shipController;

    private void Awake() {
        _t = transform;
        _shipController = new ShipController(rotationSpeed);
    }

    void Update() {
        _shipController.RotateAt(GameInput.MousePosition, _t);
    }
}