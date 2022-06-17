using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    public EBullet eBullet;
    public SpawnEnemy spawner;
    public float attackRate, attackDuration, bulletForce, speedOffset, stopDistance, retreatDistance;
    Animator animator;
    bool attacking;
    float timeSinceAttack;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        timeSinceAttack += Time.deltaTime;
        if (timeSinceAttack > attackDuration) attacking = false;
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
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) > stopDistance)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, (player.moveSpeed + speedOffset) * Time.deltaTime);
        }
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < retreatDistance)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, (-player.moveSpeed + speedOffset) * Time.deltaTime);
        }
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
        attacking = true;
        animator.SetBool("Shooting", true);
        Vector2 bulletDir = (player.transform.position - gameObject.transform.position).normalized;
        Vector2 bulletVelocity = bulletDir * bulletForce;
        float bulletRotation = Mathf.Atan2(bulletDir.y, bulletDir.x) * Mathf.Rad2Deg;
        EBullet bullet = Instantiate(eBullet, gameObject.transform.position, Quaternion.Euler(0, 0, bulletRotation));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bulletVelocity;
        timeSinceAttack = 0;
    }
}