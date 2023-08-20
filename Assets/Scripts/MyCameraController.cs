using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour, IStageObserver
{
    public void CheckStage(int _curStage)
    {
        transform.position += Vector3.forward * 60f;
    }

    private void Start()
    {
        GameManager.Instance.RegisterStageobserver(GetComponent<IStageObserver>());
    }
}
