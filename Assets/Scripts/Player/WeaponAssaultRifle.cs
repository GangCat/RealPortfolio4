using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class AmmoEvent : UnityEngine.Events.UnityEvent<float> { }
public class DmgEvent : UnityEngine.Events.UnityEvent<float> { }
public class AttacRateEvent : UnityEngine.Events.UnityEvent<float> { }
public class AttributeDmgEvent : UnityEngine.Events.UnityEvent<float[]> { }
public enum EWeaponState { None = -1, Idle, Attack, Reload }
public class WeaponAssaultRifle : MonoBehaviour
{
    public OnUseAmmoDelegate OnUseAmmoCallback
    {
        set => onUseAmmoCallback = value;
    }

    public OnEnemyDamagedDelegate OnEnemyDamagedCallback
    {
        set => onEnemyDamagedCallback = value;
    }

    [HideInInspector]
    public AmmoEvent onAmmoEvent = new AmmoEvent();
    public DmgEvent onDmgEvent = new DmgEvent();
    public AttacRateEvent onAttackRateEvent = new AttacRateEvent();
    public AttributeDmgEvent onAttributeDmgEvent = new AttributeDmgEvent();
    public bool IsReload => isReload;
    public bool IsAttack => isAttack;
    public float Dmg => weaponSetting.dmg;
    public float AttackRate => weaponSetting.attackRate;
    public float[] AttributeDmgs => weaponSetting.attributeDmgs;

    #region BuffDmg
    public void BuffDmg(float _ratio, float _duration)
    {
        if (isDmgBuff)
            StopCoroutine("ResetDmg");

        isDmgBuff = true;

        weaponSetting.dmg = weaponSetting.dmg * _ratio > weaponSetting.maxDmg ? weaponSetting.maxDmg : weaponSetting.dmg * _ratio;

        onDmgEvent.Invoke(weaponSetting.dmg);

        StartCoroutine("ResetDmg", _duration);
    }

    private IEnumerator ResetDmg(float _duration)
    {
        yield return new WaitForSeconds(_duration);

        weaponSetting.dmg = oriDmg;
        onDmgEvent.Invoke(weaponSetting.dmg);
        isDmgBuff = false;
    }
    #endregion

    #region BuffAttackRate
    public void BuffAttackRate(float _ratio, float _duration)
    {
        if (isAttackRateBuff)
            StopCoroutine("ResetAttackRate");

        isAttackRateBuff = true;

        weaponSetting.attackRate = weaponSetting.attackRate * _ratio < weaponSetting.minAttackRate ? weaponSetting.minAttackRate : weaponSetting.attackRate * _ratio;

        onAttackRateEvent.Invoke(weaponSetting.attackRate);

        StartCoroutine("ResetAttackRate", _duration);
    }

    private IEnumerator ResetAttackRate(float _duration)
    {
        yield return new WaitForSeconds(_duration);

        weaponSetting.attackRate = oriAttackRate;
        onAttackRateEvent.Invoke(weaponSetting.attackRate);
        isAttackRateBuff = false;
    }
    #endregion

    #region BuffAttributeDmgs
    public void BuffAttributeDmgs(float _ratio, float _duration)
    {
        if (isAttributeDmgBuff)
            StopCoroutine("ResetAttributeDmgs");

        isAttributeDmgBuff = true;


        for (int i = 0; i < weaponSetting.attributeDmgs.Length; ++i)
        {
            weaponSetting.attributeDmgs[i] =
                weaponSetting.attributeDmgs[i] * _ratio > weaponSetting.maxAttributeDmg ?
                weaponSetting.maxAttributeDmg :
                weaponSetting.attributeDmgs[i] * _ratio;
        }


        onAttributeDmgEvent.Invoke(weaponSetting.attributeDmgs);

        StartCoroutine("ResetAttributeDmgs", _duration);
    }

    private IEnumerator ResetAttributeDmgs(float _duration)
    {
        yield return new WaitForSeconds(_duration);

        for (int i = 0; i < weaponSetting.attributeDmgs.Length; ++i)
            weaponSetting.attributeDmgs[i] = oriAttributeDmgs[i];

        onAttributeDmgEvent.Invoke(weaponSetting.attributeDmgs);

        isAttributeDmgBuff = false;
    }
    #endregion

    public void ChangeDmg(float _increaseDmg)
    {
        weaponSetting.dmg = oriDmg + _increaseDmg;

        onDmgEvent.Invoke(weaponSetting.dmg);
    }

    public void ChangeAttackRate(float _ratioAttackRate)
    {
        weaponSetting.attackRate = oriAttackRate * (1 - _ratioAttackRate);

        onAttackRateEvent.Invoke(weaponSetting.attackRate);
    }

