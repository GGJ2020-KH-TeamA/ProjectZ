using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject Item;

    void Start()
    {
        InvokeRepeating("SpawnItem", 0.5f, 1);
    }

    private void SpawnItem()
    {
        Instantiate(Item,transform);
    }
}
