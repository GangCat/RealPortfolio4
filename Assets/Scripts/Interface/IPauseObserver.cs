using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPauseObserver
{
    /// <summary>
    /// ��ü�κ��� ������Ʈ�� �޴� �޼ҵ�
    /// </summary>
    /// <param name="_isPause"></param>
    void CheckPause(bool _isPause);
}