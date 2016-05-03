using UnityEngine;
using System.Collections;

// Author: Tiffany Fischer
// Modified by: Nathan Boehning
public class CameraFollow : MonoBehaviour
{
    // Distance in the x axis the player can move before being followed by camera
    public float xMargin = 1.5f;

    // Distance in the y axis the player can move before being followed by camera
    public float yMargin = 1.5f;

    // How smooth the camera catches its target (x axis)
    public float xSmooth = 1.5f;

    // How smooth camera catches its target (y axis)
    public float ySmooth = 1.5f;

    // The maximum x and y coordinates the camera can have
    private Vector2 maxXandY;

    // The minimum x and y coordinates the camera can have
    private Vector2 minXandY;

    // Reference the players transform
    public Transform objectToFollow;

	// Use this for initialization
	void Awake ()
    {
        // Resolution independence solution A

        // Getting the bounds for background in WORLD size
        var backgroundBounds = GameObject.Find("Background").GetComponent<SpriteRenderer>().bounds;

        // Get viewable bounds of camera in WORLD size
        var cameraTopLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        var cameraBottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        // Set the min and max X for the camera
        minXandY.x = backgroundBounds.min.x - cameraTopLeft.x;
        maxXandY.x = backgroundBounds.max.x - cameraBottomRight.x;

        // Set the min and max Y for the camera
        minXandY.y = backgroundBounds.min.y - cameraTopLeft.y;
        maxXandY.y = backgroundBounds.max.y - cameraBottomRight.y;

        // End of resolution solution
       
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
	    if(objectToFollow != null)
        {
            // Check if the object is close to the edge and update the cameras position
            // Also check if the camera bounds have reached the edge of the screen and don't move beyond it

            float targetX = transform.position.x;
            float targetY = transform.position.y;

            if(CheckXMargin())
                // Lerp between the current position and the object position, using xSmooth
                targetX = Mathf.Lerp(transform.position.x, objectToFollow.position.x, xSmooth * Time.fixedDeltaTime);

            if (CheckYMargin())
                // Lerp between the current position and the object position, using xSmooth
                targetY = Mathf.Lerp(transform.position.y, objectToFollow.position.y, ySmooth * Time.fixedDeltaTime);

            targetX = Mathf.Clamp(targetX, minXandY.x, maxXandY.x);
            targetY = Mathf.Clamp(targetY, minXandY.y, maxXandY.y);

            transform.position = new Vector3(targetX, targetY, transform.position.z);
        }
	}

    /// <summary>
    /// Check if the object has moved near the edge of the cameras bounds.
    /// </summary>
    /// <returns>If the object has moved near the X edge of the cameras bounds.</returns>
    bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - objectToFollow.position.x) > xMargin;
    }

    /// <summary>
    /// Check if the object has moved near the edge of the cameras bounds.
    /// </summary>
    /// <returns>If the object has moved near the Y edge of the cameras bounds.</returns>
    bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - objectToFollow.position.y) > yMargin;
    }
}
