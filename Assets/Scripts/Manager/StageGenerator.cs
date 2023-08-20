using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EDoorDir { None = -1, Front, Back, Left, Right }
[System.Serializable]
public enum EStageState { Start, Normal, Gold, Boss }
public class StageGenerator : MonoBehaviour
{
    public struct SStagePos
    {
        public int x;
        public int y;
        public int rank;

        public SStagePos(int _x, int _y, int _rank)
        {
            x = _x;
            y = _y;
            rank = _rank;
        }
    }

    public void GenerateLevel(int _minRoomCnt, OnPlayerMoveToNextStageDelegate _callback)
    {
        minStageCnt = _minRoomCnt;
        arrayLength = Mathf.Clamp((int)(minStageCnt * 0.7f), 10, 31);
        arrayStage = new int[arrayLength, arrayLength];
        System.Array.Clear(arrayStage, 0, arrayStage.Length);

        while (!SetStagePosition())
            ResetLevel();

        StartCoroutine("GenerateLevelCoroutine", _callback);
    }

    private IEnumerator GenerateLevelCoroutine(OnPlayerMoveToNextStageDelegate _callback)
    {
        GameObject mapGo = null;
        foreach(SStagePos room in listStage)
        {
            if (room.rank == 0)
                mapGo = Instantiate(StagePrefabs[(int)EStageState.Start], transform);
            else if (room.rank < goldRank)
                mapGo = Instantiate(StagePrefabs[(int)EStageState.Normal], transform);
            else if (room.rank < bossRank)
                mapGo = Instantiate(StagePrefabs[(int)EStageState.Gold], transform);
            else
                mapGo = Instantiate(StagePrefabs[(int)EStageState.Boss], transform);

            float xPos = (room.x - (arrayLength >> 1)) * offsetX;
            float zPos = (room.y - (arrayLength >> 1)) * offsetZ;
            mapGo.transform.position = new Vector3(xPos, 0f, zPos);
            yield return null;
            //yield return new WaitForSeconds(0.1f);

            SetBridge(room.x, room.y, mapGo);
            mapGo.GetComponent<Stage>().Init(_callback);
            listStageGo.Add(mapGo);
            yield return null;
            //yield return new WaitForSeconds(0.1f);
        }
    }

    public void ResetLevel()
    {
        foreach (GameObject go in listStageGo)
            Destroy(go);

        arrayLength = Mathf.Clamp((int)(minStageCnt * 0.7f), 10, 31);
        arrayStage = new int[arrayLength, arrayLength];
        System.Array.Clear(arrayStage, 0, arrayStage.Length);
        listStage.Clear();
        listStageGo.Clear();
    }

    private bool SetStagePosition()
    {
        int ttlRoomCnt = 0;
        int prevRoomCnt = 0;
        int tempRoomCnt = 0;
        int prevRank = 0;
        int randomRoom = 0;
        goldRank = 0;
        bossRank = 0;

        // 시작지점 초기화
        listStage.Clear();
        System.Array.Clear(arrayStage, 0, arrayStage.Length);

        SStagePos roomPos = new SStagePos(arrayLength >> 1, arrayLength >> 1, 0);
        arrayStage[arrayLength >> 1, arrayLength >> 1] = 1;
        listStage.Add(roomPos);

        ++ttlRoomCnt;

        while (ttlRoomCnt < minStageCnt)
        {
            if (prevRank > minStageCnt && ttlRoomCnt < minStageCnt)
                return false;

            tempRoomCnt = 0;
            for (int i = prevRoomCnt; i < ttlRoomCnt; ++i)
            {
                if (MyMathf.CheckRange(listStage[i].x, 1, arrayLength - 1) &&
                    MyMathf.CheckRange(listStage[i].y, 1, arrayLength - 1))
                    tempRoomCnt += SetStagePosInArray(listStage[i].x, listStage[i].y, prevRank + 1, ref randomRoom);
            }

            prevRoomCnt = ttlRoomCnt;
            ttlRoomCnt += tempRoomCnt;
            ++prevRank;
        }

        goldRank = prevRank + 1;
        bossRank = prevRank + 2;

        tempRoomCnt = 0;

        for (int i = 0; i < ttlRoomCnt; ++i)
        {
            if (MyMathf.CheckRange(listStage[i].x, 1, arrayLength - 1) &&
                MyMathf.CheckRange(listStage[i].y, 1, arrayLength - 1))
                tempRoomCnt += SetStagePosInArray(listStage[i].x, listStage[i].y, goldRank, ref randomRoom, 4, true);
            if (tempRoomCnt > 1)
                break;
        }
        if (tempRoomCnt != 2)
            return false;

        tempRoomCnt = 0;
        for (int i = 0; i < ttlRoomCnt; ++i)
        {
            if (MyMathf.CheckRange(listStage[i].x, 1, arrayLength - 1) &&
                MyMathf.CheckRange(listStage[i].y, 1, arrayLength - 1))
                tempRoomCnt += SetStagePosInArray(listStage[i].x, listStage[i].y, bossRank, ref randomRoom, 4, true);
            if (tempRoomCnt > 0)
                break;
        }
        if (tempRoomCnt != 1)
            return false;

        return true;

        // 랭크 1에 해당하는 방을 생성한다.
        // 2번째로 생기는 방은 반드시 2~4개의 방이 생성됨.


        // 최대 뻗어나갈 수 있는 방의 랭크 개수를 가지고 계산하자.
        // 만약 누군가가 4르 ㄹ입력한다면 배열은 4의 2배 + 3 크기로 만들자.( 좌 우 끝에 비우기 위함)
    }

