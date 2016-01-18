using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public static int maxLevel = 3;
    public static Level[] levels = new Level[maxLevel];
    public static int currentLevel;
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
        if (maxLevel > 0)
        {
            currentLevel = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
	}
	
    public static bool SetLevel(int level)
    {
        if (level >= 0 && level < maxLevel)
        {
            levels[level].gameObject.SetActive(true);
            levels[currentLevel].gameObject.SetActive(false);
            currentLevel = level;
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
