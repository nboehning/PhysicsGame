using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Floor")
        {
            GameObject.Find("Cannon").GetComponent<Cannon>().DestroyStar();
            Destroy(gameObject);
        }
    }
}
