using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 3f;

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
        Enemy enemy = collision.transform.GetComponent<Enemy>();
        // Destroy the bullet whenever it hits an enemy
        if (enemy != null) 
        {
            // Destroy this bullet
            Destroy(gameObject);
            // Destory the enemy
            Destroy(collision.gameObject);
        }
        
        
    }
}