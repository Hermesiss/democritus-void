using UnityEngine;

public class Parallax2D : MonoBehaviour {

    private Vector3 _startPosition;
    
    //TODO add tile changing mode
    
    //TODO add new mode - custom shift ignoring Z
    
    private void Awake() {
        _startPosition = transform.position;
        PlayerController.OnPositionChange.AddListener(delta => {
            //transform.Translate(delta * transform.position.z * Time.deltaTime, Space.World);
            transform.position = _startPosition - (Vector3) (delta / transform.position.z);
        });
    }

    
}
