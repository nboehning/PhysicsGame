using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour
{
    GameObject cannonBall;
    public float powerX = 30.0f;
    public float powerY = 30.0f;
    public float rotateSpeed = 5f;

    public float maxRotation, minRotation;

	// Use this for initialization
	void Start ()
    {
        maxRotation = 10 - rotateSpeed;
        minRotation = 360 + rotateSpeed;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            // Increase the power
            if (powerX <= 39)
                powerX++;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            // Decrease the power
            if (powerX >= 21)
                powerX--;
        }
        if(Input.GetKey(KeyCode.W))
        {
            // Rotate the cannon up
            transform.Rotate(Vector3.forward, rotateSpeed);

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Clamp(transform.eulerAngles.z, -30f, 30f));
        }
        else if(Input.GetKey(KeyCode.S))
        {
            // Rotate the cannon down
            transform.Rotate(Vector3.back, rotateSpeed);

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Clamp(transform.eulerAngles.z, -30f, 30f));
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Cannonballs();
        }
	}

    void Cannonballs()
    {
        GameObject cannonballInstance;
        cannonballInstance = Instantiate(Resources.Load("CannonBall"), new Vector3(transform.position.x + 2, transform.position.y + 1, transform.position.z), Quaternion.identity) as GameObject;
        cannonballInstance.GetComponent<Rigidbody2D>().MoveRotation(54);
        cannonballInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(powerX, powerY);
    }
}
