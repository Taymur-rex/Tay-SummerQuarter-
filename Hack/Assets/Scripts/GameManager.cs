using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Stores the one (and only) instance of this script
    public static GameManager Instance {get; private set;}
    public static bool isGameOver = false;
    [SerializeField] private float gameOverDelay = 2f;
    [SerializeField] private int score = 0;


    private void Awake()
    {
        // Check our singleton
        if (Instance == null)
        {
            // Assign this instance of the script as THE instance
            Instance = this; 
        }
        else // There is already a GameManager assigned
        {
            // Destroy this extra copy of this script
            Destroy(gameObject);
        }

        score = 0;
        UIManager.Instance?.UpdateScore(score);
        isGameOver = false;
    } 

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        StartCoroutine(GameOverRoutine());
    }

    public void EarnPoints(int value)
    {
        // Add the points to the score
        score += value;
        // Update the UI
        UIManager.Instance.UpdateScore(score);
    }


    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(gameOverDelay);

        UIManager.Instance.ToggleGameOverUI(true);
    }

    public void LoadMainMenu()
    {
        // Play UI Audio
        // Stop Death Audio
        AudioManager.Instance.StopSound("death");
        // Load the Main Menu Scene
        SceneManager.LoadScene(0);
    }

    public void LoadCurrentScene()
    {
        // Play UI Audio
        // Stop Death Audio
        AudioManager.Instance.StopSound("death");
        // Restarts the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}