using Game.Config;
using UnityEngine;

namespace Game.Level
{
    public class Item
    {
        private ItemView _view;
        private ItemConfig _config;

        public Transform LeftHandTarget => _view.LeftHandTarget;
        public Transform Transform => _view.transform;
        public Transform AimSightTransform => _view.AimSightTransform;
        public int AnimatorLayer => _config.AnimatorLayer;
        public virtual bool CanUse => true;
        
        public Item(ItemView view, ItemConfig config)
        {
            _view = view;
            _config = config;
        }

        public virtual bool Use()
        {
            if (!CanUse) return true;
            _view.Use();
            return true;
        }

        public void SetActive(bool isActive)
        {
            _view.gameObject.SetActive(isActive);
        }
        
        public struct Data
        {
            private float useCooldown;
            private float timer;

            public bool IsAvailable => timer > useCooldown;

            public Data(ItemConfig config)
            {
                useCooldown = config.UseCooldown;
                timer = 0;
            }

            public void Update(float deltaTime)
            {
                if (!IsAvailable)
                    timer += deltaTime;
            }

            public void ResetTimer()
            {
                timer = 0;
            }
        }
    }
}