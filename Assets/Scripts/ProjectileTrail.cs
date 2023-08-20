using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrail : MonoBehaviour
{
    public void IsPaused(bool _isPaused)
    {
        if (gameObject.activeSelf)
        {
            if (_isPaused)
                StartCoroutine("Delay");
            else
            {
                StopCoroutine("Delay");
                trailRenderer.time = trailTime;
            }
        }
    }

    public void TrailClear()
    {
        if (trailRenderer == null)
            return;

        trailRenderer.Clear();
    }

    private IEnumerator Delay()
    {
        while (true)
        {
            trailRenderer.time += Time.deltaTime;
            yield return null;
        }
    }

    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }


    [SerializeField]
    private float trailTime = 0.05f;

    private TrailRenderer trailRenderer = null;
}
