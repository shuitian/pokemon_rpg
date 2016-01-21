using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(TechTreeNode))]
public class TechTreeNodeEditor : Editor
{
    static TechTreeNode start;
    static TechTreeNode end;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TechTreeNode node = (TechTreeNode)target;

        if (GUILayout.Button("设为起点"))
        {
            start = node;
        }
        if (GUILayout.Button("设为终点"))
        {
            end = node;
            if (start && start != end)
            {
                Vector3 startPoint = start.transform.position + new Vector3(1.5F, 0, -1);
                Vector3 endPoint = end.transform.position + new Vector3(-1.5F, 0, -1);
                if (endPoint.x > startPoint.x)
                {
                    end.AddParent(start, startPoint, endPoint);
                }
            }
        }
    }
}
