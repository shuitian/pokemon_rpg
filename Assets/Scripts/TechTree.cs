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
}
