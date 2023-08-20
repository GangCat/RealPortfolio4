using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactAutoDestroy : MonoBehaviour
{
    private void Update()
    {
        if (!particle.isPlaying)
            Destroy(gameObject);
    }

    private ParticleSystem particle;
}