
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    [SerializeField] private Transform target;
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private Camera cam;

    private float _startFOV; // field of view
    private float _targetFOV;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _startFOV = cam.fieldOfView;
        _targetFOV = _startFOV;
    }   

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _targetFOV, zoomSpeed * Time.deltaTime);
    }

    public void ZoomIn(float newZoom)
    {
        _targetFOV = newZoom;
    }

    public void ZoomOut()
    {
        _targetFOV = _startFOV;
    }
}
