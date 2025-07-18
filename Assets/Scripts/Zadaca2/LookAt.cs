using System;
using UnityEngine;

namespace Zadaca2
{
    public class LookAt : MonoBehaviour
    {
        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}