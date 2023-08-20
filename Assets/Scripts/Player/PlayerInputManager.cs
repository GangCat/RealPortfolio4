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

    private void Awake()
    {
        playerMove = GetComponent<PlayerMovementController>();
        playerAnim = GetComponent<PlayerAnimatorController>();
        playercollider = GetComponent<PlayerCollider>();
        weaponAR = GetComponentInChildren<WeaponAssaultRifle>();
        statusGold = GetComponent<StatusGold>();
        gameManager = GameManager.Instance;
    }

    public void SetOnUseAmmoCallback(OnUseAmmoDelegate _callback)
    {
        weaponAR.OnUseAmmoCallback = _callback;
    }

    public void SetOnEnemyDamagedCallback(OnEnemyDamagedDelegate _callback)
    {
        weaponAR.OnEnemyDamagedCallback = _callback;
    }

    public void SetOnGoldChangeCallback(OnGoldChangeDelegate _callback)
    {
        statusGold.OnGoldChangeCallback = _callback;
    }

    public void SetOnPlayerDamagedCallback(OnPlayerDamagedDelegate _callback)
    {
        playercollider.OnPlayerDamagedCallback = _callback;
    }

    private void Start()
    {
        gameManager.RegisterPauseObserver(GetComponent<IPauseObserver>());
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

    public void CheckPaused(bool _isPaused)
    {
        isPaused = _isPaused;

        playerMove.CheckPaused(isPaused);
        playerAnim.CheckPaused(isPaused);
        weaponAR.CheckPaused(isPaused);
    }

    public void CheckBossEngage(bool _isPaused)
    {
        isBossEngage = _isPaused;
    }

    private float x = 0.0f;
    private float z = 0.0f;

    private bool isRun = false;
    private bool isInteract = false;
    private bool isSellCrystal = false;
    private bool isSelected = false;
    private bool isPaused = false;
    private bool isBossEngage = false;

    private WeaponAssaultRifle weaponAR = null;
    private PlayerMovementController playerMove = null;
    private PlayerAnimatorController playerAnim = null;
    private PlayerCollider playercollider = null;
    private GameManager gameManager = null;
    private StatusGold statusGold = null;
}
