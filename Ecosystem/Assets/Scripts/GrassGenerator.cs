using UnityEngine;

public class GrassGenerator : MonoBehaviour
{
    [SerializeField] private int _grassCount;
    [SerializeField] private float _growTime;
    [SerializeField] private float _rayDistance;
    [SerializeField] private GameObject _grassPrefab;
    [SerializeField] private GameObject _plane;
    [SerializeField] private float _currentGrowTime;

    private void Update()
    {
        _currentGrowTime -= Time.deltaTime;

        if (_currentGrowTime <= 0f)
        {
            for (int i = 0; i < _grassCount; i++)
            {
                float randomX = Random.Range(-1000f, 1000f);
                float randomZ = Random.Range(-1000f, 1000f);

                RaycastHit hit;
                if (Physics.Raycast(new Vector3(randomX, 100, randomZ), Vector3.down, out hit, _rayDistance, LayerMask.GetMask("Terrain"))) // Пуск луча от высоты для попадания на террейн
                {
                    Vector3 grassSpawnPoint = hit.point;
                    Instantiate(_grassPrefab, grassSpawnPoint, Quaternion.identity);
                }
            }

            _currentGrowTime = _growTime;
        }
    }
}
