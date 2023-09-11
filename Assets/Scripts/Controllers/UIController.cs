using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] CannonController controller;
    [SerializeField] Slider _slider;
    [SerializeField] TextMeshProUGUI _powerText;

    void Update()
    {
        _slider.value = controller.Power / 100;
        _powerText.text = controller.Power.ToString("0");
    }
}
