using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SliderViewUI : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    [SerializeField]
    RectTransform DecideView ;

    // Update is called once per frame
    void Update()
    {
        DecideHitView();
        SPsliderView();
    }

    void DecideHitView()
    {
        float NewVal = EmbraceDecide.instance.GetDecideVal / 100;
        DecideView.anchorMin = new Vector2(NewVal, DecideView.anchorMin.y);
        DecideView.anchorMax = new Vector2(NewVal, DecideView.anchorMax.y);

    }

    void SPsliderView()
    {
        slider.value = 1 - ( (CharacterController.instance.MaxSP - CharacterController.instance.SP) / CharacterController.instance.MaxSP);
    }
}
