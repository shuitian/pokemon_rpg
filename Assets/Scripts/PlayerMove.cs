using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    static public int[] pos_x = { 1, 0, 0, -1 };
    static public int[] pos_y = { 0, 1, -1, 0 };
    public float moveSpcae = 0.5F;
    float lastTime = 0;
    // Use this for initialization
    void Start () {
        transform.position = new Vector3(0, 0, -1);
    }

    // Update is called once per frame
    void Update () {
        if(Time.time - lastTime < moveSpcae)
        {
            return;
        }
        lastTime = Time.time;
        int direct = -1;    
	    if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direct = 0;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            direct = 1;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            direct = 2;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direct = 3;
        }
        if (direct != -1)
        {
            if ((int)transform.position.x + pos_x[direct] >= Level.width || (int)transform.position.x + pos_x[direct] < 0 || (int)transform.position.y + pos_y[direct] >= Level.height || (int)transform.position.y + pos_y[direct] < 0)
            {
                return;
            }
            if (Level.Instance().terrainData[(int)transform.position.x + pos_x[direct], (int)transform.position.y + pos_y[direct]] != (int)CellType.WALL)
            {
                transform.position = new Vector3((int)transform.position.x + pos_x[direct], (int)transform.position.y + pos_y[direct], -1);
                if (Level.Instance().terrainData[(int)transform.position.x, (int)transform.position.y] == (int)CellType.UPSTAIRS)
                {
                    if(Game.SetLevel(Game.currentLevel + 1))
                    {
                        transform.position = new Vector3(0, 0, -1);
                    }
                }
                else if (Level.Instance().terrainData[(int)transform.position.x, (int)transform.position.y] == (int)CellType.DOWNSTAIRS)
                {
                    if(Game.SetLevel(Game.currentLevel - 1))
                    {
                        transform.position = new Vector3(Level.width - 1, Level.height - 1, -1);
                    }
                }
            }
        }
    }
}