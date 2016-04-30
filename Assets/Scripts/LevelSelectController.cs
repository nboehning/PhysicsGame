using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    
    public Level[] levels;
    public static int maxLevel = 1;
    public static int selectedLevel;
    public Sprite earnedStarSprite;
    public Image backgroundImage;
    public Sprite[] levelBackgrounds;
    private Sprite originalBackground;
    private List<Button> playButtons = new List<Button>(); 

	// Use this for initialization
	void Start ()
	{
	    originalBackground = backgroundImage.sprite;
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

    public void SetBackground(Button btn)
    {
        for (int i = 0; i < playButtons.Count; i++)
        {
            if (btn == playButtons[i])
            {
                backgroundImage.sprite = levelBackgrounds[i];
            }
        }
    }

    public void RevertBackground()
    {
        backgroundImage.sprite = originalBackground;
    }
}
