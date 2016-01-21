using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TechTree : MonoBehaviour {

    public float length;
    static public TechTreeNode current;
    public GameObject treePanel;
    public void LightNode()
    {
        if (!current || !current.Check()) 
        {
            return;
        }
        current.Light();
    }

    public void onValueChanged(Scrollbar bar)
    {
        treePanel.transform.localPosition = new Vector3(bar.value * -7.5F + 1.3F / length, treePanel.transform.localPosition.y, treePanel.transform.localPosition.z);
    }

    int count = 0;
    public Scrollbar bar;
    float step = 0.2F;
    void Update()
    {
        if (current == null)
        {
            current = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TechTreeNode>();
            current.GetComponent<Button>().Select();
        }
        if (Input.GetKeyDown(KeyCode.A) && bar.value >= 0) 
        {
            bar.value -= step;
        }
        else if (Input.GetKeyDown(KeyCode.D) && bar.value <= 1)
        {
            bar.value += step;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LightNode();
        }
    }
}
