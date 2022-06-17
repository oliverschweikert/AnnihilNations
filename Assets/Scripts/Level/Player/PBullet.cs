using UnityEngine;

public class PBullet : MonoBehaviour
{
    public Player player;
    public float removeDistance;
    void Update()
    {
        RemoveDistantBullets();
    }
    private void RemoveDistantBullets()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) > removeDistance)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<Enemy>().Kill();
        }
    }
}
