using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    private bool isColliding = false;
    private string collidingObjectName;
    public float despawnTime = 0f;

    [SerializeField]
    private Vector3 trans1;
    [SerializeField]
    private Vector3 trans2;
    public float TimeOnGround = 0;
    public float TimeOnGroundBeforeDespawnTime = 0.05f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
        collidingObjectName = collision.gameObject.name;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }

    void Update()
    {

        GameObject.Find("ScoreHandler").GetComponent<ScoreScript>().score = Mathf.RoundToInt(transform.position.x);

        if (isColliding && collidingObjectName == "groundcol")
        {
            TimeOnGround += 1 * Time.deltaTime;
        }
        if (TimeOnGround >= TimeOnGroundBeforeDespawnTime)
        {
            trans2 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            TimeOnGround = 0;
        }
        else if (TimeOnGround <= TimeOnGroundBeforeDespawnTime / 2)
        {
            trans1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
        if (trans1 == trans2 && trans1 != null && trans2 != null)
        {
            Destroy(gameObject, despawnTime);
        }
    }
}
