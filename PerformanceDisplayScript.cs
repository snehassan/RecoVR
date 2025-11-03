using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceDisplayScript : MonoBehaviour
{
    [SerializeField] private PerformanceRecordScript recordPref;
    [SerializeField] private RecoverySlider sliderScript;
    [SerializeField] private Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        UpdatePerformance();
    }

    void UpdatePerformance()
    {
        //PlayerPrefs.SetString("Bat4BatsData", "0.02;" + System.DateTime.Now.ToString() + ";3;2~");
        string full = PlayerPrefs.GetString("Bat4BatsData", "");

        string[] splitData = full.Split('~');

        foreach (string s in splitData)
        {
            string[] furtherSplit = s.Split(';');

            if (furtherSplit.Length == 4)
            {
                string score = furtherSplit[0];
                string datetime = furtherSplit[1];
                string recovery = furtherSplit[2];
                string spawn = sliderScript.options[(int.Parse(furtherSplit[3]) - (int)sliderScript.slider.minValue)];

                PerformanceRecordScript pr = Instantiate(recordPref, parent);
                pr.SetUp(score, spawn, recovery, datetime);
            }
            else
                break;
        }

    }
}