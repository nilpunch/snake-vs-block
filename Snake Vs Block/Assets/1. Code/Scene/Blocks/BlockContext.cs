using UnityEngine;

namespace SnakeVsBlock
{
    public class BlockContext : MonoBehaviour
    {
        [SerializeField] private VisualBlock _visualBlock = null;
        [SerializeField] private PhysicalBlock _physicalBlock = null;

        public VisualBlock Visual => _visualBlock;
        
        public PhysicalBlock Physical => _physicalBlock;
    }
}