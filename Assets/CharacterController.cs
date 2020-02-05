using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharStatusEnum {
    Normal,
    FallenDown,
    Embrace,
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
   public  Transform PlayerViewObjectTransform;

    [SerializeField]
    AnimationCurve Curve;

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

                SPCounting();

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
            case CharStatusEnum.Embrace:
                if (FirstTimeEntry)
                {
                    EmbraceDecideUIView.instance.SetOpen();
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {

                    switch (EmbraceDecide.instance.GetIsHitDecideArea()) //懶
                    {
                        case "PREFECT":
                            SP = MaxSP;
                            OnRelayCorrectly();
                            break;
                        case "Good":
                            SP += MaxSP * 0.35f;

                            OnRelayCorrectly();

                            break;
                        case "MISS":
                            OnRelayCorrectly();
                            manager.instance.failhit();
                            break;
                        default:
                            break;
                    }


                    EmbraceDecideUIView.instance.SetClose();
                }
                break;
            default:
                break;
        }
    }

    void OnRelayCorrectly()
    {
        //old player
        PlayerViewObjectTransform.parent = manager.instance.allBeadArray[2].transform;
        manager.instance.hitRightBead();

        PlayerViewObjectTransform = manager.instance.NextCharTrans;
        manager.instance.NextCharTrans = null;
        PlayerViewObjectTransform.parent = null;
        CharStatus = CharStatusEnum.Normal;
    }

    float FallenDownTimer = 0;

    void SPCounting()
    {
        float CurvedVal = Curve.Evaluate( FPSS.instance.FinshPerSpecifySec ) ;

        float val = CurvedVal <= 0 ? -5 : CurvedVal;
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
