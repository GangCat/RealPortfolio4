using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponMinigun : WeaponBase
{
    public override void OnAttack()
    {
        Vector3 targetPos = targetTr.position;
        targetPos.y = 1.0f;
        Quaternion rot = Quaternion.LookRotation(targetPos - trMuzzleOfWeapon.position);
        projectileMemoryPool.SpawnProjectile(trMuzzleOfWeapon.position, rot, weaponSetting.dmg);
        enemyController.GetComponent<Animator>().Play("Shoot", -1, 0);

        --weaponSetting.curAmmo;
    }

    public override void Reload()
    {
        weaponSetting.curAmmo = weaponSetting.maxAmmo;
    }

    public override void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
            anim.StartPlayback();
        else
            anim.StopPlayback();
    }


    private void Start()
    {
        limitDistance = weaponSetting.attackDistance * 1.5f;
    }

    private void Awake()
    {
        projectileMemoryPool = GetComponent<ProjectileMemoryPool>();
        anim = GetComponent<Animator>();
    }

    [SerializeField]
    private Transform trMuzzleOfWeapon = null;
    [SerializeField]
    private EnemyController enemyController = null;

    private bool isPaused = false;

    private ProjectileMemoryPool projectileMemoryPool = null;
    private Animator anim = null;
}
