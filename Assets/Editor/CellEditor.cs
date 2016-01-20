using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cell))]
public class CellEditor : Editor {

    SerializedProperty m_Monster;
    SerializedProperty m_Item;
    void OnEnable()
    {
        SerializedObject m_Object = new SerializedObject(target);
        m_Monster = m_Object.FindProperty("monster");
        m_Item = m_Object.FindProperty("item");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        Cell cell = (Cell)target;
        int t = cell.id;
        cell.id = EditorGUILayout.IntField("id", cell.id);
        EditorGUILayout.PropertyField(m_Monster);
        EditorGUILayout.PropertyField(m_Item);
        if (t != cell.id)
        {
            if (cell.IsMonster())
            {
                MonsterData m = Sql.GetMonsterData(cell.id);
                Monster monster = cell.GetComponent<Monster>();
                monster.id = cell.id;
                monster.hp = m.hp;
                monster.characterName = m.name;
                monster.attack = m.attack;
                monster.defence = m.defence;
                monster.gold = m.gold;
            }
            else if(cell.IsItem())
            {
                ItemData m = Sql.GetItemData(cell.id + 9);
                Item item = cell.GetComponent<Item>();
                item.id = cell.id;
                item.addHp = m.addHp;
                item.itemName = m.name;
                item.addAttack = m.addAttack;
                item.addDefence = m.addDefence;
                item.addGold = m.addGold;
            }
        }

        if (t == cell.id)
        {
            return;
        }
        if (cell.id == (int)CellType.ROAD && t != (int)CellType.ROAD)
        {
            cell.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1);
        }
        else if (cell.id != (int)CellType.ROAD && t == (int)CellType.ROAD)
        {
            cell.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
        }

        if (cell.IsMonster())
        {
            cell.GetComponent<SpriteRenderer>().sprite = LoadResources.monsters[cell.id - 1];
        }
        else if (cell.IsWall())
        {
            cell.GetComponent<SpriteRenderer>().sprite = LoadResources.wall;
        }
        else if (cell.IsUpFloor())
        {
            cell.GetComponent<SpriteRenderer>().sprite = LoadResources.up_floor;
        }
        else if (cell.IsDownFloor())
        {
            cell.GetComponent<SpriteRenderer>().sprite = LoadResources.down_floor;
        }
        else if (cell.id == (int)CellType.add_attack_10)
        {
            cell.GetComponent<SpriteRenderer>().sprite = LoadResources.add_attack_10;
        }
        else if (cell.id == (int)CellType.add_defence_1)
        {
            cell.GetComponent<SpriteRenderer>().sprite = LoadResources.add_defence_1;
        }
        else if (cell.id == (int)CellType.add_hp_100)
        {
            cell.GetComponent<SpriteRenderer>().sprite = LoadResources.add_hp_100;
        }
        else if (cell.id == (int)CellType.add_hp_1000)
        {
            cell.GetComponent<SpriteRenderer>().sprite = LoadResources.add_hp_1000;
        }
        else if (cell.id == (int)CellType.add_hp_10000)
        {
            cell.GetComponent<SpriteRenderer>().sprite = LoadResources.add_hp_10000;
        }
    }
}
