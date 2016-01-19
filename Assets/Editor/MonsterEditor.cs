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
        if (monster.id >= 1 && monster.id <= Level.maxData)
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
        if (monster.id == (int)CellType.WALL)
        {
            monster.GetComponent<SpriteRenderer>().sprite = Level.walls[Random.Range(0, Level.walls.Length)];
        }
        else if (monster.id == (int)CellType.UPSTAIRS)
        {
            monster.GetComponent<SpriteRenderer>().sprite = Level.up_floor;
        }
        else if (monster.id == (int)CellType.DOWNSTAIRS)
        {
            monster.GetComponent<SpriteRenderer>().sprite = Level.down_floor;
        }
        else if (monster.id >= 1 && monster.id <= Level.maxData)
        {
            monster.GetComponent<SpriteRenderer>().sprite = Level.pics[monster.id - 1];
        }
    }
}
