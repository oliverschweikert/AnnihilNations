using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Player player;
    public float speedOffset, stopDistance, retreatDistance;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
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
}
