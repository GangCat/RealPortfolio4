using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public void ActivateDoorTrigger()
    {
        foreach(DoorToNextStage door in doors)
            door.ActivateDoorTrigger();
    }

    public Vector3 GetMinSpawnPoint()
    {
        return minSpawnPoint.GetPosition();
    }

    public Vector3 GetMaxSpawnPoint()
    {
        return maxSpawnPoint.GetPosition();
    }

    public void Init(OnPlayerMoveToNextStageDelegate _callback)
    {
        doors = GetComponentsInChildren<DoorToNextStage>();
        
        foreach(DoorToNextStage door in doors)
            door.Init(_callback);
    }

    private DoorToNextStage[] doors = null;

    [SerializeField]
    private MinSpawnPoint minSpawnPoint = null;
    [SerializeField]
    private MaxSpawnPoint maxSpawnPoint = null;
}
