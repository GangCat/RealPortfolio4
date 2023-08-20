using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleToNextStage : InteractiveBase
{
    public void ActivateTrigger()
    {
        GetComponent<Collider>().enabled = true;
    }

    public override void Use()
    {
        door.OpenDoor();
    }

    private void Awake()
    {
        door = GetComponentInChildren<MoveGate>();
    }

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
    }

    private MoveGate door = null;
}
