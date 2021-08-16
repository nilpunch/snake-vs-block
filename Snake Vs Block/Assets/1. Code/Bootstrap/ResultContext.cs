using System;
using UnityEngine;

namespace SnakeVsBlock.Boot
{
    public class ResultContext : MonoBehaviour
    {
        public event Action Continued;
        public event Action Revived;
    }
}