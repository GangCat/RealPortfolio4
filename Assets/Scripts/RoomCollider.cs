using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngageEvent : UnityEngine.Events.UnityEvent<Transform> { }
public class RoomCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            onEngageEvent.Invoke(_collision.transform);
        }
    }

    [HideInInspector]
    public EngageEvent onEngageEvent = new EngageEvent();
}
