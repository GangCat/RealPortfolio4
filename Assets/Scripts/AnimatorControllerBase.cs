using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllerBase : MonoBehaviour
{
    /// <summary>
    /// 애니메이션 이름, 레이어 순서, 애니메이션이 시작되는 지점의 비율을 매개변수로 받는다.
    /// 레이어가 -1이면 첫번쨰 state의 애니메이션이 출력된다.
    /// </summary>
    /// <param name="_stateName"></param>
    /// <param name="_layer"></param>
    /// <param name="_normalizedTime"></param>
    public void Play(string _stateName, int _layer, float _normalizedTime)
    {
        anim.Play(_stateName, _layer, _normalizedTime);
    }

    /// <summary>
    /// 해당 이름의 애니메이션이 출력중인지를 반환한다.
    /// 출력중이라면 true가 반환된다.
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    public bool CurAnimationIs(string _name, int _layerIdx = 0)
    {
        return anim.GetCurrentAnimatorStateInfo(_layerIdx).IsName(_name);
    }

    /// <summary>
    /// 해당 이름의 파라미터의 float값을 변경한다.
    /// </summary>
    /// <param name="_paramName"></param>
    /// <param name="_value"></param>
    public void SetFloat(string _paramName, float _value)
    {
        //if() 해당 파라미터가 float 타입인지 확인하는 오류 체크 필요

        anim.SetFloat(_paramName, _value);
    }

    /// <summary>
    /// 해당 이름의 파라미터의 bool값을 변경한다.
    /// </summary>
    /// <param name="_paramName"></param>
    /// <param name="_boolVal"></param>
    public void SetBool(string _paramName, bool _boolVal)
    {
        //if() 해당 파라미터가 bool 타입인지 확인하는 오류 체크 필요

        anim.SetBool(_paramName, _boolVal);
    }

    /// <summary>
    /// 해당 이름의 파라미터의 bool값을 반환한다.
    /// </summary>
    /// <param name="_paramName"></param>
    /// <returns></returns>
    public bool GetBool(string _paramName)
    {
        //if() 해당 파라미터가 bool 타입인지 확인하는 오류 체크 필요

        return anim.GetBool(_paramName);
    }

    /// <summary>
    /// 해당 이름의 파라미터의 Trigger를 작동한다.
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
