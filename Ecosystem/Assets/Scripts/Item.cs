using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Item : MonoBehaviour, IPointerDownHandler
{
    public int Number;
    [SerializeField] private bool _isLocker;
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _standartColor;
    [SerializeField] private Image _image;
    [SerializeField] private ItemSelector _itemSelector;
    
    private Creature _creature;
    private bool _isSelectedNow;


    private void Awake()
    {
        _container = transform.parent;

        for (int i = 0; i < _container.childCount; i++)
        {
            if (gameObject.name == _container.GetChild(i).name)
            {
                Number = i;
            }
        }

        if (_objectPrefab)
        {
            if (_objectPrefab.GetComponent<Creature>())
            {
                _creature = _objectPrefab.GetComponent<Creature>();
            }
        }

        _image = GetComponent<Image>();
    }

    public void SetItemState(bool state)
    {
        _isSelectedNow = state;
    }

    public bool GetItemState()
    {
        return _isSelectedNow;
    }

    public GameObject GetItemPrefab()
    {
        return _objectPrefab;
    }

    public Creature GetCreature()
    {
        return _creature;
    }

    public void SelectColor()
    {
        _image.color = _selectedColor;
    }

    public void DeselectColor()
    {
        _image.color = _standartColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isLocker)
        {
            _itemSelector.LockState();
        }
        else
        {
            _itemSelector.UnlockState();
        }

        _itemSelector.DeselectAll();
        _itemSelector.Select(this);

        if (_image.color == _standartColor)
        {
            SelectColor();
        }
        else
        {
            DeselectColor();
        }
    }
}
