using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector3 _startMousePosition;
    [SerializeField] private Vector3 _currentMousePos;
    [SerializeField] private Vector3 _cameraNewPosition;

    [Header("Для приближения камеры двумя пальцами")]
    [SerializeField] private Vector3 _startFirstFingerPosition;
    [SerializeField] private Vector3 _startSecondFingerPosition;
    [SerializeField] private Vector3 _currentFirstFingerPosition;
    [SerializeField] private Vector3 _currentSecondFingerPosition;

    [Header("Sensitivity")]
    [SerializeField] private float _sensitivity = 5f;

    [Header("Confines")]
    [SerializeField] private float _minX = -100, _maxX = 100;
    [SerializeField] private float _minY = -100, _maxY = 100;
    [SerializeField] private float _minZ = -100, _maxZ = 100;

    [Header("Camera State")]
    public bool IsLock;

    private float _newXPos;
    private float _newYPos;
    private float _newZPos;

    private float _oldXPos;
    private float _oldYPos;
    private float _oldZPos;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        WindowsControl();
        AndroidControl();
    }

    public void WindowsControl()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startMousePosition = Input.mousePosition;
                _oldXPos = _newXPos;
                _oldZPos = _newZPos;
            }

            if (Input.GetMouseButton(0))
            {
                // Чтобы работало правильно, надо сделать камере родительский пустой объект и установить нужны наклон. А дальше все будет работать в локльных координатах
                _currentMousePos = Input.mousePosition;
                _newXPos = _oldXPos - (_currentMousePos.x - _startMousePosition.x) * _sensitivity / 100;
                _newZPos = _oldZPos - (_currentMousePos.y - _startMousePosition.y) * _sensitivity / 100;
                float newClampXPos = Mathf.Clamp(_newXPos, _minX, _maxX);
                float newClampZPos = Mathf.Clamp(_newZPos, _minZ, _maxZ);
                _cameraNewPosition = new Vector3(newClampXPos, _camera.transform.localPosition.y, newClampZPos);

                _camera.transform.localPosition = _cameraNewPosition;
            }
        }
    }

    public void AndroidControl()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount < 2)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        _startMousePosition = touch.position;
                        _oldXPos = _newXPos;
                        _oldZPos = _newZPos;
                    }

                    if (touch.phase == TouchPhase.Moved)
                    {
                        _currentMousePos = touch.position;
                        _newXPos = _oldXPos - (_currentMousePos.x - _startMousePosition.x) * _sensitivity;
                        _newZPos = _oldZPos - (_currentMousePos.y - _startMousePosition.y) * _sensitivity;
                        float newClampXPos = Mathf.Clamp(_newXPos, _minX, _maxX);
                        float newClampZPos = Mathf.Clamp(_newZPos, _minZ, _maxZ);
                        _cameraNewPosition = new Vector3(newClampXPos, _camera.transform.localPosition.y, newClampZPos);
                        _camera.transform.localPosition = _cameraNewPosition;
                    }

                }
            }
            else
            {
                for (int i = 0; i < Input.touches.Length; i++)
                {
                    Touch[] touch = Input.touches;
                    // Приближение камеры двумя пальцами
                    if (touch[i].phase == TouchPhase.Began)
                    {
                        _startFirstFingerPosition = touch[0].position;
                        _startSecondFingerPosition = touch[1].position;
                        _oldYPos = _newYPos;
                    }

                    if (touch[i].phase == TouchPhase.Moved)
                    {
                        _currentFirstFingerPosition = touch[0].position;
                        _currentSecondFingerPosition = touch[1].position;
                        _newYPos = _oldYPos - (_currentFirstFingerPosition.x - _currentSecondFingerPosition.x) * _sensitivity;// Найти способ приближения экрана
                        float newClampYPos = Mathf.Clamp(_newYPos, _minY, _maxY);
                        _cameraNewPosition = new Vector3(_camera.transform.localPosition.x, newClampYPos, _camera.transform.localPosition.z);
                        _camera.transform.localPosition = _cameraNewPosition;
                    }
                }
            }
        }
    }
}
