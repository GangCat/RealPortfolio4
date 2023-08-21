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
        gate.OpenGate();
    }

    private void Awake()
    {
        gate = GetComponentInChildren<Gate>();
    }

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
    }

    private Gate gate = null;
}
