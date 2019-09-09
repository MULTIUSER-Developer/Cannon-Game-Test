﻿using UnityEngine;
using System.Collections;

public class AimingAndShooting : MonoBehaviour
{
    // The shot prefab game object (the thingie that will be spawned), will be set in the editor
    public GameObject shotPrefab;
    // The exit point game object, will be set in the editor
    public GameObject exit_Point;
    // Rigidbody called rb
    private Rigidbody2D rb;
    // Speed variable
    public float speed;

    //--------- Seb Thingies ----------
    // The maximum number of bullets allowed to exist at one time
    int maxBulletCount = 1;
    // Serialize Field shows the private value in the unity editor
    [SerializeField]
    // bool to to check if the max bullet count is reached
    private bool BulletExists = false;

    void shoot(Vector2 exitPoint, Quaternion rotation, Vector2 direction) {
      // Create the shot using the prefab, exit and rotation calculated and save a reference to the newly created game object
      GameObject shot = (GameObject)Instantiate(shotPrefab, exitPoint, rotation);
      // Get the rigidbody component of the gameobject
      rb = shot.GetComponent<Rigidbody2D>();
      // Use the rigidbody component to yeet the thingie away
      rb.AddForce(direction * (speed * 1000));
    }

    Vector2 getTarget () {
      // Get the mouse position and set that as the target variable
      Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
      // Return the target value
      return target;
    }

    Vector2 getExitPoint () {
      getTarget();
      // Set the exitPoint variable to the position of the exit_Point object
      Vector2 exitPoint = new Vector2(exit_Point.transform.position.x, exit_Point.transform.position.y);
      // Return the exit point
      return exitPoint;
    }

    Vector2 getDirection () {
      Vector2 target = getTarget();

      // Get the exit point value and assign it to the exitPoint variable
      Vector2 exitPoint = getExitPoint();
      // Set the direction variable to the direction which is the vector 2 of the target - the vector 2 of the exit point
      Vector2 direction = target - exitPoint;
      // Round off the vector 2
      direction.Normalize();
      // return the direction variable
      return direction;
    }

    Quaternion getRotation () {
      // Calculate the quaternion (rotation)
      Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(getDirection().y, getDirection().x) * Mathf.Rad2Deg);
      // Return the rotation variable
      return rotation;
    }

    void setRotation () {
      Quaternion rotation = getRotation();

      // Set the rotation you just calculated to the objects rotation
      transform.rotation = rotation;
    }

    void FixedUpdate() {
      setRotation();
    }

    void Update()
    {
        // If the left mouse button is pressed create the object
        if (Input.GetMouseButtonDown(0))
        {
            shoot(getExitPoint(), getRotation(), getDirection());
        }
      }
}
