using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private int playerLives = 99;
    public bool timerGoing { get; private set; }
    private float score = 0f;
    private float pointsPerSecond = 10;
    private LevelModifier levelModifier;

    private void Awake()
    {
        timerGoing = false;
        scoreText.text = "0";
        livesText.text = "Lives: " + playerLives.ToString();
        levelModifier = FindObjectOfType<LevelModifier>();
    }

    private void OnEnable()
    {
        Enemy.playerHit += UpdateStrikes;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        int countdownTime = 3;
        countdownText.text = countdownTime.ToString();
        countdownText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);

        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
        BeginGame();
    }

    private void BeginGame()
    {
        score = 0f;
        scoreText.text = score.ToString();
        playerLives = 99;
        livesText.text = "Lives: " + playerLives.ToString();
        levelModifier.PlaceEnemies();
        StartTimer();
    }

    IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            score += pointsPerSecond * Time.deltaTime;
            scoreText.text = Mathf.FloorToInt(score).ToString();
            yield return null;
        }
    }

    private void StartTimer()
    {
        timerGoing = true;
        StartCoroutine(UpdateTimer());
    }

    private void StopTimer()
    {
        timerGoing = false;
    }

    private void UpdateScore()
    {
        score += 50;
    }

    private void UpdateStrikes()
    {
        playerLives--;
        livesText.text = "Lives: " + playerLives.ToString();

        if (playerLives <= 0)
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        Time.timeScale = 0f;
        StopTimer();

        //if (PlayerPrefs.GetInt("highscore", 0) < Mathf.FloorToInt(score))
        //{
        //    texts[2].text = "New High Score!";
        //    texts[3].text = texts[1].text;
        //    PlayerPrefs.SetInt("highscore", Mathf.FloorToInt(score));
        //}

        //else
        //{
        //    texts[2].text = "Final Score: " + texts[1].text;
        //    texts[3].text = "High Score: " + PlayerPrefs.GetInt("highscore", 0).ToString();
        //}

        //panels[1].SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        StopTimer();
        //panels[0].SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        StartTimer();
        //panels[0].SetActive(false);
        //gameplayObjects[0].SetActive(true);
    }

    public void RestartGame()
    {
        //panels[0].SetActive(false);
        //panels[1].SetActive(false);
        score = 0f;
        scoreText.text = score.ToString();
        playerLives = 3;
        livesText.text = "Lives: " + playerLives.ToString();
        Time.timeScale = 1f;
        StartCoroutine(CountdownToStart());
    }

    private void OnDisable()
    {
        Enemy.playerHit -= UpdateStrikes;
    }
}
