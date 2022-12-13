using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawnPoint : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private ItemSelector _itemSelector;
    [SerializeField] private ParticleSystem _spawnEffect;
    [SerializeField] private List<ParticleCollisionEvent> _collisionEvents = new List<ParticleCollisionEvent>();

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Ground"))
        {
            int collisionNumbers = _spawnEffect.GetCollisionEvents(other, _collisionEvents);

            int i = 0;

            while (i < collisionNumbers)
            {
                Vector3 position = _collisionEvents[i].intersection - _collisionEvents[i].normal * Random.Range(0.3f, 0.8f);
                Instantiate(_itemSelector.GetSelectedItem(), position, Quaternion.identity);
                i++;
            }
        }
    }
}
