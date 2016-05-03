using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
public class Cannon : MonoBehaviour
{
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
    public Text rotationRadial;
    private char degSymbol;

    [Header("Ammo Information")]
    public Image numShotsImage;
    public int numShots = 5;                    // Add a GUI to show available shots using cannonball graphics
    int maxShots;

    [Header("Stars Destroyed")]
    public Text starCounter;
    
    // Private stuff
    private List<Rigidbody2D> rigidBodies;
    int totalStars;
    int numStars;
    float degToRad = 0.0174533f;
    private CameraFollow followScript;
    private bool hasWon;

    // Use this for initialization
	void Start ()
	{
	    degSymbol = rotationRadial.text[3];
	    followScript = Camera.main.gameObject.GetComponent<CameraFollow>();
        followScript.objectToFollow = transform;
        rotationRadial.GetComponent<Text>().text = cannonRotation + degSymbol.ToString();
        maxShots = numShots;
        totalStars = GameObject.FindGameObjectsWithTag("Star").Length;
        starCounter.text = numStars + "/" + totalStars;
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

                powerBar.fillAmount = (power - minPower) / (maxPower - minPower);
            }            
        }
        else if(Input.GetKey(KeyCode.A))
        {
            // Decrease the power
            if (power > minPower)
            {
                power--;

                powerBar.fillAmount = (power - minPower) / (maxPower - minPower);
            }            
        }

        if(Input.GetKey(KeyCode.W))
        {
            if(cannonRotation < maxCannonRotation)
            {
                cannonRotation += rotationStep;
                if (cannonRotation > maxCannonRotation)
                    cannonRotation = maxCannonRotation;
                transform.Rotate(Vector3.forward * rotationStep);

                rotationRadial.text = cannonRotation + degSymbol.ToString();
            }
        }
        else if(Input.GetKey(KeyCode.S))
        {
            // Make sure cannon can't go below min rotation
            if (cannonRotation > minCannonRotation)
            {
                // Calculate the graphics perceived rotation
                cannonRotation -= rotationStep;

                if (cannonRotation < minCannonRotation)
                    cannonRotation = minCannonRotation;

                // Actually rotate the cannon
                transform.Rotate(Vector3.back * rotationStep);
                rotationRadial.text = cannonRotation + degSymbol.ToString();
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (numShots > 0)
            {
                numShots--;
                numShotsImage.fillAmount = ((float)numShots / maxShots);
                Cannonballs();
            }
        }

        if (numStars < totalStars && numShots == 0 && followScript.objectToFollow == transform)
        {
            if (hasWon)
            {
                DisplayVictory();
            }
            else
            {
                DisplayLose();
            }
        }
                
    }
    public void DestroyStar(GameObject destroyedStar)
    {
        numStars++;
        starCounter.text = numStars + "/" + totalStars;
        for (int i = 0; i < rigidBodies.Count; i++)
        {
            if (destroyedStar == rigidBodies[i].gameObject)
                rigidBodies.RemoveAt(i);
        }

        CheckWin();
    }

    void Cannonballs()
    {
        // Spawning the cannon ball
        GameObject cannonballInstance = (GameObject)Instantiate(Resources.Load("CannonBall"), transform.position, Quaternion.identity);
        cannonballInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(power * Mathf.Cos(cannonRotation * degToRad),
                                                                    power * Mathf.Sin(cannonRotation * degToRad));
        followScript.objectToFollow = cannonballInstance.transform;
        StartCoroutine("CheckAsleep");
    }

    void DisplayVictory()
    {
        Debug.Log("You won the level!");
    }

    void DisplayLose()
    {
        Debug.Log("You Lost the level!");
    }

    void CheckWin()
    {
        if (numStars == totalStars)
        {
            LevelSelectController.curStarsEarned = 3;
            LevelSelectController.maxLevel++;
            DisplayVictory();
        }

        int minStarsNeeded = Mathf.RoundToInt(0.8f * totalStars);
        int twoStarScore = Mathf.RoundToInt(0.9f * totalStars);

        if (numStars >= minStarsNeeded)
        {
            if (numStars >= twoStarScore)
            {
                LevelSelectController.curStarsEarned = 2;
                
            }
            else
            {
                hasWon = true;
                LevelSelectController.curStarsEarned = 1;
            }
        }
    }

    // Gotten from http://answers.unity3d.com/questions/209472/detecting-when-all-rigidbodies-have-stopped-moving.html
    // Slightly modified
    IEnumerator CheckAsleep()
    {
        float timeElapsed = 0f;

        rigidBodies = FindObjectsOfType<Rigidbody2D>().ToList();
        bool allAsleep = false;
        while (!allAsleep && timeElapsed < 1f) 
        {
            allAsleep = true;
            for (int i = 0; i < rigidBodies.Count; i++)
            {
                if (!rigidBodies[i].IsSleeping())
                {
                    allAsleep = false;
                    timeElapsed = 0f;
                    Debug.Log("Time elapsed reset");
                    yield return null;
                    break;
                }
                timeElapsed += Time.fixedDeltaTime;
                Debug.Log("Time Elapsed: " + timeElapsed);

            }
        }
        followScript.objectToFollow = transform;
    }
}
