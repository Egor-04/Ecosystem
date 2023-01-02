using UnityEngine;
using UnityEngine.EventSystems;

public class DragArea : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private Transform _parent;
    [SerializeField] private UIManager _uiManager;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _uiManager.SetCameraState(true);
        Vector2 newPosition = new Vector2(Mathf.Clamp(Input.mousePosition.x, 25f, 1256), Mathf.Clamp(Input.mousePosition.y, 22, 700));
        _parent.transform.position = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _uiManager.SetCameraState(false);
    }
}
