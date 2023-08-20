using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : AnimatorControllerBase
{
    /// <summary>
    /// �ִϸ��̼��� isWalk�� isRun�� �Ķ���� ���� ������.
    /// �� �� isWalk�� ���� ��ȯ����.
    /// </summary>
    /// <param name="_inputH"></param>
    /// <param name="_inputV"></param>
    /// <param name="_inputR"></param>
    /// <returns></returns>
    public bool ControllPlayerStateAnim(bool _inputH, bool _inputV, bool _inputR)
    {
        if (!_inputR)
            SetBool("isRun", false);

        if (_inputH || _inputV)
        {
            SetBool("isWalk", true);

            if (_inputR && !weaponAR.IsAttack && !weaponAR.IsReload)
                SetBool("isRun", true);
            else
                SetBool("isRun", false);

            return true;
        }
        else
        {
            SetBool("isRun", false);
            SetBool("isWalk", false);
            return false;
        }
    }

    public void PlayDash()
    {
        Play("Dash", 0, 0);
    }

    public void PlayAttack()
    {
        Play("Attack", 1, 0.4f);
    }

    public void PlayReload()
    {
        Play("Reload", 1, 0);
    }

    public void SetDirWhileAttack(Vector3 _moveDir)
    {
        SetFloat("dirX", _moveDir.x);
        SetFloat("dirZ", _moveDir.z);
    }

    protected override void Awake()
    {
        base.Awake();
        weaponAR = GetComponentInChildren<WeaponAssaultRifle>();
    }

    public void CheckPaused(bool _isPaused)
    {
        isPaused = _isPaused;

        if (isPaused)
            anim.StartPlayback();
        else
            anim.StopPlayback(); 

    }

    private WeaponAssaultRifle weaponAR = null;

    private bool isPaused = false;
}
