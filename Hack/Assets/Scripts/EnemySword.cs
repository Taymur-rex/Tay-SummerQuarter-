using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemySword : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    private Collider swordCollider;

    private void Awake()
    {
        swordCollider = GetComponent<Collider>();
        swordCollider.enabled = false;
    }

    public void EnableHitbox()
    {
        swordCollider.enabled = true;
    }

    public void DisableHitbox()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!swordCollider.enabled)
            return;

        Health health = other.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}