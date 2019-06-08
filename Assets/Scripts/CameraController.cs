using Cinemachine;
using UnityEngine;
using UnityEngine.U2D;

public interface IScrollable {
    void Scroll(Vector2 scroll);
}

[RequireComponent(typeof(CinemachineBrain))]
[RequireComponent(typeof(PixelPerfectCamera))]
public class CameraController : MonoBehaviour, IScrollable
{
    public static CameraController Instance { get; private set; }

    [SerializeField] [Range(0, 300)] private int minimumZoom, maximumZoom, zoomSpeed;

    private CinemachineBrain _cinemachineBrain;
    private CinemachineVirtualCamera _virtualCamera;
    private PixelPerfectCamera _pixelPerfectCamera;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            DestroyImmediate(gameObject);
        }

        _cinemachineBrain = GetComponent<CinemachineBrain>();
        _pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
    }

    
    public void Scroll(Vector2 scroll) {
        
        var zoom = _pixelPerfectCamera.assetsPPU;
        zoom += (int) (zoomSpeed * scroll.y);
        zoom = Mathf.Clamp(zoom, minimumZoom, maximumZoom);
        _pixelPerfectCamera.assetsPPU = zoom;
    }
}
