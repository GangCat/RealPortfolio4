using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public void Init(
        RetVoidParamVoidDelegate _playerEnterTriggerCallback,
        RetVoidParamVoidDelegate _playerExitTriggerCallback)
    {
        playerEnterTriggerCallback = _playerEnterTriggerCallback;
        playerExitTriggerCallback = _playerExitTriggerCallback;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            playerExitTriggerCallback?.Invoke();
            playerEnterTriggerCallback?.Invoke();
        }
    }

    private RetVoidParamVoidDelegate playerEnterTriggerCallback = null;
    private RetVoidParamVoidDelegate playerExitTriggerCallback = null;
}
