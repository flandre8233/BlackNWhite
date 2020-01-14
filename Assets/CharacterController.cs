using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharStatusEnum {
    Normal,
    FallenDown
}

public class CharacterController : SingletonMonoBehavior<CharacterController>
{
    public CharStatusEnum CharStatus;
    public CharStatusEnum CharStatusCopy;

    bool FirstTimeEntry;

    public float MaxSP;

    public float SP;

    public float SPRecoverPerSec;
    public float multiple;

    [SerializeField]
    Transform PlayerViewObjectTransform;

    // Start is called before the first frame update
    void Start()
    {
        SP = MaxSP;
        CharStatusCopy = CharStatus;
    }

    // Update is called once per frame
    void Update()
    {
        SPCounting();
        FirstTimeEntry = CharStatusCopy != CharStatus;
        CharStatusCopy = CharStatus;
        switch (CharStatus)
        {
            case CharStatusEnum.Normal:
                if (FirstTimeEntry)
                {

                }
                break;
            case CharStatusEnum.FallenDown:
                if (FirstTimeEntry)
                {
                    StartCoroutine( FallenDownCounter() );
                    PlayerViewObjectTransform.eulerAngles = new Vector3(0, 0, -35);
                }


                break;
            default:
                break;
        }
    }

    IEnumerator FallenDownCounter()
    {
        yield return new WaitForSeconds(1f);
        PlayerViewObjectTransform.eulerAngles = new Vector3(0, 0, 0);
        CharStatus = CharStatusEnum.Normal;
    }

    void SPCounting()
    {
        float val = FPSS.instance.FinshPerSpecifySec <= 0 ? -5 : FPSS.instance.FinshPerSpecifySec;
        SP += (SPRecoverPerSec - val) * multiple * Time.deltaTime;
        SP = MyClamp(SP, 0, MaxSP);

        if (SP <= 0 )
        {
            CharStatus = CharStatusEnum.FallenDown;
            //SP = 50;
        }
    }

    float MyClamp(float Val, float min, float max)
    {
        if (Val <= min)
        {
            return min;
        }

        if (Val >= max)
        {
            return max;
        }

        return Val;
    }
}
