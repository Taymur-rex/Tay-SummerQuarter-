using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 25;

    private void Start()
    {
        // Automatically despawn after a few seconds
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move forward every frame
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // TODO: Damage enemies here later
        Health1 health = collision.transform.GetComponent<Health1>();
        // Destroy the bullet whenever it hits an enemy
        if (health != null) 
        {
            Debug.Log("Bullet hit enemy");
            // Deal Damage to the Health
            health.TakeDamage(damage);
            // Destory the enemy
            Destroy(gameObject);
            // Add POints
            GameManager.Instance.EarnPoints(10);
        }
        Debug.Log("Collision Detected");
    }
}