    private int SetStagePosInArray(int _x, int _y, int _rank, ref int _randomRoom, int _roomCnt = 2, bool isSpecialRoom = false)
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
            roomPos = new SStagePos(_x, _y + 1, _rank);
            listRoomPos.Add(roomPos);
        }
        else
            --_roomCnt;

        if (arrayStage[_x, _y - 1].Equals(0))
        {
            roomPos = new SStagePos(_x, _y - 1, _rank);
            listRoomPos.Add(roomPos);
        }
        else
            --_roomCnt;

        if (arrayStage[_x - 1, _y].Equals(0))
        {
            roomPos = new SStagePos(_x - 1, _y, _rank);
            listRoomPos.Add(roomPos);
        }
        else
            --_roomCnt;

        if (arrayStage[_x + 1, _y].Equals(0))
        {
            roomPos = new SStagePos(_x + 1, _y, _rank);
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
                        arrayStage[listRoomPos[i].x, listRoomPos[i].y] = 1 + _rank;
                        listStage.Add(listRoomPos[i]);
                        return 1;
                    }
                }

                return 0;
            }

            for (int i = 0; i < listRoomPos.Count; ++i)
                arrayStage[listRoomPos[i].x, listRoomPos[i].y] = 1 + _rank;

            listStage.AddRange(listRoomPos.ToArray());
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

        tempGo = SelectDoorOrWall(_x, _y + 1, EDoorDir.Front, _parentGo);
        tempGo.transform.localPosition = Vector3.zero;

        tempGo = SelectDoorOrWall(_x, _y - 1, EDoorDir.Back, _parentGo);
        tempGo.transform.localPosition = Vector3.zero;

        tempGo = SelectDoorOrWall(_x - 1, _y, EDoorDir.Left, _parentGo);
        tempGo.transform.localPosition = Vector3.zero;

        tempGo = SelectDoorOrWall(_x + 1, _y, EDoorDir.Right, _parentGo);
        tempGo.transform.localPosition = Vector3.zero;
    }

    private GameObject SelectDoorOrWall(int _x, int _y, EDoorDir _dir, GameObject _parentGo)
    {
        if (!MyMathf.CheckRange(_x, 0, arrayLength) || !MyMathf.CheckRange(_y, 0, arrayLength))
            return Instantiate(wallPrefabs[(int)_dir], _parentGo.transform);

        if (arrayStage[_x, _y] > 0)
            return Instantiate(doorPrefabs[(int)_dir], _parentGo.transform);
        else
            return Instantiate(wallPrefabs[(int)_dir], _parentGo.transform);
    }

    private void Awake()
    {
        listStage = new List<SStagePos>();
        listStageGo = new List<GameObject>();
    }

    [Header("- Start / Normal / Gold / Boss")]
    [SerializeField]
    private GameObject[] StagePrefabs = null;

    [Header("-Front / Back / Left / Right")]
    [SerializeField]
    private GameObject[] wallPrefabs = null;
    [SerializeField]
    private GameObject[] doorPrefabs = null;

    [Header("- Room Offset")]
    [SerializeField]
    private float offsetX = 0f;
    [SerializeField]
    private float offsetZ = 0f;

    private int minStageCnt = 0;
    private int arrayLength = 0;
    private int bossRank = 0;
    private int goldRank = 0;

    private int[,] arrayStage = null;

    private List<SStagePos> listStage = null;
    private List<GameObject> listStageGo = null;
}
