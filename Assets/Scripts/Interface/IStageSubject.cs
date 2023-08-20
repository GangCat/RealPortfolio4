using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStageSubject
{
    /// <summary>
    /// 스테이지 관련 옵저버 등록 메소드
    /// </summary>
    /// <param name="_observer"></param>
    void RegisterStageobserver(IStageObserver _observer);

    /// <summary>
    /// 스테이지 관련 옵저버 제거 메소드
    /// </summary>
    /// <param name="_observer"></param>
    void RemoveStageObserver(IStageObserver _observer);

    /// <summary>
    /// 갱신된 스테이지 정보 제공
    /// </summary>
    void StageStart();
}
