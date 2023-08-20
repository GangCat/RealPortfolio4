using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandSaw : WeaponBase
{
    public override void OnAttack()
    {
        enemyController.GetComponent<Animator>().Play("Punch", -1, 0);
        enemyCollider.Attack(1f);
    }

    public override void Reload()
    {
    }

    private void Start()
    {
        enemyCollider.Setup(weaponSetting.dmg);
        limitDistance = weaponSetting.attackDistance * 1.5f;
    }

    public override void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            foreach(Animator anim in gadgetsAnim)
                anim.StartPlayback();
        }
        else
        {
            foreach (Animator anim in gadgetsAnim)
                anim.StopPlayback();
        }
    }

    [SerializeField]
    private EnemyCollider enemyCollider = null;
    [SerializeField]
    private EnemyController enemyController = null;
    [SerializeField]
    private Animator[] gadgetsAnim = null;

    private bool isPaused = false;

}
