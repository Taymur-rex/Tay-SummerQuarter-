using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Stores the one (and only) instance of this script
    public static GameManager Instance {get; private set;}
    [SerializeField] public static bool isGameOver = false;

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
    } 

    public void GameOver()
    {
        // Trigger Lose state UI
        // ...

        // Load the scene at build index 0
        SceneManager.LoadScene(0);
    }

    public void LoadMainMenu()
    {
        // Play UI Audio
        // Load the Main Menu Scene
        SceneManager.LoadScene(0);
    }

    public void LoadCurrentScene()
    {
        // Play UI Audio
        // Restarts the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}