using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharStatusEnum {
    Normal,
    FallenDown,
    Win
}

public class CharacterController : SingletonMonoBehavior<CharacterController>
{
    public CharStatusEnum CharStatus;
    CharStatusEnum CharStatusCopy;

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
        globalUpdateManager.instance.registerUpdateDg(ToUpdate);
    }

    private void OnDestroy()
    {
        globalUpdateManager.instance.UnregisterUpdateDg(ToUpdate);
    }

    // Update is called once per frame
    void ToUpdate()
    {
        SPCounting();
        StatusAdmin();
    }

    

    void StatusAdmin()
    {
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
                    PlayerViewObjectTransform.eulerAngles = new Vector3(0, 0, -35);
                    FallenDownTimer = 0;
                }

                FallenDownTimer += Time.deltaTime;
                if (FallenDownTimer >= 1)
                {
                    PlayerViewObjectTransform.eulerAngles = new Vector3(0, 0, 0);
                    CharStatus = CharStatusEnum.Normal;
                }
                break;
            default:
                break;
        }
    }

    float FallenDownTimer = 0;

    void SPCounting()
    {
        float val = FPSS.instance.FinshPerSpecifySec <= 0 ? -5 : FPSS.instance.FinshPerSpecifySec;
        val *= multiple;
        SP += (SPRecoverPerSec - val) * Time.deltaTime;
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
