using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI 참조")]
    public GameObject titleScreenPanel;
    public GameObject hudPanel;
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;  // ✅ Game Clear UI 추가
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI finalScoreText; 
    public TextMeshProUGUI clearScoreText;  // ✅ Game Clear 점수
    public TextMeshProUGUI clearTimeText;   // ✅ Game Clear 시간

    [Header("게임 상태")]
    private int score = 0;
    private float playTime = 0f;
    private bool isPlaying = false;
    private int health = 3;

    void Start()
    {
        ShowTitleScreen();
        UpdateScoreUI();
        UpdateTimeUI();
        UpdateHealthUI();
    }

    void Update()
    {
        if (isPlaying)
        {
            playTime += Time.deltaTime;
            UpdateTimeUI();
        }
    }

    // ✅ Start 화면
    void ShowTitleScreen()
    {
        titleScreenPanel.SetActive(true);
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false); // ✅ GameClear도 숨김
        Time.timeScale = 0f;
        isPlaying = false;
    }

    // ✅ 게임 시작
    public void StartGame()
    {
        titleScreenPanel.SetActive(false);
        hudPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false); // ✅ GameClear 숨김
        Time.timeScale = 1f;
        score = 0;
        playTime = 0f;
        health = 3;
        isPlaying = true;
        UpdateScoreUI();
        UpdateTimeUI();
        UpdateHealthUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthUI();
        if (health <= 0)
        {
            GameOver();
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void UpdateTimeUI()
    {
        if (timeText != null)
        {
            int minutes = Mathf.FloorToInt(playTime / 60f);
            int seconds = Mathf.FloorToInt(playTime % 60f);
            timeText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = "Health: " + health;
    }

    // ✅ 게임 오버
    void GameOver()
    {
        Debug.Log("💀 Game Over!");
        isPlaying = false;
        Time.timeScale = 0f;
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        if (finalScoreText != null)
            finalScoreText.text = "Final Score: " + score;
    }

    // ✅ 게임 클리어 추가
    public void GameClear()
    {
        Debug.Log("🎉🎉 Game Clear! 🎉🎉");
        isPlaying = false;
        Time.timeScale = 0f;

        hudPanel.SetActive(false);
        gameClearPanel.SetActive(true);

        if (clearScoreText != null)
            clearScoreText.text = "Score: " + score;

        if (clearTimeText != null)
        {
            int minutes = Mathf.FloorToInt(playTime / 60f);
            int seconds = Mathf.FloorToInt(playTime % 60f);
            clearTimeText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
