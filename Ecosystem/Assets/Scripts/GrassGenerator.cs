using System.Collections.Generic;
using UnityEngine;

public class GrassGenerator : MonoBehaviour
{
    [SerializeField] private float _growTime;
    [SerializeField] private GameObject _grassPrefab;
    private float _currentGrowTime;

    private void Update()
    {
        _currentGrowTime -= Time.deltaTime;

        if (_currentGrowTime <= 0f)
        {
            Instantiate(_grassPrefab, , Quaternion.identity);
        }
    }
}
