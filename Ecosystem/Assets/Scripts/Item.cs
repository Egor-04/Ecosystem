using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour, IPointerDownHandler
{
    public int Number;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _standartColor;
    [SerializeField] private Image _image;
    [SerializeField] private ItemSelector _itemSelector;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _standartColor = _image.color;
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
