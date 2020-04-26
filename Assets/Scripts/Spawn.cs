using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject sheepPrefab;
    const int numSheep = 10000;
   
    void Start()
    {
        for (int i = 0; i < numSheep; i++)
        {
            Vector3 sheepPos = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            Instantiate(sheepPrefab, sheepPos, Quaternion.identity);
        }   
    }
}
