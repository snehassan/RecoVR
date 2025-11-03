using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RecoverySlider : MonoBehaviour
{
    public Slider slider;
    [SerializeField] private TMP_Text text;
    [SerializeField] public string[] options;
    private string initialString;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        initialString = text.text;
        UpdateText();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText()
    {
        text.text = initialString + options[(int)(slider.value - slider.minValue)].ToString();
    }
}
