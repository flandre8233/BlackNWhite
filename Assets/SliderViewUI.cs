using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SliderViewUI : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    // Update is called once per frame
    void Update()
    {
        SPsliderView();
    }

    void SPsliderView()
    {
        slider.value = 1 - ( (CharacterController.instance.MaxSP - CharacterController.instance.SP) / CharacterController.instance.MaxSP);
    }
}
