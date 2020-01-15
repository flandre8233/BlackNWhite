using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbraceDecide : SingletonMonoBehavior<EmbraceDecide>
{
    [SerializeField]
    int minDecideVal;
    [SerializeField]
    int maxDecideVal;

    [SerializeField]
    float PingPongSpeed;

    [SerializeField]
    int DecideDiff;

    float DecideVal;

   public float GetDecideVal {
        get {
            return DecideVal;
        }
    }
    float GetDecidePoint {
        get {
            return (maxDecideVal + minDecideVal)/2 ;
        }
    }

    bool GetIsInDecideArea {
        get {
            return (DecideVal >= GetLeftSideDecideArea && DecideVal < GetRightSideDecideArea );
        }
    }

    float GetLeftSideDecideArea {
        get {
            return GetDecidePoint - DecideDiff;
        }
    }
    float GetRightSideDecideArea {
        get {
            return GetDecidePoint + DecideDiff;
        }
    }
    bool isToLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    float CurTime;
    // Update is called once per frame
    void Update()
    {
        CurTime += Time.deltaTime;
        if (isToLeft)
        {
            DecideVal = Mathf.Lerp(minDecideVal, maxDecideVal, CurTime / PingPongSpeed);
        }
        else
        {
            DecideVal = Mathf.Lerp(maxDecideVal, minDecideVal, CurTime / PingPongSpeed);
        }

        if (CurTime >= PingPongSpeed)
        {
            CurTime = 0;
            isToLeft = !isToLeft;
        }
    }
}
