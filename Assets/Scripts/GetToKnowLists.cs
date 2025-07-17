using System;
using System.Collections.Generic;
using UnityEngine;

public class GetToKnowLists : MonoBehaviour
{
    public List<int> numbers;

    private void Start()
    {
        numbers.Sort();
        for (int i = numbers.Count - 1; i >= 0; i--)
        {
            Debug.Log(numbers[i]);
        }
    }
}