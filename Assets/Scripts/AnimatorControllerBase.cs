using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllerBase : MonoBehaviour
{
    /// <summary>
    /// �ִϸ��̼� �̸�, ���̾� ����, �ִϸ��̼��� ���۵Ǵ� ������ ������ �Ű������� �޴´�.
    /// ���̾ -1�̸� ù���� state�� �ִϸ��̼��� ��µȴ�.
    /// </summary>
    /// <param name="_stateName"></param>
    /// <param name="_layer"></param>
    /// <param name="_normalizedTime"></param>
    public void Play(string _stateName, int _layer, float _normalizedTime)
    {
        anim.Play(_stateName, _layer, _normalizedTime);
    }

    /// <summary>
    /// �ش� �̸��� �ִϸ��̼��� ����������� ��ȯ�Ѵ�.
    /// ������̶�� true�� ��ȯ�ȴ�.
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    public bool CurAnimationIs(string _name, int _layerIdx = 0)
    {
        return anim.GetCurrentAnimatorStateInfo(_layerIdx).IsName(_name);
    }

    /// <summary>
    /// �ش� �̸��� �Ķ������ float���� �����Ѵ�.
    /// </summary>
    /// <param name="_paramName"></param>
    /// <param name="_value"></param>
    public void SetFloat(string _paramName, float _value)
    {
        //if() �ش� �Ķ���Ͱ� float Ÿ������ Ȯ���ϴ� ���� üũ �ʿ�

        anim.SetFloat(_paramName, _value);
    }

    /// <summary>
    /// �ش� �̸��� �Ķ������ bool���� �����Ѵ�.
    /// </summary>
    /// <param name="_paramName"></param>
    /// <param name="_boolVal"></param>
    public void SetBool(string _paramName, bool _boolVal)
    {
        //if() �ش� �Ķ���Ͱ� bool Ÿ������ Ȯ���ϴ� ���� üũ �ʿ�

        anim.SetBool(_paramName, _boolVal);
    }

    /// <summary>
    /// �ش� �̸��� �Ķ������ bool���� ��ȯ�Ѵ�.
    /// </summary>
    /// <param name="_paramName"></param>
    /// <returns></returns>
    public bool GetBool(string _paramName)
    {
        //if() �ش� �Ķ���Ͱ� bool Ÿ������ Ȯ���ϴ� ���� üũ �ʿ�

        return anim.GetBool(_paramName);
    }

    /// <summary>
    /// �ش� �̸��� �Ķ������ Trigger�� �۵��Ѵ�.
    /// </summary>
    /// <param name="_paramName"></param>
    public void SetTrigger(string _paramName)
    {
        anim.SetTrigger(_paramName);
    }


    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected Animator anim = null;
}
