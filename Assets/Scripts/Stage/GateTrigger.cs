using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public void Init(RetVoidParamVoidDelegate _playerEnterTriggerCallback)
    {
        playerEnterTriggerCallback = _playerEnterTriggerCallback;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
            playerEnterTriggerCallback?.Invoke();
    }

    private RetVoidParamVoidDelegate playerEnterTriggerCallback = null;
}
