using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    [SerializeField] private int _selectedNumber = 1;
    [SerializeField] private Item[] _items;

    public int GetNumber()
    {
        return _selectedNumber;
    }

    public void Select(Item item)
    {
        _selectedNumber = item.Number;
    }

    public GameObject GetSelectedItem()
    {
        return _items[_selectedNumber].gameObject;
    }

    public void DeselectAll()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].DeselectColor();
        }
    }
}
