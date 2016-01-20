using UnityEngine;
using System.Collections;

public class TechTree : MonoBehaviour {

    static public TechTreeNode current;

    public void LightNode()
    {
        if (!current || !current.Check()) 
        {
            return;
        }
        current.Light();
    }
}
