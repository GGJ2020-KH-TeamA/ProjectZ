using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject Item;

    private void OnEnable() => PlayerControl.GameOverEvent += StopSpawnItem;
    private void OnDisable() => PlayerControl.GameOverEvent -= StopSpawnItem;

    void Start()
    {
        InvokeRepeating("SpawnItem", 0.5f, 1);
    }
    
    private void StopSpawnItem()
    {
        CancelInvoke("SpawnItem");
    }

    private void SpawnItem()
    {
        Instantiate(Item, transform);
    }
}
