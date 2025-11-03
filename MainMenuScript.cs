using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Slider recoveryStageSlider;
    [SerializeField] private Slider spawnRangeSlider;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void LoadBat4Bats()
    {
        PlayerPrefs.SetInt("RecoveryStage", (int)recoveryStageSlider.value);
        PlayerPrefs.SetInt("SpawnRange", (int)spawnRangeSlider.value);

        SceneManager.LoadScene(1);

    }
}
