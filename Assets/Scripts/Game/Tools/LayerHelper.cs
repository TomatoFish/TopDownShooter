using UnityEngine;

namespace Tools
{
    public static class LayerHelper
    {
        public const string PointerFloorHitLayerName = "CameraFloor";
        public const string PointerEnemyHitLayerName = "Enemy";
        
        public static int PointerFloorHitLayer => 1 << LayerMask.NameToLayer(PointerFloorHitLayerName);
        public static int PointerEnemyHitLayer => 1 << LayerMask.NameToLayer(PointerEnemyHitLayerName);
    }
}