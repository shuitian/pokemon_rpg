using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    static public int[] pos_x = { 1, 0, 0, -1 };
    static public int[] pos_y = { 0, 1, -1, 0 };
    public float moveSpcae = 0.5F;
    float lastTime = 0;
    public AudioSource sound;
    // Use this for initialization
    void Start () {
        transform.position = new Vector3(0, 0, -1);
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (Game.Instance().battleObject.activeInHierarchy || Game.Instance().techTree.activeInHierarchy || Game.Instance().messageObject.activeInHierarchy)
        {
            lastTime = Time.time + 5 * moveSpcae;
            return;
        }
        if (Time.time - lastTime < moveSpcae)
        {
            return;
        }
        lastTime = Time.time;
        float t_x = 0;
        float t_y = 0;
        if (Input.GetKey(KeyCode.D))
        {
            t_x = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            t_x = -1;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            t_y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
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
            Cell cell = Level.Instance().cells[x, y];
            if (cell.IsRoad())
            {
                transform.position = new Vector3(x, y, -1);
                if (sound && Game.sound)
                {
                    sound.Play();
                }
            }
            else if (cell.IsUpFloor())
            {
                transform.position = new Vector3(x, y, -1);
                if (Game.Instance().SetLevel(Game.currentLevel + 1))
                {
                    transform.position = new Vector3(0, 0, -1);
                }
            }
            else if (cell.IsDownFloor())
            {
                transform.position = new Vector3(x, y, -1);
                if (Game.Instance().SetLevel(Game.currentLevel - 1))
                {
                    transform.position = new Vector3(Level.width - 1, Level.height - 1, -1);
                }
            }
            else if (cell.IsMonster())
            {
                if (!cell.gameObject.GetComponent<MonsterHpComponent>())
                {
                    cell.gameObject.AddComponent<MonsterHpComponent>();
                }
                MonsterData m = MonsterData.GetMonsterDataFromNetwork(cell.id);
                if (m != null)
                {
                    cell.monster.id = cell.id;
                    cell.monster.hpComponent.SetHp(m.hp);
                    cell.monster.characterName = m.name;
                    cell.monster.SetAttack(m.attack);
                    cell.monster.SetDefence(m.defence);
                    cell.monster.gold = m.gold;
                    Battle.Instance().battle(Player.Instance(), cell.monster);
                }
            }
            else if (cell.IsItem())
            {
                ItemData itemData = ItemData.GetItemDataFromNetwork(cell.item.id + 9);
                if (itemData != null)
                {
                    Player.Instance().GetItem(itemData);
                    cell.GetComponent<SpriteRenderer>().sprite = null;
                    cell.id = (int)CellType.ROAD;
                    if (Game.Instance().winAudioSource && Game.sound)
                    {
                        Game.Instance().winAudioSource.Play();
                    }
                }
            }
        }
    }

   
}