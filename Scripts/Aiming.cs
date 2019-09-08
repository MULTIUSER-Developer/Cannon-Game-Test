using UnityEngine;
using System.Collections;

public class Aiming : MonoBehaviour
{
    public GameObject shotPrefab;

    public GameObject exit_Point;

    private Rigidbody2D rb;

    public float speed;
    private object player;

    private void Start()
    {
            player = GameObject.Find("Bullet");
            rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            speed += 50 * Time.deltaTime;
        }
        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 exitPoint = new Vector2(exit_Point.transform.position.x, exit_Point.transform.position.y);
        Vector2 direction = target - exitPoint;
        direction.Normalize();

        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        transform.rotation = rotation;

        if (Input.GetMouseButtonUp(0))
        {
            GameObject shot = (GameObject)Instantiate(shotPrefab, exitPoint, rotation);
            rb = shot.GetComponent<Rigidbody2D>();
            rb.velocity =  * speed;
        }

    }
}