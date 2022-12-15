using UnityEngine;

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
        if (Input.GetMouseButtonDown(0))
        {
            _startMousePosition = Input.mousePosition;
            _oldXPos = _newXPos;
            _oldZPos = _newZPos;
        }

        if (Input.GetMouseButton(0))
        {
            // „тобы работало правильно, надо сделать камере родительский пустой объект и установить нужны наклон. ј дальше все будет работать в локльных координатах
            _currentMousePos = Input.mousePosition;
            _newXPos = _oldXPos - (_currentMousePos.x - _startMousePosition.x) * _sensitivity / 100;
            _newZPos = _oldZPos - (_currentMousePos.y - _startMousePosition.y) * _sensitivity / 100;
            _cameraNewPosition = new Vector3(_newXPos, _camera.transform.localPosition.y, _newZPos);

            _camera.transform.localPosition = _cameraNewPosition;
        }
    }
}
