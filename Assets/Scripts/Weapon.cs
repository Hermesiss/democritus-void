using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class Weapon : MonoBehaviour {
    [Range(0, 360)] [SerializeField] private float angle;
    [Range(0, 10)] [SerializeField] private float range;
    [Range(0, 10)] [SerializeField] private float projectileSpeed;
    [Range(0, 100)] [SerializeField] private float firerate;
    
    [SerializeField] private Projectile projectilePrefab;

    private LineRenderer _sight;
    private Image _arc;
    private Transform _rectTransform;
    private Vector3 _currentDirection;

    private readonly Stopwatch _stopwatch = new Stopwatch();

    public void Fire() {
        if (_stopwatch.ElapsedMilliseconds < 1000 / firerate) return;
        var bullet = Instantiate(projectilePrefab.gameObject, transform.position, Quaternion.identity);
        var proj = bullet.GetComponent<Projectile>();
        proj.Launch(range, _currentDirection * projectileSpeed);
        _stopwatch.Restart();
    }

    private void Awake() {
        _stopwatch.Start();
        _sight = GetComponent<LineRenderer>();
        _arc = GetComponentInChildren<Image>();
        _arc.fillAmount = angle / 360;
        _arc.transform.Rotate(transform.forward, angle / 2);
    }

    private void Update() {
        if (Time.timeScale < 0.1f) return;

    }

    private void LateUpdate() {
        if (Time.timeScale < 0.1f) return;
        DrawSight();
    }

    private void DrawSight() {
        var position = transform.position;
        var z = position.z;
        var mousePosition = GameInput.MousePosition;
        mousePosition.z = z;
        var direction = mousePosition - position;
        var up = transform.up;
        var sAngle = Vector3.SignedAngle(direction, up, Vector3.forward);
        if (Mathf.Abs(sAngle) > angle / 2) {
            var sign = -Mathf.Sign(sAngle);
            direction = Quaternion.Euler(0, 0, angle / 2 * sign) * up;
        }

        var directionNormalized = direction.normalized;
        _sight.SetPositions(new[] {
            position,
            position + directionNormalized * range
        });
        _currentDirection = directionNormalized;
    }
}
