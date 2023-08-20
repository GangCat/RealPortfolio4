using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPool
{
    public int TotalCnt => ttlCnt;
    public int ActiveCnt => activeCnt;

    /// <summary>
    /// �Է¹��� ������Ʈ�� ������� �޸�Ǯ ����
    /// </summary>
    /// <param name="_poolObject"></param>
    public MemoryPool(GameObject _poolObject, int _increaseCnt = 5, Transform _parentTr = null)
    {
        ttlCnt = 0;
        activeCnt = 0;

        poolObject = _poolObject;

        poolEnableList = new List<GameObject>();
        poolDisableQueue = new Queue<GameObject>();

        InstantiateObjects(_increaseCnt, _parentTr);
    }

    /// <summary>
    /// increaseCnt ������ ������Ʈ�� ����
    /// </summary>
    public void InstantiateObjects(int _increaseCnt = 5, Transform _parentTr = null)
    {
        for (int i = 0; i < _increaseCnt; ++i)
        {
            GameObject poolGo = GameObject.Instantiate(poolObject);
            poolGo.SetActive(false);
            if (_parentTr != null)
                poolGo.transform.parent = _parentTr;

            poolDisableQueue.Enqueue(poolGo);
        }

        ttlCnt += _increaseCnt;
    }

    /// <summary>
    /// ���� �������� ��� ������Ʈ�� '����'
    /// ���� �ٲ�ų� ������ ����� �� �� ���� ȣ��
    /// </summary>
    public void DestroyObjects()
    {
        if (poolEnableList == null || poolDisableQueue == null) return;

        int cnt = poolEnableList.Count;
        for (int i = 0; i < cnt; ++i)
            GameObject.Destroy(poolEnableList[i]);

        cnt = poolDisableQueue.Count;
        for(int i = 0; i < cnt; ++i)
            GameObject.Destroy(poolDisableQueue.Dequeue());

        poolEnableList.Clear();
        poolDisableQueue.Clear();
    }

    /// <summary>
    /// �ش� ������Ʈ�� ����
    /// </summary>
    /// <returns></returns>
    public GameObject ActivatePoolItem(int _increaseCnt = 5, Transform _parentTr = null)
    {
        if (poolEnableList == null || poolDisableQueue == null) return null;

        // ��� ������Ʈ�� Ȱ��ȭ�Ǿ��ִ� ���¶�� increaseCnt��ŭ �߰� ����
        if (poolDisableQueue.Count <= 0)
            InstantiateObjects(_increaseCnt, _parentTr);

        GameObject poolGo = poolDisableQueue.Dequeue();
        poolEnableList.Add(poolGo);

        poolGo.SetActive(true);

        ++activeCnt;

        return poolGo;
    }

    /// <summary>
    /// �ش� ������Ʈ�� ��Ȱ��ȭ
    /// </summary>
    /// <param name="_removeObject"></param>
    public void DeactivatePoolItem(GameObject _removeObject)
    {
        if (poolEnableList == null || poolDisableQueue == null || _removeObject == null) return;

        int cnt = poolEnableList.Count;
        for (int i = 0; i < cnt; ++i)
        {
            GameObject poolGo = poolEnableList[i];
            if (poolGo == _removeObject)
            {
                poolGo.SetActive(false);
                poolEnableList.Remove(poolGo);
                poolDisableQueue.Enqueue(poolGo);
                poolGo.transform.position = tempPos;

                --activeCnt;

                return;
            }
        }
    }

    /// <summary>
    /// ��� ������Ʈ�� ��Ȱ��ȭ
    /// </summary>
    public void DeactivateAllPoolItems()
    {
        if (poolEnableList == null || poolDisableQueue == null) return;

        int cnt = poolEnableList.Count;
        for (int i = 0; i < cnt; ++i)
        {
            GameObject poolGo = poolEnableList[i];

            poolGo.SetActive(false);
            poolGo.transform.position = tempPos;

            poolEnableList.Remove(poolGo);
            poolDisableQueue.Enqueue(poolGo);
        }

        activeCnt = 0;
    }

    public bool IsEnableListEmpty()
    {
        return poolEnableList.Count < 1;
    }

    private int ttlCnt = 0;
    private int activeCnt = 0;

    private Vector3 tempPos = new Vector3(0.0f, -30.0f, 0.0f);
    private GameObject poolObject; // ������Ʈ Ǯ������ �����ϴ� ������Ʈ ������

    private List<GameObject> poolEnableList = null;
    private Queue<GameObject> poolDisableQueue = null;

}
