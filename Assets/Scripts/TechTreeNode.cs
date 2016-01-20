using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TechTreeNode : MonoBehaviour {

    public string nodeName {
        get
        {
            if (_nodeName == null)
            {
                _nodeName = GetComponentInChildren<Text>().text;
            }
            return _nodeName;
        }
    }
    string _nodeName;
    public string info;
    public ArrowType arrowType;
    public bool isLighting;
    public TechTreeNode[] parents;
    public int gold;
    public int hp;
    public int attack;
    public int defence;
    public Text infoText;
    public TechTreeNode[] GetParents()
    {
        return parents;
    }

    public void Light()
    {
        isLighting = true;
        Player.Instance().LoseGold(gold);
        Player.Instance().AddHp(hp);
        Player.Instance().attack += attack;
        Player.Instance().defence += defence;
        infoText.text = "项目学习成功";
        ColorBlock colorblock = ColorBlock.defaultColorBlock;
        Color color = new Color(246 / 255.0F, 116 / 255.0F, 116 / 255.0F);
        colorblock.normalColor = color;
        colorblock.highlightedColor = color;
        colorblock.pressedColor = color;
        GetComponent<Button>().colors = colorblock;
    }

    public bool Check()
    {
        if (isLighting)
        {
            infoText.text = "对不起，已经学习该项目";
            return false;
        }
        if (Player.Instance().gold < gold)
        {
            infoText.text = "对不起，金币不足，需要金币" + gold + "，你拥有" + Player.Instance().gold;
            return false;
        }
        if(arrowType == ArrowType.OR)
        {
            foreach(TechTreeNode node in GetParents())
            {
                if (node.isLighting)
                {
                    return true;
                }
                else
                {
                    infoText.text = "对不起，项目\"" + node.nodeName + "\"未学习，无法学习该项目";
                    return false;
                }
            }
            return true;
        }
        else if(arrowType == ArrowType.AND)
        {
            foreach (TechTreeNode node in GetParents())
            {
                if (!node.isLighting)
                {
                    infoText.text = "对不起，项目\"" + node.nodeName + "\"未学习，无法学习该项目";
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShowInfo()
    {
        infoText.text = info;
        TechTree.current = this;
    }
}

public enum ArrowType
{
    AND,
    OR,
}

