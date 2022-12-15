using UnityEngine;

public class TouchSpawn : MonoBehaviour
{
    [SerializeField] private ItemSelector _itemSelector;
    [SerializeField] private ParticleSystem _spawnEffect;

    private Vector3 _startMousePos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && Input.mousePosition.y > 146f && Input.mousePosition.y < 670f)
        {
            if (_itemSelector.GetSelectorState() == false)
            {
                if (Input.mousePosition == _startMousePos || Input.mousePosition == _startMousePos)
                {
                    if (_itemSelector)
                    {
                        _spawnEffect.Play();
                    }
                }
                //else
                //{
                //    _itemSelector.SetSelectorState(true); Ќужно дл€ блокировки во врем€ передвижени€ камеры
                //}
            }
        }
    }
}
