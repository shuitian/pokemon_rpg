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
        if (Game.Instance().battle.gameObject.activeInHierarchy || Game.Instance().restartGameObject.activeInHierarchy)
        {
            return;
        }
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
            int x = (int)transform.position.x + pos_x[direct];
            int y = (int)transform.position.y + pos_y[direct];
            if (x >= Level.width || x < 0 || y >= Level.height || y < 0)
            {
                return;
            }
            if (Level.Instance().cells[x, y].monster.id != (int)CellType.WALL)
            {
                transform.position = new Vector3(x, y, -1);
                if (Level.Instance().cells[x, y].monster.id == (int)CellType.up_floor) 
                {
                    if(Game.Instance().SetLevel(Game.currentLevel + 1))
                    {
                        transform.position = new Vector3(0, 0, -1);
                    }
                }
                else if (Level.Instance().cells[x, y].monster.id == (int)CellType.down_floor)
                {
                    if(Game.Instance().SetLevel(Game.currentLevel - 1))
                    {
                        transform.position = new Vector3(Level.width - 1, Level.height - 1, -1);
                    }
                }
                else if(Level.Instance().cells[x, y].monster.id >= 1 && Level.Instance().cells[x, y].monster.id <= LoadResources.maxMonster)
                {
                    Game.Instance().battle.gameObject.SetActive(true);
                    Game.Instance().battle.battle(Player.Instance(), Level.Instance().cells[x, y].monster);
                }
                else if (Level.Instance().cells[x, y].monster.id == (int)CellType.add_attack_10)
                {
                    Player.Instance().attack += 10;
                    Level.Instance().cells[x, y].monster.id = 0;
                    Level.Instance().cells[x, y].GetComponent<SpriteRenderer>().sprite = null;
                }
                else if (Level.Instance().cells[x, y].monster.id == (int)CellType.add_defence_1)
                {
                    Player.Instance().defence += 1;
                    Level.Instance().cells[x, y].monster.id = 0;
                    Level.Instance().cells[x, y].GetComponent<SpriteRenderer>().sprite = null;
                }
                else if (Level.Instance().cells[x, y].monster.id == (int)CellType.add_hp_100)
                {
                    Player.Instance().AddHp(100);
                    Level.Instance().cells[x, y].monster.id = 0;
                    Level.Instance().cells[x, y].GetComponent<SpriteRenderer>().sprite = null;
                }
                else if (Level.Instance().cells[x, y].monster.id == (int)CellType.add_hp_1000)
                {
                    Player.Instance().AddHp(1000);
                    Level.Instance().cells[x, y].monster.id = 0;
                    Level.Instance().cells[x, y].GetComponent<SpriteRenderer>().sprite = null;
                }
                else if (Level.Instance().cells[x, y].monster.id == (int)CellType.add_hp_10000)
                {
                    Player.Instance().AddHp(10000);
                    Level.Instance().cells[x, y].monster.id = 0;
                    Level.Instance().cells[x, y].GetComponent<SpriteRenderer>().sprite = null;
                }
            }
        }
    }
}