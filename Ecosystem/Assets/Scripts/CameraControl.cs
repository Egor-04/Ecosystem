using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector3 _startMousePosition;
    [SerializeField] private Vector3 _currentMousePos;
    [SerializeField] private Vector3 _cameraNewPosition;

    [Header("Sensitivity")]
    [SerializeField] private float _sensitivity = 5f;

    [Header("Confines")]
    [SerializeField] private float _minX = -100, _maxX = 100;
    [SerializeField] private float _minZ = -100, _maxZ = 100;

    [Header("Camera State")]
    public bool IsLock;

    private float _newXPos;
    private float _newZPos;

    private float _oldXPos;
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
                // ����� �������� ���������, ���� ������� ������ ������������ ������ ������ � ���������� ����� ������. � ������ ��� ����� �������� � �������� �����������
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
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x > Screen.width && touch.phase == TouchPhase.Began)
                {
                    _startMousePosition = touch.position;
                    _oldXPos = _newXPos;
                    _oldZPos = _newZPos;
                }

                if (touch.position.x > Screen.height && touch.phase == TouchPhase.Moved)
                {
                    _currentMousePos = touch.position;
                    _newZPos = _oldXPos - (_currentMousePos.y - _startMousePosition.y) * _sensitivity;
                    _newXPos = _oldZPos + (_currentMousePos.x - _startMousePosition.x) * _sensitivity;
                    float newClampXPos = Mathf.Clamp(_newXPos, _minX, _maxX);
                    float newClampZPos = Mathf.Clamp(_newZPos, _minZ, _maxZ);
                    _cameraNewPosition = new Vector3(newClampXPos, _camera.transform.localPosition.y, newClampZPos);
                    _camera.transform.position = _cameraNewPosition;
                }

                if (Input.touchCount == 2)
                {
                    Debug.LogError("Work");
                }
            }
        }
    }
}
