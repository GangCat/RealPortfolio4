using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStageObserver
{
    /// <summary>
    /// ��ü�κ��� ������Ʈ�� �޴� �޼ҵ�
    /// </summary>
    /// <param name="_curStage"></param>
    void CheckStage(int _curStage);
}
