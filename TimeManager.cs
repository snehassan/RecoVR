using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimeManager : MonoBehaviour
{
    [SerializeField] private EnemySpawnerScript enemySpawner;
    [SerializeField] public float prepTime = 15;
    [SerializeField] public float totalTimer;
    [SerializeField] private float waveTime = 30;
    [SerializeField] private TMP_Text PrepTimeText;
    [SerializeField] private TMP_Text TotalTimeText;

    private int currentWave = 1;
    private int prevWave;
    private bool gameStarted;
    private bool gameEnded;
    private bool running = true;

    private GameEventsManagerScript GEManager;
    private void Awake()
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
    void GameOver()
    {
        running = false;
    }
    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if(running)
            totalTimer += Time.deltaTime;

        currentWave = (int)((totalTimer + prepTime) / waveTime) - 1;

        if(totalTimer > prepTime && !gameStarted)
        {
            StartCoroutine(SpawnRoutine());
            gameStarted = true;
            PrepTimeText.transform.parent.gameObject.SetActive(false);
        }
        else if(!gameStarted)
        {
            PrepTimeText.transform.parent.gameObject.SetActive(true);

            PrepTimeText.text = (prepTime - totalTimer).ToString("F1");
        }

        if(!gameEnded && gameStarted)
        {
            TotalTimeText.text = (totalTimer - prepTime).ToString("F1");
            TotalTimeText.gameObject.SetActive(true);
        }
    }

    IEnumerator SpawnRoutine()
    {
        while(true && running)
        {
            enemySpawner.SpawnEnemyAtRandomPosition(Mathf.Clamp(1 * currentWave, 2, 7));
            yield return new WaitForSeconds(waveTime);
        }
    }
}
