using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    public LineRenderer line_0;
    public GameObject corner_1;
    public LineRenderer line_1;
    public GameObject corner_2;
    public LineRenderer line_2;
    public LineRenderer line_3;
    public TechTreeNode start;
    public TechTreeNode end;
}

public enum ArrowType
{
    AND,
    OR,
}