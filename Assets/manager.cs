using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : SingletonMonoBehavior<manager>
{
    public GameObject[] beadObjectArray;
    public List<int> allRowData;
    public List<GameObject> allBeadArray;

    public MapBlock[] mapBlocks;

    public int SpecialTotalSpawn;
    public int EmbraceSpawnedCount;
    public Transform NextCharTrans;


    public int Finish = 0;

    public int combo = 0;

    int TotalBean;

    public float Timer = 0;

    public bool inGameover = false;
    public bool start = false;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < mapBlocks.Length; i++)
        {
            allRowData.AddRange(mapBlocks[i].ConToInt());
        }
        TotalBean = allRowData.Count - 10;
        serializeOneRow(0);
        serializeOneRow(1);
        serializeOneRow(2);
        serializeOneRow(3);
        serializeOneRow(4);
        serializeOneRow(5);
        serializeOneRow(6);
        serializeOneRow(7);
        serializeOneRow(8);
        serializeOneRow(9);

        globalUpdateManager.instance.registerUpdateDg(playerContorl);
        globalUpdateManager.instance.registerUpdateDg(gameStartTimer);
        start = true;
    }

    void gameStartTimer()
    {
        Timer += globalVarManager.deltaTime;
    }

    void allRowFixPos()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("bead"))
        {
            item.transform.position = new Vector3(item.transform.position.x - 1, item.transform.position.y , 0);
        }
    }

    bool doOnce = false;

   

   public void hitRightBead()
    {
        combo++;
        Finish++;
        FPSS.instance.RecordFinish();

        allRowData.RemoveAt(0);
        Destroy(allBeadArray[0]);
        allBeadArray.RemoveAt(0);
        allRowFixPos();
        serializeOneRow(9);

        if (Finish >= TotalBean)
        {
            print("End");
            OnWin();
        }
    }

    void OnWin()
    {
        CharacterController.instance.CharStatus = CharStatusEnum.Win;

        inGameover = true;
        globalUpdateManager.instance.UnregisterUpdateDg(playerContorl);
        globalUpdateManager.instance.UnregisterUpdateDg(gameStartTimer);
        globalUpdateManager.instance.UnregisterUpdateDg(TimerText.instance.ToUpdate);
   
    }

    public void button(int number)
    {
        start = true;
        Debug.Log("hit");
        if (allRowData[2] == number)
        {
            hitRightBead();
          
        }
        else
        {
            failhit();
        }
    }

    void playerContorl()
    {
        if (CharacterController.instance.SP <= 0)
        {
            return;
        }
        if (CharacterController.instance.CharStatus == CharStatusEnum.FallenDown)
        {
            return;
        }

        if (allRowData[2] == 2)
        {
            CharacterController.instance.CharStatus = CharStatusEnum.Embrace;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            hitRightBead();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            button(0);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            button(1);
        }
    }
    public float WrongLostSp;
   public  void failhit()
    {
        print("failhit");
        combo = 0;
        CharacterController.instance.SP -= WrongLostSp;
        CharacterController.instance.CharStatus = CharStatusEnum.FallenDown;

    }

    [SerializeField]
    GameObject PlayerPrefab;

    void serializeOneRow(int index)
    {
        if (EmbraceSpawnedCount * (TotalBean / SpecialTotalSpawn) < Finish)
        {
            EmbraceSpawnedCount++;
            SpawnSpecialBead();
            return;
        }

        int perBeadX = 0;
        int perBeadY = 0;

        Vector3 thisBeadVector3 = new Vector3(perBeadX + 1 * allBeadArray.Count - 1 , perBeadY, 0);
        GameObject go = Instantiate(beadObjectArray[allRowData[index]], thisBeadVector3, Quaternion.identity);
        allBeadArray.Add(go);

    }

    /*
     void serializeOneRow()
    {
        if (EmbraceSpawnedCount * (TotalBean / SpecialTotalSpawn) < Finish)
        {
            EmbraceSpawnedCount++;
            SpawnSpecialBead();
            return;
        }

        int perBeadX = -2;
        int perBeadY = 0;

        int thisRowSpawnNumber = Random.Range(0, 2);
        allRowData.Add(thisRowSpawnNumber);
        Vector3 thisBeadVector3 = new Vector3(perBeadX + 1 * allRowData.Count - 1 , perBeadY, 0);
        GameObject go = Instantiate(beadObjectArray[thisRowSpawnNumber], thisBeadVector3, Quaternion.identity);
        allBeadArray.Add(go);
    }
     */

    void SpawnSpecialBead()
    {
        int perBeadX = 0;
        int perBeadY = 0;

        int thisRowSpawnNumber = 2; 
        allRowData[9] = 2;
        Vector3 thisBeadVector3 = new Vector3(perBeadX + 1 * allBeadArray.Count - 1, perBeadY, 0);
        GameObject go = Instantiate(beadObjectArray[thisRowSpawnNumber], thisBeadVector3, Quaternion.identity);
        allBeadArray.Add(go);

        thisBeadVector3.y += 1.5f;
        GameObject go1 = Instantiate(PlayerPrefab, thisBeadVector3, Quaternion.identity);
        go1.transform.parent = go.transform;
        go1.transform.localScale = new Vector3(Random.Range(0.7f, 1.3f ) , Random.Range(0.7f, 1.3f), Random.Range(0.7f, 1.3f ) );
        NextCharTrans = go1.transform;
    }

}
