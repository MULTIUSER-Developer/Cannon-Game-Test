using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "groundcol")
        {
            Debug.Log("hit");
            Destroy(gameObject, 2f);
        }
    }
}