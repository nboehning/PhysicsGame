using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public int numStarsEarned = 0;
        public bool levelComplete;
        public bool canPlay;
        public Button btnPlayLevel;
        public Image starOne;
        public Image starTwo;
        public Image starThree;
    }

    public GameObject camera;
    public Level[] levels;
    public static int maxLevel = 1;
    public static int curStarsEarned = -1;
    public Sprite earnedStarSprite;
    public SpriteRenderer levelImage;
    public Sprite[] levelBackgrounds;
    private List<Button> playButtons = new List<Button>(); 

	// Use this for initialization
	void Start ()
	{
	    if (SceneManager.GetActiveScene().name == "MainMenu")
	    {
            if(curStarsEarned != -1)
            {
                levels[maxLevel - 1].numStarsEarned = curStarsEarned;
                curStarsEarned = -1;
            }
	        for (int i = 0; i < levels.Length; i++)
	        {
	            if (i < maxLevel)
	            {
	                switch (levels[i].numStarsEarned)
	                {
	                    case 1:
	                        levels[i].starOne.sprite = earnedStarSprite;
	                        break;
	                    case 2:
	                        levels[i].starOne.sprite = earnedStarSprite;
	                        levels[i].starTwo.sprite = earnedStarSprite;
	                        break;
	                    case 3:
	                        levels[i].starOne.sprite = earnedStarSprite;
	                        levels[i].starTwo.sprite = earnedStarSprite;
	                        levels[i].starThree.sprite = earnedStarSprite;
	                        break;
	                }
	                playButtons.Add(levels[i].btnPlayLevel);                    
	            }
	            else
	            {
	                levels[i].btnPlayLevel.GetComponentInChildren<Text>().text = "";
	                levels[i].btnPlayLevel.interactable = false;
	            }
	        }
	    }
	}

    public void SetBackground(Button btn)
    {
        for (int i = 0; i < playButtons.Count; i++)
        {
            if (btn == playButtons[i])
            {
                levelImage.sprite = levelBackgrounds[i];
                camera.GetComponent<Animator>().SetTrigger("IsHovering");
            }
        }
    }

    public void RevertBackground()
    {
        camera.GetComponent<Animator>().SetTrigger("IsReleased");
    }
}
