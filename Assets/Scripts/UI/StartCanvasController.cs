using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject optionPopupUI;
    [SerializeField] private Slider backgroundSlider;
    [SerializeField] private Slider effectsSlider;
    AudioManager audioManager;
    
    public void OptionPopupUI()
    {
        optionPopupUI.SetActive(true);
        AudioManager.Instance.InitSliders(backgroundSlider, effectsSlider);
    }
}
