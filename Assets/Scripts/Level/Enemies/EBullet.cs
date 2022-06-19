using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet : MonoBehaviour
{
    public Player player;
    public Enemy enemy;
    void Update()
    {
        if (Vector2.Distance(gameObject.transform.position, player.transform.position) > 20)
        {
            enemy.currentBullets.Remove(gameObject.GetComponent<EBullet>());
            Destroy(gameObject);
        }
    }
}
