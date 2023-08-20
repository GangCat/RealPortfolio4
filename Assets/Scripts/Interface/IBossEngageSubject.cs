using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossEngageSubject
{ 
    /// <summary>
     /// �Ͻ����� ���� ������ ��� �޼ҵ�
     /// </summary>
     /// <param name="_observer"></param>
    void RegisterBossEngageObserver(IBossEngageObserver _observer);

    /// <summary>
    /// �Ͻ����� ���� ������ ���� �޼ҵ�
    /// </summary>
    /// <param name="_observer"></param>
    void RemoveBossEngageObserver(IBossEngageObserver _observer);

    void ToggleBossEngage();
}
