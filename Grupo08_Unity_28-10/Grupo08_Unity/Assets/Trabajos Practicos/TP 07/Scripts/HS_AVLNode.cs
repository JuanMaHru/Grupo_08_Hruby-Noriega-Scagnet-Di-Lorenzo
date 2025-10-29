using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AVLNode.cs
public class HS_AVLNode
{
    public Score data;
    public HS_AVLNode left;
    public HS_AVLNode right;
    public int height;

    public HS_AVLNode(Score data)
    {
        this.data = data;
        height = 1;
    }
}
