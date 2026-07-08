using UnityEngine;
using TMPro; 


public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

[SerializeField] private TextMeshProUGUI scoreText; 
 
    private void Awake()
    {
      if (Instance == null)
      {
         Instance = this;
      }

      else
      {
        Destroy(gameObject);
      }
    
    }

    public void UpdateScore(int score)
    {
      scoreText.text = $"Score:{score} ";
    }
}
