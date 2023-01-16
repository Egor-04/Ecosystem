using UnityEngine;
using UnityEngine.EventSystems;

public class DragArea : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private Transform _parent;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private ItemSelector _itemSelector;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _itemSelector = FindObjectOfType<ItemSelector>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _itemSelector.LockState();
        _uiManager.SetCameraState(true);
        Vector2 newPosition = new Vector2(Mathf.Clamp(Input.mousePosition.x, -0.0002f *Screen.width, Screen.width), Mathf.Clamp(Input.mousePosition.y, -0.0002f * Screen.height, Screen.height));
        _parent.transform.position = newPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _uiManager.SetCameraState(false);
    }
}
