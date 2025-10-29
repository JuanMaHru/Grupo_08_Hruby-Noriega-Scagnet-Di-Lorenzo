using System;
using System.Collections.Generic;
using UnityEngine;

public class HS_AVLTree
{
    public HS_AVLNode Root;

    int Height(HS_AVLNode n) => n == null ? 0 : n.height;

    int BalanceFactor(HS_AVLNode n) => n == null ? 0 : Height(n.left) - Height(n.right);

    void UpdateHeight(HS_AVLNode n) => n.height = 1 + Math.Max(Height(n.left), Height(n.right));

    // comparación: devuelve >0 si a > b (a es "mayor" en orden)
    int CompareScores(Score a, Score b)
    {
        if (a.points != b.points) return a.points.CompareTo(b.points);
        return string.Compare(a.playerName, b.playerName, StringComparison.Ordinal);
    }

    HS_AVLNode RotateRight(HS_AVLNode y)
    {
        var x = y.left;
        var T2 = x.right;
        x.right = y;
        y.left = T2;
        UpdateHeight(y);
        UpdateHeight(x);
        return x;
    }

    HS_AVLNode RotateLeft(HS_AVLNode x)
    {
        var y = x.right;
        var T2 = y.left;
        y.left = x;
        x.right = T2;
        UpdateHeight(x);
        UpdateHeight(y);
        return y;
    }

    HS_AVLNode InsertNode(HS_AVLNode node, Score score)
    {
        if (node == null) return new HS_AVLNode(score);

        int cmp = CompareScores(score, node.data);
        if (cmp < 0)
            node.left = InsertNode(node.left, score);
        else if (cmp > 0)
            node.right = InsertNode(node.right, score);
        else
            node.right = InsertNode(node.right, score); // duplicados a la derecha

        UpdateHeight(node);
        int balance = BalanceFactor(node);

        // Balanceo AVL
        if (balance > 1 && CompareScores(score, node.left.data) < 0)
            return RotateRight(node);
        if (balance < -1 && CompareScores(score, node.right.data) > 0)
            return RotateLeft(node);
        if (balance > 1 && CompareScores(score, node.left.data) > 0)
        {
            node.left = RotateLeft(node.left);
            return RotateRight(node);
        }
        if (balance < -1 && CompareScores(score, node.right.data) < 0)
        {
            node.right = RotateRight(node.right);
            return RotateLeft(node);
        }

        return node;
    }

    public void Insert(Score score)
    {
        Root = InsertNode(Root, score);
    }

    public List<Score> InOrder()
    {
        var result = new List<Score>();
        InOrder(Root, result);  // llama al recursivo
        return result;
    }

    private void InOrder(HS_AVLNode node, List<Score> list)
    {
        if (node == null) return;
        InOrder(node.left, list);
        list.Add(node.data);
        InOrder(node.right, list);
    }

    // 🔹 PRE-ORDER (raíz, izquierda, derecha)
    public List<Score> PreOrder()
    {
        var result = new List<Score>();
        PreOrder(Root, result);
        return result;
    }

    void PreOrder(HS_AVLNode node, List<Score> list)
    {
        if (node == null) return;
        list.Add(node.data);
        PreOrder(node.left, list);
        PreOrder(node.right, list);
    }

    // 🔹 POST-ORDER (izquierda, derecha, raíz)
    public List<Score> PostOrder()
    {
        var result = new List<Score>();
        PostOrder(Root, result);
        return result;
    }

    void PostOrder(HS_AVLNode node, List<Score> list)
    {
        if (node == null) return;
        PostOrder(node.left, list);
        PostOrder(node.right, list);
        list.Add(node.data);
    }

    // 🔹 LEVEL-ORDER (por niveles, usando cola)
    public List<Score> LevelOrder()
    {
        var result = new List<Score>();
        if (Root == null) return result;
        var q = new Queue<HS_AVLNode>();
        q.Enqueue(Root);
        while (q.Count > 0)
        {
            var n = q.Dequeue();
            result.Add(n.data);
            if (n.left != null) q.Enqueue(n.left);
            if (n.right != null) q.Enqueue(n.right);
        }
        return result;
    }
}
