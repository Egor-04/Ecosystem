using UnityEngine;
using TMPro;

public class CameraControl : MonoBehaviour
{
    [Header("General Variables")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _cameraMover;
    [SerializeField] private Vector3 _cameraNewPosition;


    [Header("Control for PC and Android")]
    [SerializeField] private Vector3 _startMousePosition;
    [SerializeField] private Vector3 _currentMousePos;

    [Header("Camera Zoom PC and Android")]
    [SerializeField] private Vector3 _startZoomMousePos;
    [SerializeField] private Vector3 _currentZoomMousePos;

    [Header("Zoom for Android")]
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Touch _currTouchA;
    [SerializeField] private Touch _currTouchB;
    [SerializeField] private Vector2 _lastTouchA;
    [SerializeField] private Vector2 _lastTouchB;
    [SerializeField] private float _currDistance;
    [SerializeField] private float _lastDistance;
    [SerializeField] private float _zoomValue;

    [Header("Sensitivity")]
    [SerializeField] private float _sensitivity = 5f;

    [Header("Confines")]
    [SerializeField] private float _minX = -100, _maxX = 100;
    [SerializeField] private float _minZ = -100, _maxZ = 100;

    [Header("Camera State")]
    public bool IsLock;

    // For Camera Move Control
    private float _newXPos;
    private float _newZPos;

    private float _oldXPos;
    private float _oldZPos;

    // For Camera Zoom
    [SerializeField] private float _minZoomZPos = -100, _maxZoomZPos = 100;
    private float _newZoomCamZPos;
    private float _oldZoomCamZPos;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _cameraMover = transform.parent;
    }

    private void Update()
    {
        WindowsControl();
        ZoomCameraWindows();

        AndroidControl();
        ZoomCameraAndroid();
    }

    public void WindowsControl()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            //Camera Move Control
            if (Input.GetMouseButtonDown(0))
            {
                _startMousePosition = Input.mousePosition;
                float newClampXPos = Mathf.Clamp(_newXPos, _minX, _maxX);
                float newClampZPos = Mathf.Clamp(_newZPos, _minZ, _maxZ);
                _oldXPos = newClampXPos;
                _oldZPos = newClampZPos;
            }

            if (Input.GetMouseButton(0))
            {
                // „тобы работало правильно, надо сделать камере родительский пустой объект и установить нужны наклон. ј дальше все будет работать в локльных координатах
                _currentMousePos = Input.mousePosition;
                _newXPos = _oldXPos - (_currentMousePos.x - _startMousePosition.x) * _sensitivity / 100;
                _newZPos = _oldZPos - (_currentMousePos.y - _startMousePosition.y) * _sensitivity / 100;
                float newClampXPos = Mathf.Clamp(_newXPos, _minX, _maxX);
                float newClampZPos = Mathf.Clamp(_newZPos, _minZ, _maxZ);
                _cameraNewPosition = new Vector3(newClampXPos, _cameraMover.transform.localPosition.y, newClampZPos);
                _cameraMover.transform.localPosition = _cameraNewPosition;
            }
        }
    }

    private void ZoomCameraWindows()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _startMousePosition = Input.mousePosition;
            float newClampZPos = Mathf.Clamp(_newZoomCamZPos, _minZoomZPos, _maxZoomZPos);
            _oldZoomCamZPos = newClampZPos;
        }

        if (Input.GetMouseButton(1))
        {
            _currentMousePos = Input.mousePosition;
            _newZoomCamZPos = _oldZoomCamZPos - (_currentMousePos.y - _startMousePosition.y) * _sensitivity / 100;
            float newClampZPos = Mathf.Clamp(_newZoomCamZPos, _minZoomZPos, _maxZoomZPos);
            _cameraNewPosition = new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y, newClampZPos);
            _camera.transform.localPosition = _cameraNewPosition;
        }
    }

    private void AndroidControl()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    _startMousePosition = touch.position;
                    float newClampXPos = Mathf.Clamp(_newXPos, _minX, _maxX);
                    float newClampZPos = Mathf.Clamp(_newZPos, _minZ, _maxZ);
                    _oldXPos = newClampXPos;
                    _oldZPos = newClampZPos;
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    _currentMousePos = touch.position;
                    _newXPos = _oldXPos - (_currentMousePos.x - _startMousePosition.x) * _sensitivity / 100;
                    _newZPos = _oldZPos - (_currentMousePos.y - _startMousePosition.y) * _sensitivity / 100;
                    float newClampXPos = Mathf.Clamp(_newXPos, _minX, _maxX);
                    float newClampZPos = Mathf.Clamp(_newZPos, _minZ, _maxZ);
                    _cameraNewPosition = new Vector3(newClampXPos, _cameraMover.transform.localPosition.y, newClampZPos);
                    _cameraMover.transform.localPosition = _cameraNewPosition;
                }
            }
        }
    }

    private void ZoomCameraAndroid()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount == 2)
            {
                _currTouchA = Input.GetTouch(0);
                _currTouchB = Input.GetTouch(1);

                _lastTouchA = _currTouchA.position - _currTouchA.deltaPosition;
                _lastTouchB = _currTouchB.position - _currTouchB.deltaPosition;

                _currDistance = Vector2.Distance(_currTouchA.position, _currTouchB.position);
                _lastDistance = Vector2.Distance(_lastTouchA, _lastTouchB);

                _zoomValue = _lastDistance - _currDistance;
                float clampZoomValue = Mathf.Clamp(_zoomValue, _minZoomZPos, _maxZoomZPos);
                    
                _text.text = clampZoomValue.ToString();
                _cameraNewPosition = new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y, clampZoomValue);
                _camera.transform.localPosition = _cameraNewPosition;
            }
            else
            {
                _currDistance = 0f;
                _lastDistance = 0f;
            }
        }
    }
}
