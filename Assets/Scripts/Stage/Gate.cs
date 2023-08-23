using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public void Init()
    {
        door = GetComponentInChildren<GateDoor>();
        door.Init();
    }

    public void OpenGate()
    {
        door.OpenGateDoor();
    }

    public void CloseGate()
    {
        door.CloseGateDoor();
    }

    private GateDoor door = null;
}
