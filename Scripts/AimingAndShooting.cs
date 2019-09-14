using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AimingAndShooting : MonoBehaviour
{
    // The shot prefab game object (the thingie that will be spawned), will be set in the editor
    public GameObject shotPrefab;
    // The exit point game object, will be set in the editor
    public GameObject exit_Point;
    // Rigidbody called rb
    private Rigidbody2D rb;
    // Speed variable
    //public float speed;
    // Serialize Field shows the private value in the unity editor
    [SerializeField]
    // bool to to check if the max bullet count is reached
    private bool BulletExists = false;
    // Pretty self explanatory
    bool IsGamePaused = false;
    public GameObject theCanvas;
    // Get a reference to the slider we want to use as a power meter
    public Slider PowerMeter;
    public Transform PowerMeterTransformPoint;
    public float powerMeterIncrease = 5;
    public bool powerMeterUp = true;
    public GameObject PowerMeterGameObject;
    private bool LMBDown = false;

    void shoot(Vector2 exitPoint, Quaternion rotation, Vector2 direction) {
      // Create the shot using the prefab, exit and rotation calculated and save a reference to the newly created game object
      GameObject shot = (GameObject)Instantiate(shotPrefab, exitPoint, rotation);
      // Get the rigidbody component of the gameobject
      rb = shot.GetComponent<Rigidbody2D>();
      // Use the rigidbody component to yeet the thingie away
      rb.AddForce(direction * (PowerMeter.value * 1000));
      PowerMeter.value = 0;
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

    Vector2 fixDirection() {
      Vector2 direction = getDirection();

      if (direction.x > 0) {
        return direction;
      } else {
        return -direction;
      }
    }

    Quaternion getRotation () {
      // Calculate the quaternion (rotation)
      Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(fixDirection().y, fixDirection().x) * Mathf.Rad2Deg);
      // Return the rotation variable
      return rotation;
    }

    void setRotation () {
      Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector3 lookAt = mouseScreenPosition;
      float AngleRad = Mathf.Atan2(lookAt.y - this.transform.position.y, lookAt.x - this.transform.position.x);
      float AngleDeg = (180 / Mathf.PI) * AngleRad;
      this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }

    void checkIfBulletExists () {
      // Check if something with the tag "Bullet" exists
      if (GameObject.FindWithTag("Bullet")) {
        // If it does set BulletExists to true
        BulletExists = true;
      }
      else
      {
        // If not set it to false
        BulletExists = false;
      }
    }

    void Start ()
    {
      theCanvas = GameObject.Find("Canvas");
    }

    void FixedUpdate() {
      setRotation();
      checkIfBulletExists();
    }

    void isTheGamePaused()
    {
      IsGamePaused = theCanvas.GetComponent<Pausemenu>().GameIsPaused;
    }

    void PowerMeterUpOrDown () {
    if (PowerMeter.value == 0) {
        powerMeterUp = true;
      } else if (PowerMeter.value == 10) {
        powerMeterUp = false;
      }
    }

    void MovePowerMeter () {
      if (powerMeterUp) {
        PowerMeter.value += powerMeterIncrease * Time.deltaTime;
      } else {
        PowerMeter.value -= powerMeterIncrease * Time.deltaTime;
      }
    }

    void Update()
    {
      isTheGamePaused();

      if (Input.GetMouseButton(0) && !BulletExists && !IsGamePaused) {
        PowerMeterUpOrDown();
        MovePowerMeter();
        PowerMeterGameObject.SetActive(true);
      } else {
        PowerMeterGameObject.SetActive(false);
      }

      if (Input.GetMouseButtonUp(0) && !BulletExists && !IsGamePaused) {
        shoot(getExitPoint(), getRotation(), fixDirection());
      }

      PowerMeter.transform.rotation = transform.rotation;
      PowerMeter.transform.position = PowerMeterTransformPoint.transform.position;
    }
}
