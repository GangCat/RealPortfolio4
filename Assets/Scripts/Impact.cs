using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void Setup(MemoryPool _pool)
    {
        memoryPool = _pool;
    }

    private void Update()
    {
        if (!particle.isPlaying && memoryPool != null)
        {
            memoryPool.DeactivatePoolItem(gameObject);
        }
    }

    private ParticleSystem particle;
    private MemoryPool memoryPool;
}