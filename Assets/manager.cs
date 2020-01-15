using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : SingletonMonoBehavior<manager>
{
    public GameObject[] beadObjectArray;
    public List<int> allRowData;
    public List<GameObject> allBeadArray;

    public int Finish = 0;
   


    public int combo = 0;

  

    public int TotalBean = 150;

    public float Timer = 0;

    public bool inGameover = false;
    public bool start = false;

    // Use this for initialization
    void Start()
    {
        serializeOneRow();
        serializeOneRow();
        serializeOneRow();
        serializeOneRow();
        serializeOneRow();
        serializeOneRow();
        serializeOneRow();
        serializeOneRow();
        serializeOneRow();
        serializeOneRow();
        serializeOneRow();

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

   

    void hitRightBead()
    {
        combo++;
        Finish++;
        FPSS.instance.RecordFinish();

        //gameStartTimeLeft = 2;
        allRowData.RemoveAt(0);
        Destroy(allBeadArray[0]);
        allBeadArray.RemoveAt(0);
        allRowFixPos();
        serializeOneRow();

        if (Finish >= TotalBean)
        {
            print("End");
            OnWin();
        }
    }

    void OnWin()
    {
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
    void failhit()
    {
        print("failhit");
        combo = 0;
        CharacterController.instance.SP -= WrongLostSp;
        CharacterController.instance.CharStatus = CharStatusEnum.FallenDown;

    }




    void serializeOneRow()
    {
        int perBeadX = -2;
        int perBeadY = 0;

        int thisRowSpawnNumber = Random.Range(0, 2);
        allRowData.Add(thisRowSpawnNumber);
        Vector3 thisBeadVector3 = new Vector3(perBeadX + 1 * allRowData.Count - 1 , perBeadY, 0);
        GameObject go = Instantiate(beadObjectArray[thisRowSpawnNumber], thisBeadVector3, Quaternion.identity);
        allBeadArray.Add(go);
    }



}
