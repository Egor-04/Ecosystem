using UnityEngine;

public class CanvasHolder : MonoBehaviour
{
    [SerializeField] private float _vertical = -53;
    [SerializeField] private float _horizontal = -53;
    [SerializeField] private Transform _canvas;

    private void Start()
    {
        _canvas = transform;
    }

    private void Update()
    {
        _canvas.transform.eulerAngles = new Vector3(transform.eulerAngles.x, _horizontal, transform.eulerAngles.z);
    }
}
