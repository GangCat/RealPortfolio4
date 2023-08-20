using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStageSubject
{
    /// <summary>
    /// �������� ���� ������ ��� �޼ҵ�
    /// </summary>
    /// <param name="_observer"></param>
    void RegisterStageobserver(IStageObserver _observer);

    /// <summary>
    /// �������� ���� ������ ���� �޼ҵ�
    /// </summary>
    /// <param name="_observer"></param>
    void RemoveStageObserver(IStageObserver _observer);

    /// <summary>
    /// ���ŵ� �������� ���� ����
    /// </summary>
    void StageStart();
}
