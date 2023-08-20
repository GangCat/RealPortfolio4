using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour, IPauseObserver
{
    public abstract void Use(GameObject _entity);

    protected IEnumerator Start()
    {
        gameManager.RegisterPauseObserver(GetComponent<IPauseObserver>());

        float y = transform.position.y;

        while (true)
        {
            while (isPaused)
                yield return null;

            transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);

            Vector3 pos = transform.position;
            pos.y = Mathf.Lerp(y, y + moveDistance, Mathf.PingPong(Time.time * pingpongSpeed, 1f));
            transform.position = pos;

            yield return null;
        }
    }

    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void CheckPaused(bool _isPaused)
    {
        isPaused = _isPaused;
    }

    [SerializeField]
    protected float moveDistance = 0.2f;
    [SerializeField]
    protected float pingpongSpeed = 0.5f;
    [SerializeField]
    protected float rotSpeed = 50f;

    protected bool isPaused = false;

    protected GameManager gameManager = null;
}