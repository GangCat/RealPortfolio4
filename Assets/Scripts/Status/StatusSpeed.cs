using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEvent : UnityEngine.Events.UnityEvent<float, float> { }
public class StatusSpeed : MonoBehaviour
{
    public SpeedEvent onSpeedEvent = new SpeedEvent();
    public float RunSpeed => runSpeed;
    public float WalkSpeed => walkSpeed;

    #region BuffSpeed
    public void BuffSpeed(float _ratio, float _duration)
    {
        if (isBuff)
            StopCoroutine("ResetSpeed");

        isBuff = true;

        walkSpeed = walkSpeed * _ratio > maxWalkSpeed ? maxWalkSpeed : walkSpeed * _ratio;
        runSpeed = runSpeed * _ratio > maxRunSpeed ? maxRunSpeed : runSpeed * _ratio;

        onSpeedEvent.Invoke(walkSpeed, runSpeed);

        StartCoroutine("ResetSpeed", _duration);
    }

    private IEnumerator ResetSpeed(float _duration)
    {
        yield return new WaitForSeconds(_duration);

        runSpeed = oriRunSpeed;
        walkSpeed = oriWalkSpeed;

        onSpeedEvent.Invoke(walkSpeed, runSpeed);
        isBuff = false;
    }
    #endregion

    public void ChangeSpeed(float _ratioSpeed)
    {
        walkSpeed = oriWalkSpeed * ( 1 + _ratioSpeed);
        runSpeed = oriRunSpeed * (1 + _ratioSpeed);

        onSpeedEvent.Invoke(walkSpeed, runSpeed);
    }

    private void Start()
    {
        oriWalkSpeed = walkSpeed;
        oriRunSpeed = runSpeed;
    }


    [Header("-Movement Speed")]
    [SerializeField]
    private float runSpeed = 10.0f;
    [SerializeField]
    private float walkSpeed = 6.0f;
    [SerializeField]
    private float maxRunSpeed = 25.0f;
    [SerializeField]
    private float maxWalkSpeed = 15.0f;

    private bool isBuff = false;

    private float oriWalkSpeed;
    private float oriRunSpeed;
}
