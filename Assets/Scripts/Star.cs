using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Star : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Floor")
        {
            GameObject.Find("Cannon").GetComponent<Cannon>().DestroyStar(gameObject);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
            Invoke("DestroyStar", Random.Range(1.5f, 5f));
    }

    void DestroyStar()
    {
        Destroy(gameObject);
    }
}
