using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TechTree : MonoBehaviour {

    static public TechTreeNode current;
    public GameObject treePanel;
    public Text textInfo;
    static TechTree _techTree;
    void Awake()
    {
        _techTree = this;
    }

    public static TechTree Instance()
    {
        return _techTree;
    }

    public void LightNode()
    {
        if (!current || !current.Check()) 
        {
            return;
        }
        current.Light();
    }

    public void onValueChangedHorizontal()
    {
        treePanel.transform.localPosition = new Vector3(barHorizontal.value * -21F + 1.3F / 20, treePanel.transform.localPosition.y, treePanel.transform.localPosition.z);
    }

    public void onValueChangedVertical()
    {
        treePanel.transform.localPosition = new Vector3(treePanel.transform.localPosition.x, barVertical.value * 5.9F + -0.1F, treePanel.transform.localPosition.z);
    }

    int count = 0;
    public Scrollbar barHorizontal;
    public Scrollbar barVertical;
    public float stepHorizontal = 0.125F;
    public float stepVertical = 0.125F;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Game.Instance().techTree.SetActive(false);
        }
        if (current == null)
        {
            current = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TechTreeNode>();
            current.GetComponent<Button>().Select();
        }
        if (Input.GetKeyDown(KeyCode.A) && barHorizontal.value >= 0) 
        {
            barHorizontal.value -= stepHorizontal;
        }
        else if (Input.GetKeyDown(KeyCode.D) && barHorizontal.value <= 1)
        {
            barHorizontal.value += stepHorizontal;
        }
        if (Input.GetKeyDown(KeyCode.W) && barVertical.value >= 0)
        {
            barVertical.value -= stepVertical;
        }
        else if (Input.GetKeyDown(KeyCode.S) && barVertical.value <= 1)
        {
            barVertical.value += stepVertical;
        }
        else
        {
            float v = Input.GetAxis("Mouse ScrollWheel");
            if (v > 0)
            {
                barVertical.value -= stepVertical;
            }
            else if (v < 0)
            {
                barVertical.value += stepVertical;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LightNode();
        }
    }
}
