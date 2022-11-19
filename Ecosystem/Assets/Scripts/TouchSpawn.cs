using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSpawn : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private ItemSelector _itemSelector;
    [SerializeField] private ParticleSystem _spawnEffect;
    [SerializeField] private List<ParticleCollisionEvent> _collisionEvents;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.y > 146f)
        {
            if (_itemSelector.GetNumber() != 0)
            {
                _spawnEffect.Play();
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == _layerMask)
        {
            Debug.LogError("SSSS");
            int collisionNumbers = _spawnEffect.GetCollisionEvents(other, _collisionEvents);

            int i = 0;

            while (i < collisionNumbers)
            {
                Vector3 position = _collisionEvents[i].intersection - _collisionEvents[i].normal * Random.Range(0.3f, 0.8f);
                Instantiate(_itemSelector, position, Quaternion.identity);
                i++;
            }
        }
    }
}
