using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityTool.Libgame;
using System.Collections.Generic;

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
    
    public string info
    {
        get
        {
            if (_info == null)
            {
                _info = "耗费" + gold + "金币，学习" + nodeName;
                if (hp > 0)
                {
                    _info += "，增加生命值" + hp;
                }
                if (attack > 0)
                {
                    _info += "，增加攻击力" + attack;
                }
                if (defence > 0)
                {
                    _info += "，增加防御力" + defence;
                }
                _info += "。";
            }
            return _info;
        }
    }
    string _info;
    public ArrowType arrowType;
    public bool isLighting;
    List<TechTreeNode> parents = new List<TechTreeNode>();
    List<TechTreeNode> children = new List<TechTreeNode>();
    public int gold;
    public int hp;
    public int attack;
    public int defence;
    public Skill skill;
    public List<TechTreeNode> GetParents()
    {
        return parents;
    }

    public List<TechTreeNode> GetChildren()
    {
        return children;
    }

    void Awake()
    {
        int n = transform.childCount;
        for(int i = 1; i < n; i++)
        {
            Arrow arrow = transform.GetChild(i).GetChild(0).GetComponent<Arrow>();
            arrow.end.parents.Add(arrow.start);
            arrow.start.children.Add(arrow.end);
        }
    }
    
    public void AddParent(TechTreeNode parent, Vector3 startPoint, Vector3 endPoint)
    {
        //parents.Add(parent);
        if (!LoadResources.arrow)
        {
            return;
        }
        float x = endPoint.x - startPoint.x;
        Arrow arrow;
        if (startPoint.y == endPoint.y)
        {
            GameObject arrowObject = ObjectPool.Instantiate(LoadResources.arrow, startPoint, Quaternion.identity, parent.transform);
            arrow = arrowObject.GetComponentInChildren<Arrow>();
            arrow.line_0.SetPosition(0, new Vector3(-0.015F, 0, -0.1F));
            arrow.line_0.SetPosition(1, new Vector3(x * 0.85F, 0, -0.1F));
            arrow.line_0.SetWidth(0.1F, 0.1F);

            arrow.line_1.SetPosition(0, new Vector3(x * 0.85F, 0, -0.1F));
            arrow.line_1.SetPosition(1, new Vector3(x, 0, -0.1F));
            arrow.line_1.SetWidth(x * 0.15F, 0);

            arrow.corner_1.SetActive(false);
            arrow.corner_2.SetActive(false);
            arrow.line_2.gameObject.SetActive(false);
            arrow.line_3.gameObject.SetActive(false);
        }
        else
        {
            float y = endPoint.y - startPoint.y;
            GameObject arrowObject = ObjectPool.Instantiate(LoadResources.arrow, startPoint, Quaternion.identity, parent.transform);
            arrow = arrowObject.GetComponentInChildren<Arrow>();
            arrow.line_0.SetPosition(0, new Vector3(-0.015F, 0, -0.1F));
            arrow.line_0.SetPosition(1, new Vector3(x * 0.3F, 0, -0.1F));
            arrow.line_0.SetWidth(0.1F, 0.1F);

            arrow.line_1.SetPosition(0, new Vector3(x * 0.3F, 0, -0.1F));
            arrow.line_1.SetPosition(1, new Vector3(x * 0.7F, y, -0.1F));
            arrow.line_1.SetWidth(0.1F, 0.1F);

            arrow.line_2.SetPosition(0, new Vector3(x * 0.7F, y, -0.1F));
            arrow.line_2.SetPosition(1, new Vector3(x * 0.85F, y, -0.1F));
            arrow.line_2.SetWidth(0.1F, 0.1F);

            arrow.line_3.SetPosition(0, new Vector3(x * 0.85F, y, -0.1F));
            arrow.line_3.SetPosition(1, new Vector3(x, y, -0.1F));
            arrow.line_3.SetWidth(x * 0.15F, 0);

            arrow.corner_1.transform.localPosition = new Vector3(x * 0.3F + 0.005F, y > 0 ? -0.005F : 0.005F, 0);
            arrow.corner_2.transform.localPosition = new Vector3(x * 0.7F - 0.005F, y - (y > 0 ? -0.005F : 0.005F), 0);
        }
        arrow.start = parent;
        arrow.end = this;
    }

    public void Light()
    {
        isLighting = true;
        Player.Instance().LoseGold(gold);
        Player.Instance().hpComponent.AddHp(hp);
        Player.Instance().AddAttack(attack);
        Player.Instance().AddDefence(defence);
        Message.RaiseOneMessage<Player>("PlayerShow", this, Player.Instance());
        TechTree.Instance().textInfo.text = "项目学习成功";
        ColorBlock colorblock = ColorBlock.defaultColorBlock;
        Color color = new Color(246 / 255.0F, 116 / 255.0F, 116 / 255.0F);
        colorblock.normalColor = color;
        colorblock.highlightedColor = color;
        colorblock.pressedColor = color;
        GetComponent<Button>().colors = colorblock;
    }

    public bool Check()
    {
        if (_Check())
        {
            if (Game.sound && Game.Instance().winAudioSource)
            {
                Game.Instance().winAudioSource.Play();
            }
            return true;
        }
        if (Game.sound && Game.Instance().loseAudioSource)
        {
            Game.Instance().loseAudioSource.Play();
        }
        return false;
    }
    bool _Check()
    {
        if (isLighting)
        {
            TechTree.Instance().textInfo.text = "对不起，已经学习项目" + nodeName;
            return false;
        }
        if (Player.Instance().GetCurrentGold() < gold)
        {
            TechTree.Instance().textInfo.text = "对不起，金币不足，无法学习项目" + nodeName + "，该项目需要金币" + gold + "，你拥有" + Player.Instance().GetCurrentGold();
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
                    TechTree.Instance().textInfo.text = "对不起，项目\"" + node.nodeName + "\"未学习，无法学习该项目";
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
                    TechTree.Instance().textInfo.text = "对不起，项目\"" + node.nodeName + "\"未学习，无法学习该项目";
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
        TechTree.Instance().textInfo.text = info;
        TechTree.current = this;
    }
}