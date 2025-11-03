using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PerformanceRecordScript : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text spawnRangeText;
    public TMP_Text recoveryStageText;
    public TMP_Text dateTimeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(string score, string spawn, string recovery, string datetime)
    {
        scoreText.text = score;
        spawnRangeText.text = spawn;
        recoveryStageText.text = recovery;
        dateTimeText.text = datetime;
    }
}
