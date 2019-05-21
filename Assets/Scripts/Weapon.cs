using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour {
    [Range(0, 360)] [SerializeField] private float angle;
    [Range(0, 10)] [SerializeField] private float range;
    [Range(0, 10)] [SerializeField] private float projectileSpeed;
    [Range(0, 100)] [SerializeField] private float fireRate;
    
    [SerializeField] private Projectile projectilePrefab;

    private LineRenderer _sight;
    private Transform _rectTransform;
    private Vector3 _currentDirection;
    private AudioSource _audio;

    private readonly Stopwatch _stopwatch = new Stopwatch();

    /// <summary>
    /// Fire this weapon
    /// </summary>
    /// <param name="velocity">Additional direction and speed</param>
    public void Fire(Vector2 velocity) {
        if (_stopwatch.ElapsedMilliseconds < 1000 / fireRate) return;
        var bullet = Instantiate(projectilePrefab.gameObject, transform.position, Quaternion.identity);
        var proj = bullet.GetComponent<Projectile>();
        proj.Launch(range / projectileSpeed, _currentDirection * projectileSpeed + (Vector3) velocity, _currentDirection);
        _stopwatch.Restart();
        _audio.PlayOneShot(proj.FireSound);
    }

    private void Awake() {
        _stopwatch.Start();
        _sight = GetComponent<LineRenderer>();
        _audio = GetComponent<AudioSource>();
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
