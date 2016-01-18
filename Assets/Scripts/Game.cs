using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

    static Game game;
    public static int maxLevel = 3;
    public static Level[] levels = new Level[maxLevel];
    public static int currentLevel;
    public Animator animator;
    public Text levelNumber;
    static public Game Instance()
    {
        return game;
    }
    void Awake()
    {
        game = this;
    }
    // Use this for initialization
    void Start () {
        GameObject terrain = GameObject.FindGameObjectWithTag("Terrain");
        if (terrain.transform.childCount == maxLevel)
        {
            for (int i = 0; i < terrain.transform.childCount; i++) 
            {
                levels[i] = terrain.transform.GetChild(i).GetComponent<Level>();
            }
        }
        currentLevel = 0;
        SetLevel(0);
    }
	
	// Update is called once per frame
	void Update () {
        if (animator && !animator.IsInTransition(0)&& animator.GetBool("Level") && animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.ChangeLevel")) 
        {
            animator.SetBool("Level", false);
        }
    }
	
    public bool SetLevel(int level)
    {
        if (level >= 0 && level < maxLevel)
        {
            levels[currentLevel].gameObject.SetActive(false);
            levels[level].gameObject.SetActive(true);
            currentLevel = level;
            levelNumber.text = "第" + (level + 1) + "层";
            if (animator)
            {
                animator.SetBool("Level", true); 
            }
            return true;
        }
        if (level >= maxLevel)
        {
            Win();
        }
        return false;
    }

    static public void Lose()
    {
        print("You Fail!");
    }

    static public void Win()
    {
        print("You Win!");
    }
}
