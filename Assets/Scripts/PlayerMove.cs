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
        if (Game.Instance().battle.gameObject.activeInHierarchy)
        {
            lastTime = Time.time;
            return;
        }
        if(Time.time - lastTime < moveSpcae)
        {
            return;
        }
        lastTime = Time.time;
        float t_x = 0;
        float t_y = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            t_x = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            t_x = -1;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            t_y = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            t_y = -1;
        }
        int direct = (t_x == 0) ? ((t_y == 0) ? -1 : ((t_y > 0) ? 1 : 2)) : ((t_x > 0) ? 0 : 3);
        if (direct != -1)
        {
            int x = (int)transform.position.x + pos_x[direct];
            int y = (int)transform.position.y + pos_y[direct];
            if (x >= Level.width || x < 0 || y >= Level.height || y < 0)
            {
                return;
            }
            if (Level.Instance().cells[x, y].IsRoad())
            {
                transform.position = new Vector3(x, y, -1);
            }
            else if (Level.Instance().cells[x, y].IsUpFloor())
            {
                transform.position = new Vector3(x, y, -1);
                if (Game.Instance().SetLevel(Game.currentLevel + 1))
                {
                    transform.position = new Vector3(0, 0, -1);
                }
            }
            else if (Level.Instance().cells[x, y].IsDownFloor())
            {
                transform.position = new Vector3(x, y, -1);
                if (Game.Instance().SetLevel(Game.currentLevel - 1))
                {
                    transform.position = new Vector3(Level.width - 1, Level.height - 1, -1);
                }
            }
            else if (Level.Instance().cells[x, y].IsMonster())
            {
                Game.Instance().battle.gameObject.SetActive(true);
                Game.Instance().battle.battle(Player.Instance(), Level.Instance().cells[x, y].monster);
            }
            else if (Level.Instance().cells[x, y].IsItem())
            {
                Player.Instance().GetItem(Level.Instance().cells[x, y].item);
                Level.Instance().cells[x, y].GetComponent<SpriteRenderer>().sprite = null;
                Level.Instance().cells[x, y].id = (int)CellType.ROAD;
            }
        }
    }

   
}