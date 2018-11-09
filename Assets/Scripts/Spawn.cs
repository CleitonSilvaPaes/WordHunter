using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    private Transform player;
    public GameObject item;

    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void SpawnItem()
    {
        item.GetComponent<SpriteRenderer>().sortingOrder = 12;
        Vector2 playerPos = new Vector2(player.position.x + 2, player.position.y);
        Instantiate(item.transform, playerPos, Quaternion.identity);
    }
}