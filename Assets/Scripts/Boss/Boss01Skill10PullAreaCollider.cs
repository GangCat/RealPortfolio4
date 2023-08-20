using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01Skill10PullAreaCollider : MonoBehaviour
{
    private void OnTriggerStay(Collider _other)
    {
        if(_other.CompareTag("Player"))
            _other.GetComponent<Rigidbody>().AddForce((transform.position - _other.transform.position) * 4f, ForceMode.Force);
    }

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
    }
}
