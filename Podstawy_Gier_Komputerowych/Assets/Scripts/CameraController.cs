using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerBacteria Player; // Player to follow.
    public float CameraSpeed = 12.0f; // Speed of the camera shifting.
    private bool _allowZoom;

    private Camera _camera;
    void Start()
    {
        if (_camera == null)
        {
            _camera = GetComponent<Camera>();
            _camera.orthographic = true;
        }
    }

    void LateUpdate()
    {
        Vector3 position = transform.position;

        float interpolation = CameraSpeed * Time.deltaTime;

        position.y = Mathf.Lerp(transform.position.y, Player.transform.position.y, interpolation);
        position.x = Mathf.Lerp(transform.position.x, Player.transform.position.x, interpolation);

        transform.position = PixelPerfectCorrection(position);

        if (_allowZoom)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                _camera.orthographicSize++;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
                _camera.orthographicSize--;
            }
        }

        if (Input.GetKeyDown(KeyCode.F11))
            _allowZoom = !_allowZoom;
    }

    private Vector3 PixelPerfectCorrection(Vector3 insertVector)
    {
        var temp = _camera.WorldToScreenPoint(insertVector);
        temp.y = Mathf.Round(temp.y);
        temp.x = Mathf.Round(temp.x);
        return _camera.ScreenToWorldPoint(temp);
    }
}
