using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPauseObserver
{
    /// <summary>
    /// 주체로부터 업데이트를 받는 메소드
    /// </summary>
    /// <param name="_isPaused"></param>
    void CheckPaused(bool _isPaused);
}