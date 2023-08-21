using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour, IPauseObserver
{
    public void Setup(MemoryPool _memoryPool, float _dmg, ImpactMemoryPool _impactPool, RetVoidRaramIntDelegate _callback = null)
    {
        impactPool = _impactPool;
        memoryPool = _memoryPool;
        dmg = _dmg;
        onEnemyDamagedCallback = _callback;
    }

    private void OnEnable()
    {
        if(trail != null)
            trail.TrailClear();

        StartCoroutine("AutoDisable");
    }

    private IEnumerator AutoDisable()
    {
        float curTime = Time.time;

        while (autoDisableTime > Time.time - curTime)
        {
            if (isPaused)
                autoDisableTime += Time.deltaTime;

            yield return null;
        }
        Disable();
    }

    private void Disable()
    {
        if(memoryPool != null)
            memoryPool.DeactivatePoolItem(gameObject);
    }

    private void Update()
    {
        if(!isPaused)
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision != null)
        {
            if (_collision.transform.CompareTag("Enemy"))
            {
                SpawnImpact(_collision, -transform.forward);
                _collision.transform.GetComponent<EnemyController>().TakeDmg(dmg);
                onEnemyDamagedCallback?.Invoke((int)dmg);
            }
            else if (_collision.transform.CompareTag("Obstacle"))
            {
                SpawnImpact(_collision, -transform.forward);
            }
            else if (_collision.transform.CompareTag("Destructible"))
            {
                SpawnImpact(_collision, -transform.forward);
                _collision.transform.GetComponent<DestructibleObject>().TakeDmg(dmg);
            }
            else if (_collision.transform.CompareTag("Player"))
            {
                _collision.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                _collision.transform.GetComponent<PlayerCollider>().TakeDmg(dmg);
            }
            else if (_collision.transform.CompareTag("Wall"))
            {
                SpawnImpact(_collision, -transform.forward);
            }
            else if (_collision.transform.CompareTag("Boss"))
            {
                SpawnImpact(_collision, -transform.forward);
                _collision.transform.GetComponent<BossCollider>().TakeDmg(dmg);
            }

        }

        Disable();
    }

    private void SpawnImpact(Collision _collision, Vector3 _dir)
    {
        if (gameObject != null)
            impactPool.SpawnInit(_collision.GetContact(0).point, _dir);
    }

    public void CheckPaused(bool _isPaused)
    {
        isPaused = _isPaused;
        trail.IsPaused(isPaused);
    }

    private void Awake()
    {
        gameManager = GameManager.Instance;
        trail = GetComponent<ProjectileTrail>();
    }

    private void Start()
    {
        gameManager.RegisterPauseObserver(GetComponent<IPauseObserver>());
    }

    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float autoDisableTime = 0.5f;

    private bool isPaused = false;

    private float dmg = 0;

    private MemoryPool memoryPool = null;
    private ImpactMemoryPool impactPool = null;
    private GameManager gameManager = null;
    private ProjectileTrail trail = null;
    private RetVoidRaramIntDelegate onEnemyDamagedCallback = null;
}
