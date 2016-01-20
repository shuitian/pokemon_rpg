using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(LoadResources))]
public class Game : MonoBehaviour {

    static Game game;
    public static int maxLevel = 10;
    public static Level[] levels = new Level[maxLevel];
    public static int currentLevel;
    public GameObject animaterObject;
    public Battle battle;
    public Text messageBox;
    public GameObject messageObject;
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
        if (messageObject.activeInHierarchy)
        {
            if (click && Input.anyKeyDown)
            {
                messageObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    public void Restart()
    {
        Application.LoadLevel(0);
    }

    public bool SetLevel(int level)
    {
        if (level >= 0 && level < maxLevel)
        {
            levels[currentLevel].gameObject.SetActive(false);
            levels[level].gameObject.SetActive(true);
            currentLevel = level;
            return true;
        }
        if (level >= maxLevel)
        {
            Win();
        }
        return false;
    }

    public void Lose()
    {
        ShowMessage("你输了，请按任意键重新开始游戏!");
    }

    public void Win()
    {
        ShowMessage("恭喜你，你赢了!");
    }

    public void ShowMessage(string str)
    {
        messageObject.SetActive(true);
        click = false;
        if (messageBox)
        {
            StartCoroutine(_ShowMessage(str));
        }
    }

    bool click = false;
    IEnumerator _ShowMessage(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            messageBox.text = str.Substring(0, i); ;
            yield return 0;
        }
        click = true;
    }
}
