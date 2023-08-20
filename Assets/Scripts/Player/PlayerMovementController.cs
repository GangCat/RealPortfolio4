using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public bool CanDash => canDash;

    public void MoveUpdate(float _x, float _z, bool _isRun)
    {

        moveDir = new Vector3(_x, 0f, _z).normalized;

        if (weaponAR.IsAttack || weaponAR.IsReload)
        {
            playerRigidbody.velocity = moveDir * statusSpeed.WalkSpeed;
            SetMoveDirWhileAttack(moveDir);
        }
        else
            playerRigidbody.velocity = moveDir * (_isRun ? statusSpeed.RunSpeed : statusSpeed.WalkSpeed);

    }

    public void MoveLerp(float _x, float _z, bool _isRun)
    {
        moveDir = new Vector3(_x, 0f, _z).normalized;

        moveDir = Vector3.Slerp(moveDir, Vector3.zero, decelerationRate * Time.deltaTime);

        if (weaponAR.IsAttack || weaponAR.IsReload)
            playerRigidbody.velocity = moveDir * statusSpeed.WalkSpeed;
        else
            playerRigidbody.velocity = moveDir * (_isRun ? statusSpeed.RunSpeed : statusSpeed.WalkSpeed);

    }

    private void SetMoveDirWhileAttack(Vector3 _moveDir)
    {
        Matrix4x4 matRot = Matrix4x4.Rotate(transform.rotation);
        matRot = matRot.inverse;
        _moveDir = matRot.MultiplyPoint3x4(_moveDir).normalized;

        playerAnim.SetDirWhileAttack(_moveDir);
    }

    public void DashUpdate(float _x, float _z)
    {
        canDash = false;

        if (weaponAR.IsAttack)
        {
            moveDir = new Vector3(_x, 0f, _z).normalized;
            transform.forward = moveDir;
        }

        StartCoroutine("OnDash", transform.forward);
    }

    private IEnumerator OnDash(Vector3 _moveDir)
    {
        playerAnim.PlayDash();
        weaponAR.ChangeState(EWeaponState.Idle);
        playerCollider.IgnoreLayer(gameObject.layer, LayerMask.NameToLayer("Enemy"), true);
        playerCollider.IgnoreLayer(gameObject.layer, LayerMask.NameToLayer("Interactive"), true);
        playerCollider.IgnoreLayer(gameObject.layer, LayerMask.NameToLayer("Boss"), true);
        playerCollider.IgnoreLayer(gameObject.layer, LayerMask.NameToLayer("Obstacle"), true);

        float lastDashTime = Time.time;
        while (true)
        {
            while (isPaused) // 일시정지 상태일 때 대기
            {
                playerRigidbody.velocity = Vector3.zero;
                CalcCanDash(ref lastDashTime);
                yield return null;
            }

            if (CalcCanDash(ref lastDashTime))
            {
                Debug.Log("dash");
                canDash = true;
                yield break;
            }

            yield return null;

            if (playerAnim.CurAnimationIs("Dash"))
                playerRigidbody.velocity = _moveDir * dashSpeed;
            else
            {
                if (playerAnim.GetBool("isAttack"))
                    weaponAR.ChangeState(EWeaponState.Attack);

                playerCollider.IgnoreLayer(gameObject.layer, LayerMask.NameToLayer("Enemy"), false);
                playerCollider.IgnoreLayer(gameObject.layer, LayerMask.NameToLayer("Interactive"), false);
                playerCollider.IgnoreLayer(gameObject.layer, LayerMask.NameToLayer("Boss"), false);
                playerCollider.IgnoreLayer(gameObject.layer, LayerMask.NameToLayer("Obstacle"), false);
            }
        }
    }

    private bool CalcCanDash(ref float _lastDashTime)
    {
        if (isPaused)
            _lastDashTime += Time.deltaTime;

        return dashRate < (Time.time - _lastDashTime);
    }

    public void RotateUpdate(float _x, float _z, bool _mouseDown)
    {
        moveDir = new Vector3(_x, 0.0f, _z).normalized;
        if (_mouseDown)
        {
            RaycastHit hit;
            Picking(out hit);

            Vector3 point = hit.point;
            point.y = 0.0f;

            if (Vector3.SqrMagnitude(point - transform.position) > 1.5f) // 너무 가까운 곳을 찍으면 캐릭터가 누워버림
            {
                float theta = MyMathf.CalcAngleToTarget(transform.position, point);
                //transform.rotation = Quaternion.LookRotation(point - transform.position);
                transform.rotation = Quaternion.Euler(0f, -theta + 90, 0f);
                //transform.LookAt(point);
            }
        }
        else if (moveDir != Vector3.zero)
            transform.forward = moveDir;
    }

    private void Awake()
    {
        playerAnim = GetComponent<PlayerAnimatorController>();
        weaponAR = GetComponentInChildren<WeaponAssaultRifle>();
        statusSpeed = GetComponent<StatusSpeed>();
        playerCollider = GetComponentInChildren<PlayerCollider>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    public bool Picking(out RaycastHit _hit)
    {
        Vector2 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out _hit, 1000.0f, layerForPicking))
            return true;
        else
            return false;
    }

    public void CheckPaused(bool _isPaused)
    {
        isPaused = _isPaused;

        if (isPaused)
            playerRigidbody.velocity = Vector3.zero;
    }

    [SerializeField]
    private float dashSpeed = 10.0f;
    [SerializeField]
    private float dashRate = 3.0f;
    [SerializeField]
    private LayerMask layerForPicking;

    private float decelerationRate = 2.0f;

    private bool canDash = true;
    private bool isPaused = false;

    private Vector3 moveDir = Vector3.zero;

    private StatusSpeed statusSpeed = null;
    private PlayerAnimatorController playerAnim = null;
    private WeaponAssaultRifle weaponAR = null;
    private PlayerCollider playerCollider = null;
    private Rigidbody playerRigidbody = null;
}
