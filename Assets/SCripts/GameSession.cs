//using System;
//using TMPro;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class GameSession : MonoBehaviour
//{
//    [SerializeField] int playerLives = 3;
//    [SerializeField] int score = 0;
//    [SerializeField] TextMeshProUGUI livesText;
//    [SerializeField] TextMeshProUGUI ScoreText;

//    void Awake()
//    {
//        int numGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;

//        if (numGameSessions > 1)
//        {
//            Destroy(gameObject);
//        }
//        else
//        {
//            DontDestroyOnLoad(gameObject);
//        }
//    }
//    void Start()
//    {
//        livesText.text = playerLives.ToString();
//        ScoreText.text = score.ToString();
//    }

//    public void ProcessPlayerDeath()
//    {
//        if (playerLives > 1)
//        {
//            TakeLife();
//        }
//        else
//        {
//            ResetGameSession();
//        }
//    }

//    public void AddToScore(int pointsToAdd) 
//    { 
//        score += pointsToAdd;
//        ScoreText.text = score.ToString();
//    }

//     void TakeLife()
//    {
//        playerLives--;
//        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
//        SceneManager.LoadScene(currentSceneIndex);
//        livesText.text = playerLives.ToString();
//    }

//    void ResetGameSession()
//    {
//       //FindFirstObjectByType<ScenePersist>().ResetScenePersist();
//        SceneManager.LoadScene(0);
//        Destroy(gameObject);
//    }


//}


using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] int coins = 0;

    TextMeshProUGUI livesText;
    TextMeshProUGUI scoreText;

    void Awake()
    {
        int numGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        AssignUI();
        UpdateUI();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignUI();
        UpdateUI();
    }

    void AssignUI()
    {
        GameObject livesObj = GameObject.Find("Lives Text");
        GameObject scoreObj = GameObject.Find("Score Text");

        if (livesObj != null)
        {
            livesText = livesObj.GetComponent<TextMeshProUGUI>();
        }

        if (scoreObj != null)
        {
            scoreText = scoreObj.GetComponent<TextMeshProUGUI>();
        }
    }

    void UpdateUI()
    {
        if (livesText != null)
        {
            livesText.text = playerLives.ToString();
        }

        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        coins += 1;
        UpdateUI();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    void TakeLife()
    {
        playerLives--;
        UpdateUI();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void ResetGameSession()
    {
        playerLives = 3;
        score = 0;
        coins = 0;

        if (ScenePersist.instance != null)
        {
            ScenePersist.instance.ResetPersistence();
        }

        SceneManager.LoadScene(0);
    }
}