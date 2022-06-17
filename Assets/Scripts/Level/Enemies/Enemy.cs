using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    public FirePoint firePoint;
    public SpawnEnemy spawner;
    public float speedOffset, stopDistance, retreatDistance;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        if (!animator.GetBool("Dead"))
        {
            MoveEnemy();
            AnimateEnemy();
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
}