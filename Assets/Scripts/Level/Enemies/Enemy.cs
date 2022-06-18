using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    public EBullet eBullet;
    public SpawnEnemy spawner;
    public float attackRate, attackDuration, bulletForce, speedOffset, stopDistance, retreatDistance;
    public int wanderRadius;
    Animator animator;
    bool attacking, wandering;
    float timeSinceAttack;
    Vector3 wanderPos;
    int[] multiplier = new int[] { 1, -1 };
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartWandering();
    }
    void Update()
    {
        timeSinceAttack += Time.deltaTime;
        if (timeSinceAttack > attackDuration)
        {
            attacking = false;
            if (timeSinceAttack < attackRate)
                wandering = true;
            else
                wandering = false;
        };
        if (!animator.GetBool("Dead"))
        {
            if (!attacking)
                MoveEnemy();
            AnimateEnemy();
            if (CanShoot())
                AttackPlayer();
        }
    }
    private void AnimateEnemy()
    {
        var lookDir = player.transform.position - GetComponent<Transform>().position;
        animator.SetFloat("PlayerX", lookDir.normalized.x);
        animator.SetFloat("PlayerY", lookDir.normalized.y);
    }
    private void MoveEnemy()
    {
        if (!wandering)
        {
            if (Vector3.Distance(player.transform.position, gameObject.transform.position) > stopDistance)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, (player.moveSpeed + speedOffset) * Time.deltaTime);
            }
            if (Vector3.Distance(player.transform.position, gameObject.transform.position) < retreatDistance)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, (-player.moveSpeed + speedOffset) * Time.deltaTime);
            }
        }
        if (wandering)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, wanderPos, (player.moveSpeed + speedOffset) * Time.deltaTime);
        }
    }
    private void StartWandering()
    {
        var randomX = (Random.Range(0, wanderRadius) + 10) * multiplier[Random.Range(0, 2)];
        var randomY = (Random.Range(0, wanderRadius) + 10) * multiplier[Random.Range(0, 2)];
        Vector3 wanderVector = new Vector3(randomX, randomY);
        wanderPos = wanderVector + player.transform.position;
        wandering = true;
    }
    private bool CanShoot()
    {
        return Vector3.Distance(player.transform.position, gameObject.transform.position) >= retreatDistance
        && Vector3.Distance(player.transform.position, gameObject.transform.position) <= stopDistance
        && timeSinceAttack > attackRate;
    }
    public void Kill()
    {
        spawner.UpdateRemaining();
        GetComponent<Rigidbody2D>().simulated = false;
        animator.SetBool("Dead", true);
    }
    private void FadeCorpse()
    {
        Destroy(gameObject);
    }
    private void AttackPlayer()
    {
        wandering = false;
        attacking = true;
        animator.SetBool("Shooting", true);
        Vector2 bulletDir = (player.transform.position - gameObject.transform.position).normalized;
        Vector2 bulletVelocity = bulletDir * bulletForce;
        float bulletRotation = Mathf.Atan2(bulletDir.y, bulletDir.x) * Mathf.Rad2Deg;
        EBullet bullet = Instantiate(eBullet, gameObject.transform.position, Quaternion.Euler(0, 0, bulletRotation));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bulletVelocity;
        timeSinceAttack = 0;
        StartWandering();
    }
}