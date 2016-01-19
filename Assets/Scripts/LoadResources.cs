using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadResources : MonoBehaviour {

    public static Dictionary<CellType, Sprite> resource = new Dictionary<CellType, Sprite>();

    public static Sprite blue = Resources.Load<Sprite>("Texture/blue");
    public static Sprite road = Resources.Load<Sprite>("Texture/road");
    public static Sprite wall = Resources.Load<Sprite>("Texture/wall");
    public static Sprite[] monsters = Resources.LoadAll<Sprite>("Texture/pics");
    public static GameObject cell = Resources.Load("Prefabs/Cell") as GameObject;
    public static Sprite up_floor = Resources.Load<Sprite>("Texture/up_floor");
    public static Sprite down_floor = Resources.Load<Sprite>("Texture/down_floor");
    public static int maxMonster
    {
        get { return monsters.Length; }
    }

    public static Sprite add_attack_10 = Resources.Load<Sprite>("Texture/add_attack_10");
    public static Sprite add_defence_1 = Resources.Load<Sprite>("Texture/add_defence_1");
    public static Sprite add_hp_100 = Resources.Load<Sprite>("Texture/add_hp_100");
    public static Sprite add_hp_1000 = Resources.Load<Sprite>("Texture/add_hp_1000");
    public static Sprite add_hp_10000 = Resources.Load<Sprite>("Texture/add_hp_10000");
    void Awake()
    {
        blue = Resources.Load<Sprite>("Texture/blue");
        road = Resources.Load<Sprite>("Texture/road");
        wall = Resources.Load<Sprite>("Texture/wall");
        monsters = Resources.LoadAll<Sprite>("Texture/pics");
        cell = Resources.Load("Prefabs/Cell") as GameObject;
        up_floor = Resources.Load<Sprite>("Texture/up_floor");
        down_floor = Resources.Load<Sprite>("Texture/down_floor");
        add_attack_10 = Resources.Load<Sprite>("Texture/add_attack_10");
        add_defence_1 = Resources.Load<Sprite>("Texture/add_defence_1");
        add_hp_100 = Resources.Load<Sprite>("Texture/add_hp_100");
        add_hp_1000 = Resources.Load<Sprite>("Texture/add_hp_1000");
        add_hp_10000 = Resources.Load<Sprite>("Texture/add_hp_10000");
    }
}
