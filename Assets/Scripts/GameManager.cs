using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Intro,
        Playing,
        Dead
    }

    public static GameManager Instance;
    public PlayerController playerController;
    public GameState State = GameState.Intro;

    public int hp = 3;
    public float playStartTime;

    public TMP_Text score_text;

    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject BuildingSpawner;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenBaechuSpawner;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        IntroUI.SetActive(true);
    }

    float CalculateScore()
    {
        return Time.time - playStartTime;
    }

    void SaveHighScore()
    {
        int score = Mathf.FloorToInt(CalculateScore());
        int currentHighScore = PlayerPrefs.GetInt("highScore");

        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
        }
    }

    public float CalculateGameSpeed()
    {
        float initial_speed = 5f;
        float max_speed = 30f;
        float speed = initial_speed + (0.5f * Mathf.Floor(CalculateScore() / 10f));

        if (State != GameState.Playing)
            return initial_speed;

        return Mathf.Min(speed, max_speed);
    }

    void Update()   
    {
        //if (State == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        if (State == GameState.Intro && Input.GetMouseButtonDown(0))
        {
            State = GameState.Playing;
            IntroUI.SetActive(false);
            DeadUI.SetActive(false);
            BuildingSpawner.SetActive(true);
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldenBaechuSpawner.SetActive(true);

            playStartTime = Time.time;
        }

        if (State == GameState.Playing)
        {
            score_text.text = "Score : " + Mathf.FloorToInt(CalculateScore());
        }
        else if (State == GameState.Dead)
            score_text.text = "High Score : " + GetHighScore();

        if (State == GameState.Playing && hp == 0)
        {
            playerController.Died();

            BuildingSpawner.SetActive(false);
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenBaechuSpawner.SetActive(false);

            State = GameState.Dead;
            DeadUI.SetActive(true);
            
            SaveHighScore();
        }

        //if (State == GameState.Dead && Input.GetKeyDown(KeyCode.Space))
        if (State == GameState.Dead && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("main");
        }
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScore");
    }

    public void Quit()
    {
        Debug.Log("프로그램 종료합니다.");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
