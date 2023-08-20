using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorToNextStage : MonoBehaviour
{
    public EDoorDir DoorDir => doorDir;

    public void Init(OnPlayerMoveToNextStageDelegate _callback)
    {
        trigger = GetComponentInChildren<TriggerToNextStage>();
        console = GetComponentInChildren<ConsoleToNextStage>();

        trigger.OnPlayerMoveToNextStageCallback = _callback;
    }

    public void ActivateDoorTrigger()
    {
        console.ActivateTrigger();
    }

    [SerializeField]
    private EDoorDir doorDir = EDoorDir.None;

    private TriggerToNextStage trigger = null;
    private ConsoleToNextStage console = null;
}
