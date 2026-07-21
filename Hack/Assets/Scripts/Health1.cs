using UnityEngine;

public class Health1 : MonoBehaviour
{
   [SerializeField] private int maxHP = 100;
   [SerializeField] private int currentHP = 0;
   [SerializeField] private int pointValue = 100;


    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        // Check for death
        if (currentHP <= 0){
            Die();
        }
    }

    private void Die(){
        // Destory the object 
        Destroy(gameObject);
        // Earn points
        GameManager.Instance.EarnPoints(pointValue);
    }
}
