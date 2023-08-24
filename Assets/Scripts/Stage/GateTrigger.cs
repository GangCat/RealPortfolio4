using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public void Init( RetVoidParamVoidDelegate _playerEnterGateTriggerCallback)
    {
        playerEnterGateTriggerCallback = _playerEnterGateTriggerCallback;
        
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
            playerEnterGateTriggerCallback?.Invoke();
    }

    private RetVoidParamVoidDelegate playerEnterGateTriggerCallback = null;
}
