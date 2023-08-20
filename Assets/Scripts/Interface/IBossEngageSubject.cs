using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossEngageSubject
{ 
    /// <summary>
     /// 일시정지 관련 옵저버 등록 메소드
     /// </summary>
     /// <param name="_observer"></param>
    void RegisterBossEngageObserver(IBossEngageObserver _observer);

    /// <summary>
    /// 일시정지 관련 옵저버 제거 메소드
    /// </summary>
    /// <param name="_observer"></param>
    void RemoveBossEngageObserver(IBossEngageObserver _observer);

    void ToggleBossEngage();
}
