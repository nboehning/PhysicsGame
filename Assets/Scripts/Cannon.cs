using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Cannon : MonoBehaviour
{
    GameObject cannonBall;

    [Header("Power Settings")]
    public float power = 30.0f;
    public float minPower = 20.0f;
    public float maxPower = 40.0f;

    [Header("Rotation Settings")]
    public float cannonRotation = 49.0f;        // Capture initial rotation
    public float minCannonRotation = 30.0f;     // Maximum rotation of cannon
    public float maxCannonRotation = 60.0f;     // Minimum rotation of cannon
    public float rotationStep = 5.0f;           // How many degrees to rotate per click

    [Header("Power Bar")]
    public Image powerBar;

    [Header("Rotation Radial")]
    public Image rotationRadial;

    [Header("Ammo Information")]
    public Image numShotsImage;
    public int numShots = 5;                    // Add a GUI to show available shots using cannonball graphics
    int maxShots;

    // Private stuff
    int totalStars;
    int numStars;
    float degToRad = 0.0174533f;

    // Use this for initialization
	void Start ()
    {
        maxShots = numShots;
        totalStars = GameObject.FindGameObjectsWithTag("Star").Length;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            // Increase the power
            if (power < maxPower)
            {
                power++;

                powerBar.GetComponent<Image>().fillAmount = (power - minPower) / (maxPower - minPower);
            }            
        }
        else if(Input.GetKey(KeyCode.A))
        {
            // Decrease the power
            if (power > minPower)
            {
                power--;

                powerBar.GetComponent<Image>().fillAmount = (power - minPower) / (maxPower - minPower);
            }            
        }

        if(Input.GetKey(KeyCode.W))
        {
            if(cannonRotation < maxCannonRotation)
            {
                cannonRotation += rotationStep;
                transform.Rotate(Vector3.forward * rotationStep);

                rotationRadial.GetComponent<Image>().fillAmount = (cannonRotation - minCannonRotation) / (maxCannonRotation - minCannonRotation);
            }
        }
        else if(Input.GetKey(KeyCode.S))
        {
            // Make sure cannon can't go below min rotation
            if (cannonRotation > minCannonRotation)
            {
                // Calculate the graphics perceived rotation
                cannonRotation -= rotationStep;
                // Actually rotate the cannon
                transform.Rotate(Vector3.back * rotationStep);

                rotationRadial.GetComponent<Image>().fillAmount = (cannonRotation - minCannonRotation) / (maxCannonRotation - minCannonRotation);
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (numShots > 0)
            {
                numShots--;
                numShotsImage.fillAmount = ((float)numShots / (float)maxShots);
                Cannonballs();
            }
        }

        if (numStars > 0 && numShots <= 0)
            Debug.Log("Game Over");
                
    }
    public void DestroyStar()
    {
        numStars++;
        if (numStars == totalStars)
            Debug.Log("Game Won");
    }

    void Cannonballs()
    {
        // Spawning the cannon ball
        GameObject cannonballInstance = (GameObject)Instantiate(Resources.Load("CannonBall"), transform.position, Quaternion.identity);
        cannonballInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(power * Mathf.Cos(cannonRotation * degToRad),
                                                                    power * Mathf.Sin(cannonRotation * degToRad));
    }
}
