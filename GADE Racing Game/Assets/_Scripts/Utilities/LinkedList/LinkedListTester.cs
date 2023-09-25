using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedListTester : MonoBehaviour
{
    private int[] testArray = new[] { 1, 3, 5, 10 };

    private void Awake()
    {
        CustomLinkedList<int> linkedList2 = new CustomLinkedList<int>();
        foreach (var value in testArray)
        {
            linkedList2.Add(value);
        }

        Node<int> currentNode = linkedList2.Head;
        this.Log(currentNode.Data);

        while (currentNode.NextNode != null)
        {
            currentNode = currentNode.NextNode;
            this.Log(currentNode.Data);
        }
    }
}
