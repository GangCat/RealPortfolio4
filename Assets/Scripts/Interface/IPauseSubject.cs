using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPauseSubject
{
    /// <summary>
    /// �Ͻ����� ���� ������ ��� �޼ҵ�
    /// </summary>
    /// <param name="_observer"></param>
    void RegisterPauseObserver(IPauseObserver _observer);

    /// <summary>
    /// �Ͻ����� ���� ������ ���� �޼ҵ�
    /// </summary>
    /// <param name="_observer"></param>
    void RemovePauseObserver(IPauseObserver _observer);

    /// <summary>
    /// �Ͻ����� ���� ���� �޼ҵ�
    /// </summary>
    void TogglePause();
}
