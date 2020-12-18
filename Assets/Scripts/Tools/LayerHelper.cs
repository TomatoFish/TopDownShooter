using UnityEngine;

namespace Tools
{
    public static class LayerHelper
    {
        public const string PointerHitLayerName = "CameraFloor";
        
        public static int PointerHitLayer => 1 << LayerMask.NameToLayer(PointerHitLayerName);
    }
}