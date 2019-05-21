using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private AudioClip fireSound;
    private SpriteRenderer _spriteRenderer;
    public AudioClip FireSound => fireSound;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Launch a projectile
    /// </summary>
    /// <param name="lifetime">Seconds before destroying</param>
    /// <param name="velocity">Direction and speed of projectile, absolute</param>
    /// <param name="direction">Direction for sprite, can differ from velocity due to inertia</param>
    public void Launch(float lifetime, Vector3 velocity, Vector3 direction) {
        transform.Rotate(Vector3.forward, Vector3.SignedAngle(Vector3.up, direction, Vector3.forward));
        StartCoroutine(LaunchEnumerator(lifetime, velocity));
    }

    private IEnumerator LaunchEnumerator(float lifetime, Vector3 velocity) {
        var stopWatch = new Stopwatch();
        var color = _spriteRenderer.color;
        stopWatch.Start();

        var gr = new Gradient();
        gr.SetKeys(new[] {
                new GradientColorKey(color, 0)
            },
            new[] {
                new GradientAlphaKey(color.a, 0),
                new GradientAlphaKey(color.a, 0.5f),
                new GradientAlphaKey(0, 1)
            });
        while (stopWatch.ElapsedMilliseconds < lifetime * 1000) {
            transform.Translate(velocity * Time.deltaTime, Space.World);
            yield return new WaitForEndOfFrame();
            var time = stopWatch.ElapsedMilliseconds / (lifetime * 1000);


            _spriteRenderer.color = gr.Evaluate(time);
        }

        Destroy(gameObject);
    }
}