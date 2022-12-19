using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    [SerializeField] private bool _isLocked;
    [SerializeField] private int _selectedNumber = 1;
    [SerializeField] private Item[] _items;

    public void SetSelectorState(bool state)
    {
        _isLocked = state;
    }

    public bool GetSelectorState()
    {
        return _isLocked;
    }

    public bool ItemIsSelected()
    {
        return _items[_selectedNumber].GetItemState();
    }

    public int GetNumber()
    {
        return _selectedNumber;
    }

    public void Select(Item item)
    {
        //SetSelectorState(false); Если нужно блокировать спавн объектов вовремя передвижения
        item.SetItemState(true);
        _selectedNumber = item.Number;
    }

    public GameObject GetSelectedItem()
    {
        return _items[_selectedNumber].GetItemPrefab();
    }

    public Creature GetCreature()
    {
        return _items[_selectedNumber].GetCreature();
    }

    public void DeselectAll()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].SetItemState(false);
            _items[i].DeselectColor();
        }
    }
}
