using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallController : MonoBehaviour
{
    public void Setdmg(float _dmg)
    {
        dmg = _dmg;
    }

    private void Start()
    {
        StartCoroutine("AutoDestroy");
    }

    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(autoDestroyTime);

        if(gameObject != null)
            Destroy(gameObject);
    }

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            _other.GetComponent<PlayerCollider>().TakeDmg(dmg);
            Destroy(gameObject);
        }
        else if(_other.CompareTag("Floor"))
        {
            GameObject energyAreaGo = Instantiate(energyFieldPrefab, transform.position, Quaternion.identity);
            energyAreaGo.GetComponent<EnergyTickDamageArea>().Setup(dmg * 0.1f);
            energyAreaGo.GetComponent<EnergyTickDamageArea>().OnAttack();
            Destroy(gameObject);
        }
    }



    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float autoDestroyTime;
    [SerializeField]
    private GameObject energyFieldPrefab;

    private float dmg;
}
