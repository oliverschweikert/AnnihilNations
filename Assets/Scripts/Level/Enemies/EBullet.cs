using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet : MonoBehaviour
{
    public Player player;
    void Update()
    {
        if (Vector2.Distance(gameObject.transform.position, player.transform.position) > 20)
        {
            Destroy(gameObject);
        }
    }
}
