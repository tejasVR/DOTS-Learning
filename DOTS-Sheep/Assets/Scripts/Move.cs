using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{        
    void Update()
    {
        transform.Translate(0, 0, 0.08F);

        if (transform.position.z > 50F)
        {
            transform.position = new Vector3(Random.Range(-50, 50), 0, -50F);
        }
    }
}
