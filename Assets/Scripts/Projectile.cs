using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public void Launch(float range, Vector3 velocity) {
        transform.Rotate(Vector3.forward, Vector3.SignedAngle(Vector3.up, velocity, Vector3.forward));
        StartCoroutine(LaunchEnumerator(range, velocity));
    }

    private IEnumerator LaunchEnumerator(float range, Vector3 velocity) {
        var startPos = transform.position;
        while ((transform.position - startPos).sqrMagnitude < range * range) {
            transform.Translate(velocity * Time.deltaTime, Space.World);
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}
