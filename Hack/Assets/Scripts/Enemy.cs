using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemySword sword;

    [Header("Combat")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float rotationSpeed = 10f;

    private float nextAttackTime;
    private bool isAttacking;

    public void Initialize(Transform player)
    {
        target = player;
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target == null)
            return;

        float distance = Vector3.Distance(transform.position, target.position);

        // ======================
        // ATTACK
        // ======================
        if (distance <= attackRange)
        {
            agent.isStopped = true;

            FaceTarget();

            if (!isAttacking && Time.time >= nextAttackTime)
            {
                StartAttack();
            }
        }
        // ======================
        // CHASE
        // ======================
        else
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }

        bool isWalking =
            !agent.isStopped &&
            agent.velocity.sqrMagnitude > 0.01f &&
            agent.remainingDistance > agent.stoppingDistance;

        animator.SetBool("IsWalking", isWalking);
    }

    private void StartAttack()
    {
        isAttacking = true;

        // Prevent another attack until cooldown expires
        nextAttackTime = Time.time + attackCooldown;

        animator.ResetTrigger("Attack");
        animator.SetTrigger("Attack");

        StartCoroutine(AttackRoutine());
    }

    private System.Collections.IEnumerator AttackRoutine()
    {
        // Wait for cooldown before allowing another attack
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }

    private void FaceTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerControlleer player = collision.gameObject.GetComponent<PlayerControlleer>();

        if (player != null && GameManager.isGameOver == false)
        {
            // play audio 
            AudioManager.Instance.PlaySound("death");
            Debug.Log($"{gameObject.name} hit {collision.gameObject.name}");
            // trigger the death of the player
            player.Die();
            // Trigger a game Over
            GameManager.Instance.GameOver();
        }
    }

    public void EnableSwordHitbox()
    {
        sword.EnableHitbox();
    }

    public void DisableSwordHitbox()
    {
        sword.DisableHitbox();
    }
}