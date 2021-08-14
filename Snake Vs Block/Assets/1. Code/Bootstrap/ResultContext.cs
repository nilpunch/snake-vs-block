using System;
using UnityEngine;

namespace Snake.Boot
{
    public class ResultContext : MonoBehaviour
    {
        public event Action Continued;
        public event Action Revived;
    }
}