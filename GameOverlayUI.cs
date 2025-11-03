using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOverlayUI : MonoBehaviour
{
    private GameEventsManagerScript GEManager;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TimeManager tManager;
    // Start is called before the first frame update
    void Awake()
    {

        GEManager = GameObject.Find("GameEventsManager").GetComponent<GameEventsManagerScript>();
    }
    private void OnEnable()
    {
        GEManager.OnGameOver += GameOver;
    }
    private void OnDisable()
    {
        GEManager.OnGameOver -= GameOver;
    }
    // Update is called once per frame
    void Update()
    {

    }

    void GameOver()
    {
        GameOverPanel.SetActive(true);
        scoreText.text = "Time Survived : " + (tManager.totalTimer-tManager.prepTime).ToString("F2") + "s";
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
