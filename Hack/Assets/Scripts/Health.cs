using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        Debug.Log($"{gameObject.name} HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die(); 
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
