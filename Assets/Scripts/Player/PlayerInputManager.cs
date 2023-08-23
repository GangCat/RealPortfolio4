using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour, IPauseObserver, IBossEngageObserver
{
    public bool IsInteract
    {
        get
        {
            if (isInteract && !isSelected)
            {
                isSelected = true;
                return true;
            }
            return false;
        }
    }

    public bool IsSellCrystal => isSellCrystal;

    public void Init
        (
        RetVoidParamVoidDelegate _useAmmoCallback,
        RetVoidRaramIntDelegate _enemyDamagedCallback,
        RetVoidRaramIntDelegate _goldChangeCallback,
        RetVoidParamVoidDelegate _playerDamagedCallback
        )
    {
        playerMove = GetComponent<PlayerMovementController>();
        playerAnim = GetComponent<PlayerAnimatorController>();
        playercollider = GetComponent<PlayerCollider>();
        weaponAR = GetComponentInChildren<WeaponAssaultRifle>();
        statusGold = GetComponent<StatusGold>();
        gameManager = GameManager.Instance;

        gameManager.RegisterPauseObserver(GetComponent<IPauseObserver>());

        weaponAR.OnUseAmmoCallback = _useAmmoCallback;
        weaponAR.OnEnemyDamagedCallback = _enemyDamagedCallback;
        statusGold.OnGoldChangeCallback = _goldChangeCallback;
        playercollider.OnPlayerDamagedCallback = _playerDamagedCallback;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void MovePlayerToNextStage(Vector3 _desPos)
    {
        transform.position += (_desPos * warpDistance);
    }

    public void CheckPause(bool _isPaused)
    {
        isPaused = _isPaused;

        playerMove.CheckPaused(isPaused);
        playerAnim.CheckPaused(isPaused);
        weaponAR.CheckPaused(isPaused);
    }

    public void ToggleBossEngage()
    {
        isBossEngage = true;
    }


    private void Update()
    {
        UpdatePause();

        if (isPaused)
            return;

        UpdateInput();
        UpdateMove();
        UpdateDash();
        UpdateAttack();
        UpdateReload();
    }

    private void UpdatePause()
    {
        if (isBossEngage)
            return;
        if (Input.GetButtonDown("Pause"))
        {
            gameManager.TogglePause();
            Debug.Log(isPaused);
        }
    }

    private void UpdateInput()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        isRun = Input.GetButton("Run");
        isInteract = Input.GetButton("Interact");
        isSellCrystal = Input.GetButton("SellItem");

        if (!isInteract)
        {
            isSelected = false;
        }
    }

    private void UpdateMove()
    {
        if (!playerAnim.CurAnimationIs("Dash"))
        {
            bool isMove = playerAnim.ControllPlayerStateAnim(Input.GetButton("Horizontal"), Input.GetButton("Vertical"), isRun);

            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical") || Input.GetMouseButton(0))
                playerMove.RotateUpdate(x, z, Input.GetMouseButton(0));

            if (isMove)
                playerMove.MoveUpdate(x, z, isRun);
            else
                playerMove.MoveLerp(x, z, isRun);
        }
    }

    private void UpdateDash()
    {
        if (Input.GetButton("Dash") && !playerAnim.CurAnimationIs("Dash"))
        {
            if (playerMove.CanDash)
                playerMove.DashUpdate(x, z);
            else
                Debug.Log("DashCooltime!!");
        }
    }

    private void UpdateAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnim.SetBool("isAttack", true);
            weaponAR.ChangeState(EWeaponState.Attack);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            playerAnim.SetBool("isAttack", false);
            weaponAR.ChangeState(EWeaponState.Idle);
        }
    }

    private void UpdateReload()
    {
        if (Input.GetButtonDown("Reload"))
            weaponAR.ChangeState(EWeaponState.Reload);
    }

    [SerializeField]
    private float warpDistance = 15f;

    private float x = 0.0f;
    private float z = 0.0f;

    private bool isRun = false;
    private bool isInteract = false;
    private bool isSellCrystal = false;
    private bool isSelected = false;
    private bool isPaused = false;
    private bool isBossEngage = false;

    private WeaponAssaultRifle          weaponAR = null;
    private PlayerMovementController    playerMove = null;
    private PlayerAnimatorController    playerAnim = null;
    private PlayerCollider              playercollider = null;
    private GameManager                 gameManager = null;
    private StatusGold                  statusGold = null;
}
