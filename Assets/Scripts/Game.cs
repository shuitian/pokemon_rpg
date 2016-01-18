using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public static int maxLevel = 3;
    public static Level[] levels = new Level[maxLevel];
    public static int currentLevel;
    // Use this for initialization
    void Start () {
        if (transform.childCount == maxLevel)
        {
            for (int i = 0; i < transform.childCount; i++) 
            {
                levels[i] = transform.GetChild(i).GetComponent<Level>();
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
        return false;
    }
}
