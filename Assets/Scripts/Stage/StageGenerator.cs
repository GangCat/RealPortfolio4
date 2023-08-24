using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    public delegate void RetvoidParamArrayStageDelegate(Stage[] _arrayStage);

    public void GenerateLevel(int _minRoomCnt,
        RetVoidParamVec3Delegate _warpPlayerCallback,
        RetvoidParamArrayStageDelegate _returnStageArrayCallback
        )
    {
        minStageCnt = _minRoomCnt;
        arrayLength = Mathf.Clamp((int)(minStageCnt * 0.7f), 10, 31);
        arrayStage = new int[arrayLength, arrayLength];
        System.Array.Clear(arrayStage, (int)EStageState.Empty, arrayStage.Length);

        while (!SetStagePosition())
            ResetLevel();

        StartCoroutine(GenerateLevelCoroutine(_warpPlayerCallback, _returnStageArrayCallback));
    }

    private IEnumerator GenerateLevelCoroutine(
        RetVoidParamVec3Delegate _warpPlayerCallback,
        RetvoidParamArrayStageDelegate _returnStageArrayCallback
        )
    {
        GameObject mapGo = null;
        foreach(SStagePos room in listStagePos)
        {
            if (room.stageState == EStageState.Empty) continue;

            if (room.stageState == EStageState.Start)
                mapGo = Instantiate(StagePrefabs[(int)EStageState.Start], transform);
            else if (room.stageState == EStageState.Normal)
                mapGo = Instantiate(StagePrefabs[(int)EStageState.Normal], transform);
            else if (room.stageState == EStageState.Gold)
                mapGo = Instantiate(StagePrefabs[(int)EStageState.Gold], transform);
            else
                mapGo = Instantiate(StagePrefabs[(int)EStageState.Boss], transform);

            float xPos = (room.x - (arrayLength >> 1)) * offsetX;
            float zPos = (room.y - (arrayLength >> 1)) * offsetZ;
            mapGo.transform.position = new Vector3(xPos, 0f, zPos);
            yield return null;

            SetBridge(room.x, room.y, mapGo);
            mapGo.GetComponent<Stage>().Init(room.x, room.y, room.stageState, _warpPlayerCallback);
            listStageGo.Add(mapGo);
            listStage.Add(mapGo.GetComponent<Stage>());
            mapGo.SetActive(false);
            yield return null;
        }

        _returnStageArrayCallback?.Invoke(listStage.ToArray());
    }

    public void ResetLevel()
    {
        foreach (GameObject go in listStageGo)
            Destroy(go);

        arrayLength = Mathf.Clamp((int)(minStageCnt * 0.7f), 10, 31);
        arrayStage = new int[arrayLength, arrayLength];
        System.Array.Clear(arrayStage, (int)EStageState.Empty, arrayStage.Length);
        listStagePos.Clear();
        listStageGo.Clear();
    }

    private bool SetStagePosition()
    {
        int ttlRoomCnt = 0;
        int prevRoomCnt = 0;
        int tempRoomCnt = 0;
        int prevRank = 0;
        int randomRoom = 0;

        // 시작지점 초기화
        listStagePos.Clear();
        System.Array.Clear(arrayStage, (int)EStageState.Empty, arrayStage.Length);

        SStagePos roomPos = new SStagePos(arrayLength >> 1, arrayLength >> 1, EStageState.Start);
        arrayStage[arrayLength >> 1, arrayLength >> 1] = 1;
        listStagePos.Add(roomPos);

        ++ttlRoomCnt;

        while (ttlRoomCnt < minStageCnt)
        {
            if (prevRank > minStageCnt && ttlRoomCnt < minStageCnt)
                return false;

            tempRoomCnt = 0;
            for (int i = prevRoomCnt; i < ttlRoomCnt; ++i)
            {
                if (MyMathf.CheckRange(listStagePos[i].x, 1, arrayLength - 1) &&
                    MyMathf.CheckRange(listStagePos[i].y, 1, arrayLength - 1))
                    tempRoomCnt += SetStagePosInArray(listStagePos[i].x, listStagePos[i].y, EStageState.Normal, ref randomRoom);
            }

            prevRoomCnt = ttlRoomCnt;
            ttlRoomCnt += tempRoomCnt;
            ++prevRank;
        }

        #region GenGoldStage
        tempRoomCnt = 0;

        for (int i = 0; i < ttlRoomCnt; ++i)
        {
            if (MyMathf.CheckRange(listStagePos[i].x, 1, arrayLength - 1) &&
                MyMathf.CheckRange(listStagePos[i].y, 1, arrayLength - 1))
                tempRoomCnt += SetStagePosInArray(
                    listStagePos[i].x, 
                    listStagePos[i].y, 
                    EStageState.Gold, 
                    ref randomRoom, 
                    4, 
                    true);
            if (tempRoomCnt > 1)
                break;
        }
        if (tempRoomCnt != 2)
            return false;

        #endregion

        #region GenBossStage
        tempRoomCnt = 0;
        for (int i = 1; i < ttlRoomCnt; ++i)
        {
            if (MyMathf.CheckRange(listStagePos[i].x, 1, arrayLength - 1) &&
                MyMathf.CheckRange(listStagePos[i].y, 1, arrayLength - 1))
                tempRoomCnt += SetStagePosInArray(
                    listStagePos[i].x, 
                    listStagePos[i].y, 
                    EStageState.Boss, 
                    ref randomRoom, 
                    4, 
                    true);
            if (tempRoomCnt > 0)
                break;
        }
        if (tempRoomCnt != 1)
            return false;

        #endregion

        return true;
    }

    private int SetStagePosInArray(
        int _x, 
        int _y, 
        EStageState _stageState, 
        ref int _randomRoom, 
        int _roomCnt = 2, 
        bool isSpecialRoom = false)
    {
        List<SStagePos> listRoomPos = new List<SStagePos>();
        SStagePos roomPos;

        if (_randomRoom >= 4 && _roomCnt == 2)
        {
            _roomCnt = 3;
            _randomRoom = -1;
        }

        if (arrayStage[_x, _y + 1].Equals(0))
        {
            roomPos = new SStagePos(_x, _y + 1, _stageState);
            listRoomPos.Add(roomPos);
        }
        else
            --_roomCnt;

        if (arrayStage[_x, _y - 1].Equals(0))
        {
            roomPos = new SStagePos(_x, _y - 1, _stageState);
            listRoomPos.Add(roomPos);
        }
        else
            --_roomCnt;

        if (arrayStage[_x - 1, _y].Equals(0))
        {
            roomPos = new SStagePos(_x - 1, _y, _stageState);
            listRoomPos.Add(roomPos);
        }
        else
            --_roomCnt;

        if (arrayStage[_x + 1, _y].Equals(0))
        {
            roomPos = new SStagePos(_x + 1, _y, _stageState);
            listRoomPos.Add(roomPos);
        }
        else
            --_roomCnt;


        while (listRoomPos.Count > _roomCnt && listRoomPos.Count > 0)
            listRoomPos.RemoveAt(Random.Range(0, listRoomPos.Count));

        if (listRoomPos.Count > 0)
        {
            if(isSpecialRoom)
            {
                // 다시 이 반복문을 돌려서 새로 만들어질 그 방이 하나의 방과 연결되어있는지 확인.
                for (int i = 0; i < listRoomPos.Count; ++i)
                {
                    if (!IsStageIsolated(listRoomPos[0].x, listRoomPos[0].y))
                        continue;
                    else
                    {
                        arrayStage[listRoomPos[i].x, listRoomPos[i].y] = (int)_stageState;
                        listStagePos.Add(listRoomPos[i]);
                        return 1;
                    }
                }

                return 0;
            }

            for (int i = 0; i < listRoomPos.Count; ++i)
                arrayStage[listRoomPos[i].x, listRoomPos[i].y] = (int)_stageState;

            listStagePos.AddRange(listRoomPos.ToArray());
        }
        ++_randomRoom;

        return listRoomPos.Count;
    }

    private bool IsStageIsolated(int _x, int _y)
    {
        int tempRoomCnt = 0;
        if (_y < arrayLength - 1)
        {
            if (arrayStage[_x, _y + 1] > 0)
                ++tempRoomCnt;
        }
        if (_y > 0)
        {
            if(arrayStage[_x, _y - 1] > 0)
                ++tempRoomCnt;
        }
        if (_x < arrayLength - 1)
        {
            if (arrayStage[_x + 1, _y] > 0)
                ++tempRoomCnt;
        }
        if (_x > 0)
        {
            if (arrayStage[_x - 1, _y] > 0)
                ++tempRoomCnt;
        }

        if (tempRoomCnt > 1)
            return false;

        return true;
    }

    private void SetBridge(int _x, int _y, GameObject _parentGo)
    {
        GameObject tempGo = null;

        tempGo = SelectDoorOrWall(_x, _y + 1, EGateDir.Forward, _parentGo);
        tempGo.transform.localPosition = Vector3.zero;

        tempGo = SelectDoorOrWall(_x, _y - 1, EGateDir.Back, _parentGo);
        tempGo.transform.localPosition = Vector3.zero;

        tempGo = SelectDoorOrWall(_x - 1, _y, EGateDir.Left, _parentGo);
        tempGo.transform.localPosition = Vector3.zero;

        tempGo = SelectDoorOrWall(_x + 1, _y, EGateDir.Right, _parentGo);
        tempGo.transform.localPosition = Vector3.zero;
    }

    private GameObject SelectDoorOrWall(int _x, int _y, EGateDir _dir, GameObject _parentGo)
    {
        if (!MyMathf.CheckRange(_x, 0, arrayLength) || !MyMathf.CheckRange(_y, 0, arrayLength))
            return Instantiate(wallPrefabs[(int)_dir], _parentGo.transform);

        if (arrayStage[_x, _y] > 0)
            return Instantiate(gatePrefabs[(int)_dir], _parentGo.transform);
        else
            return Instantiate(wallPrefabs[(int)_dir], _parentGo.transform);
    }

    private void Awake()
    {
        listStagePos = new List<SStagePos>();
        listStageGo = new List<GameObject>();
        listStage = new List<Stage>();
    }

    [Header("- Empty / Start / Normal / Gold / Boss")]
    [SerializeField]
    private GameObject[] StagePrefabs = null;

    [Header("-Forward / Back / Left / Right")]
    [SerializeField]
    private GameObject[] wallPrefabs = null;
    [SerializeField]
    private GameObject[] gatePrefabs = null;

    [Header("- Room Offset")]
    [SerializeField]
    private float offsetX = 0f;
    [SerializeField]
    private float offsetZ = 0f;

    private int minStageCnt = 0;
    private int arrayLength = 0;

    private int[,] arrayStage = null;

    private List<SStagePos> listStagePos = null;
    private List<GameObject> listStageGo = null;
    private List<Stage> listStage = null;
}