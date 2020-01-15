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
        slider.value = EmbraceDecide.instance.GetDecideVal/100;
    }
}
