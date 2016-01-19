using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Monster))]
public class MonsterEditor : Editor
{

    public override void OnInspectorGUI()
    {
        Monster monster = (Monster)target;
        int t = monster.id;
        monster.id = EditorGUILayout.IntField("id", monster.id);
        if (monster.id >= 1 && monster.id <= LoadResources.maxMonster)
        {
            MonsterData m = Sql.GetMonsterData(monster.id);
            monster.hp = m.hp;
            monster.attack = m.attack;
            monster.defence = m.defence;
            monster.gold = m.gold;
        }
        EditorGUILayout.LabelField("hp", monster.hp.ToString());
        EditorGUILayout.LabelField("attack", monster.attack.ToString());
        EditorGUILayout.LabelField("defence", monster.defence.ToString());
        EditorGUILayout.LabelField("gold", monster.gold.ToString());

        if (t == monster.id)
        {
            return;
        }
        if (monster.id == (int)CellType.ROAD && t != (int)CellType.ROAD)
        {
            monster.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
        }
        else if (monster.id != (int)CellType.ROAD && t == (int)CellType.ROAD)
        {
            monster.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        }
        if (monster.id >= 1 && monster.id <= LoadResources.maxMonster)
        {
            monster.GetComponent<SpriteRenderer>().sprite = LoadResources.monsters[monster.id - 1];
        }
        else if (monster.id == (int)CellType.WALL)
        {
            monster.GetComponent<SpriteRenderer>().sprite = LoadResources.wall;
        }
        else if (monster.id == (int)CellType.up_floor)
        {
            monster.GetComponent<SpriteRenderer>().sprite = LoadResources.up_floor;
        }
        else if (monster.id == (int)CellType.down_floor)
        {
            monster.GetComponent<SpriteRenderer>().sprite = LoadResources.down_floor;
        }
        else if (monster.id == (int)CellType.add_attack_10)
        {
            monster.GetComponent<SpriteRenderer>().sprite = LoadResources.add_attack_10;
        }
        else if (monster.id == (int)CellType.add_defence_1)
        {
            monster.GetComponent<SpriteRenderer>().sprite = LoadResources.add_defence_1;
        }
        else if (monster.id == (int)CellType.add_hp_100)
        {
            monster.GetComponent<SpriteRenderer>().sprite = LoadResources.add_hp_100;
        }
        else if (monster.id == (int)CellType.add_hp_1000)
        {
            monster.GetComponent<SpriteRenderer>().sprite = LoadResources.add_hp_1000;
        }
        else if (monster.id == (int)CellType.add_hp_10000)
        {
            monster.GetComponent<SpriteRenderer>().sprite = LoadResources.add_hp_10000;
        }
    }
}
