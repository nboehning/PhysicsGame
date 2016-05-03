using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	public Text textBanner;
    public GameObject[] menus;
    public GameObject buttonHolder;
    private int curMenu = 0;
    public AudioClip buttonClick;
    private AudioSource source;
    public GameObject fallingStar;
    private GameObject canvas;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        gameObject.AddComponent<AudioSource>();
        source = GetComponent<AudioSource>();
        source.clip = buttonClick;
        Invoke("SpawnStar", 0f);
    }

    public void _BtnHome()
    {
        source.Play();
        menus[curMenu].SetActive(false);
        curMenu = 0;
        menus[curMenu].SetActive(true);
        textBanner.text = "MAIN MENU";
        buttonHolder.SetActive(false);
    }

    public void _BtnSelectLevel()
    {
        source.Play();
        menus[curMenu].SetActive(false);
        curMenu = 1;
        menus[curMenu].SetActive(true);
        textBanner.text = "SELECT LEVEL";
        buttonHolder.SetActive(true);
    }

    public void _BtnCredits()
    {
        source.Play();
        menus[curMenu].SetActive(false);
        curMenu = 2;
        menus[curMenu].SetActive(true);
        textBanner.text = "CREDITS";
        buttonHolder.SetActive(true);
    }

    public void _BtnExitGame()
    {
        source.Play();
        Application.Quit();
    }

    void SpawnStar()
    {
        GameObject starInstance = Instantiate(fallingStar, new Vector2(Random.Range(-6f, 6.5f), 5.6f), Quaternion.identity) as GameObject;
        starInstance.transform.SetParent(canvas.transform);
        starInstance.transform.SetAsFirstSibling();
        starInstance.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        Invoke("SpawnStar", Random.Range(0.5f, 1f));
    }
}