    public void ChangeAttributeDmgs(float _increaseAttributeDmg)
    {
        for (int i = 0; i < weaponSetting.attributeDmgs.Length; ++i)
        {
            weaponSetting.attributeDmgs[i] =
                oriAttributeDmgs[i] + _increaseAttributeDmg;
        }

        onAttributeDmgEvent.Invoke(weaponSetting.attributeDmgs);
    }

    public void ChangeState(EWeaponState _newState)
    {
        if (curState == _newState) return;

        if (isReload || playerAnim.CurAnimationIs("Dash")) return;

        if (isAttack)
        {
            isAttack = false;
        }

        StopCoroutine(curState.ToString());
        curState = _newState;
        StartCoroutine(curState.ToString());
    }

    private IEnumerator Idle()
    {
        yield return null;
    }

    private IEnumerator Attack()
    {
        playerAnim.SetBool("isRun", false);

        bool prevIsAttack = isAttack;

        while (true)
        {
            if (weaponSetting.curAmmo <= 0)
            {
                ChangeState(EWeaponState.Reload);
                yield break;
            }

            --weaponSetting.curAmmo;

            onUseAmmoCallback?.Invoke();

            onAmmoEvent.Invoke(weaponSetting.curAmmo);

            playerAnim.PlayAttack();

            isAttack = true;
            if (prevIsAttack != isAttack)
                yield return null;

            StartCoroutine("OnMuzzleEffect");

            OnAttack();

            prevIsAttack = isAttack;

            yield return new WaitForSeconds(weaponSetting.attackRate);
        }
    }

    private IEnumerator OnMuzzleEffect()
    {
        EffectMuzzle.SetActive(true);

        yield return new WaitForSeconds(weaponSetting.attackRate * 0.8f);

        EffectMuzzle.SetActive(false);
    }

    private void OnAttack()
    {
        projectileMemoryPool.SpawnProjectile(trMuzzleOfWeapon.position, trMuzzleOfWeapon.rotation, weaponSetting.dmg, onEnemyDamagedCallback);
    }

    private IEnumerator Reload()
    {
        playerAnim.SetBool("isRun", false);

        isReload = true;
        playerAnim.PlayReload();

        yield return new WaitForSeconds(0.2f);

        while (true)
        {
            if (!playerAnim.CurAnimationIs("Reload", 1))
                break;

            yield return null;
        }

        weaponSetting.curAmmo = weaponSetting.maxAmmo;
        onAmmoEvent.Invoke(weaponSetting.curAmmo);
        playerAnim.SetBool("isAmmoEmpty", false);
        Debug.Log("Ammo Refill");

        isReload = false;
        if (playerAnim.GetBool("isAttack"))
            ChangeState(EWeaponState.Attack);
        else
            ChangeState(EWeaponState.Idle);
    }

    private void Awake()
    {
        playerAnim = GetComponentInParent<PlayerAnimatorController>();
        projectileMemoryPool = GetComponent<ProjectileMemoryPool>();
    }

    private void Start()
    {
        weaponSetting.curAmmo = weaponSetting.maxAmmo;
        onAmmoEvent.Invoke(weaponSetting.curAmmo);
        EffectMuzzle.SetActive(false);

        oriDmg = weaponSetting.dmg;
        oriAttackRate = weaponSetting.attackRate;
        oriAttributeDmgs = new float[weaponSetting.attributeDmgs.Length];

        for (int i = 0; i < weaponSetting.attributeDmgs.Length; ++i)
            oriAttributeDmgs[i] = weaponSetting.attributeDmgs[i];
    }

    private void OnEnable()
    {
        curState = EWeaponState.Idle;
    }

    private void OnDisable()
    {
        curState = EWeaponState.None;
    }

    public void CheckPaused(bool _isPaused)
    {
        isPaused = _isPaused;

        if (isPaused)
            ChangeState(EWeaponState.Idle);
    }



    [Header("-MuzzleEffect")]
    [SerializeField]
    private Transform trMuzzleOfWeapon;
    [SerializeField]
    private GameObject EffectMuzzle;

    [Header("-ETC")]
    [SerializeField]
    private LayerMask interactiveLayerMask;
    [SerializeField]
    private WeaponSetting weaponSetting;

    private bool isReload = false;
    private bool isAttack = false;
    private bool isPaused = false;
    private bool isDmgBuff = false;
    private bool isAttackRateBuff = false;
    private bool isAttributeDmgBuff = false;

    private float oriDmg = 0;
    private float oriAttackRate = 0;
    private float[] oriAttributeDmgs;

    private PlayerAnimatorController playerAnim = null;
    private ProjectileMemoryPool projectileMemoryPool = null;

    private EWeaponState curState = EWeaponState.None;
    private OnUseAmmoDelegate onUseAmmoCallback = null;
    private OnEnemyDamagedDelegate onEnemyDamagedCallback = null;
}
