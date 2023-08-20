using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToNextStage : MonoBehaviour
{
    public OnPlayerMoveToNextStageDelegate OnPlayerMoveToNextStageCallback
    {
        set => onPlayerMoveToNextStageCallback = value;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            onPlayerMoveToNextStageCallback?.Invoke();
            _other.transform.position += Vector3.forward * 15f;
        }
    }

    private OnPlayerMoveToNextStageDelegate onPlayerMoveToNextStageCallback = null;
}
