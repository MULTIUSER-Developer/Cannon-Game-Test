using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public Transform startPoint;
  public GameObject ObjectToFollow;
  public float speed = 100;
  public float followSpeed = 100;
  [SerializeField]
  private bool BulletExists;
  [SerializeField]
  private bool waitTimeOver = true;

  void checkIfBulletExists () {
    // Check if something with the tag "Bullet" exists
    if (GameObject.FindWithTag("Bullet")) {
      // If it does set BulletExists to true
      BulletExists = true;
      ObjectToFollow = GameObject.FindWithTag("Bullet");
    }
    else
    {
      // If not set it to false
      BulletExists = false;
    }
  }

  void Update()
  {
    checkIfBulletExists();

    if (!BulletExists && waitTimeOver && transform.position != startPoint.position) {
      // Move the camera
        transform.position = Vector3.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);
    } else if (BulletExists && ObjectToFollow.transform.position.x >= startPoint.position.x) {
      transform.position = Vector3.MoveTowards(transform.position, new Vector3(ObjectToFollow.transform.position.x, transform.position.y, transform.position.z), followSpeed * Time.deltaTime);
    }
  }
}
