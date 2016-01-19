using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

    static Game game;
    public static int maxLevel = 11;
    public static Level[] levels = new Level[maxLevel];
    public static int currentLevel;
    public GameObject animaterObject;
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
        int a = terrain.transform.childCount;
        a = a > maxLevel ? maxLevel : a;
        for (int i = 0; i < a; i++)
        {
            levels[i] = terrain.transform.GetChild(i).GetChild(0).GetComponent<Level>();
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
                animaterObject.SetActive(true);
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
