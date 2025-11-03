using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float health = 100;
    public bool isDead;
    [SerializeField] private TimeManager tManager;
    [SerializeField] private TMPro.TMP_Text healthText;
    private GameEventsManagerScript GameEventsManager;
    // Start is called before the first frame update
    void Awake()
    {
        GameEventsManager = GameObject.Find("GameEventsManager").GetComponent<GameEventsManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, 100);
        healthText.text = "+"+health.ToString();
        if(health <= 0 && !isDead)
        {
            Die();
            GameEventsManager.OnGameOver?.Invoke();
        }
    }
    void Die()
    {
        isDead = true;
        string oldString = PlayerPrefs.GetString("Bat4BatsData", "");

        PlayerPrefs.SetString("Bat4BatsData", oldString + (tManager.totalTimer - tManager.prepTime).ToString("F2") + ";" + System.DateTime.Now.ToString() +
            ";" + PlayerPrefs.GetInt("RecoveryStage") + ";" + PlayerPrefs.GetInt("SpawnRange") + "~");

        Debug.Log(PlayerPrefs.GetString("Bat4BatsData"));
    }

    public void Damage(int val)
    {
        health -= val;
    }
}
