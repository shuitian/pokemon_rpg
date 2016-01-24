using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LoadResources))]
public class Game : MonoBehaviour {

    public Vector3 gamePosition;
    public Vector3 battlePosition;
    public Vector3 emptyPosition;
    static Game game;
    public static int maxLevel = 10;
    public static Level[] levels = new Level[maxLevel];
    public static int currentLevel;
    public GameObject battleObject;
    public Text messageBox;
    public GameObject messageObject;
    public GameObject techTree;
    static public bool sound = true;
    public AudioSource winAudioSource;
    public AudioSource loseAudioSource;
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
                messageObject.transform.position = emptyPosition;
                messageObject.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            if (sound)
            {
                sound = false;
            }else
            {
                sound = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.K) && !battleObject.activeInHierarchy)
        {
            if (techTree && techTree.activeInHierarchy)
            {
                techTree.SetActive(false);
            }
            else if (techTree && !techTree.activeInHierarchy)
            {
                techTree.SetActive(true);
            }
        }
    }

    public void Restart()
    {
		SceneManager.LoadScene (0);
    }

    public Text levelText;
    public bool SetLevel(int level)
    {
        if (level >= 0 && level < maxLevel)
        {
            levels[currentLevel].gameObject.SetActive(false);
            levels[level].gameObject.SetActive(true);
            AudioSource source = Player.Instance().GetComponent<AudioSource>();
            if (levels[level].type == LevelType.grass)
            {
                source.clip = LoadResources.grass_sound;
            }
            else if (levels[level].type == LevelType.road)
            {
                source.clip = LoadResources.road_sound;
            }
            else if (levels[level].type == LevelType.water)
            {
                source.clip = LoadResources.water_sound;
            }
            currentLevel = level;
            if (levelText)
            {
                levelText.text = "第 " + (currentLevel + 1) + " /" + Game.maxLevel + " 层";
            }
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
        ShowMessage("你输了，请按任意键重新开始游戏!", battlePosition);
        if(loseAudioSource && Game.sound)
        {
            loseAudioSource.Play();
        }
    }

    public void Win()
    {
        ShowMessage("恭喜你，你赢了!", gamePosition);
        if (winAudioSource && Game.sound)
        {
            winAudioSource.Play();
        }
    }

    public void ShowMessage(string str, Vector3 positon)
    {
        messageObject.transform.position = positon;
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
