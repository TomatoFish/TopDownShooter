using UnityEngine;

namespace Assets.Scripts
{
    public class Item : MonoBehaviour
    {
        public Transform LeftHandTarget;
        public Transform MuzzleTransform;
        public ParticleSystem MuzzleFlash;
        public int AnimatorLayer;
        public float UseCooldown;

        public virtual void Use()
        {
            
        }

        public struct Data
        {
            private float useCooldown;
            private float timer;

            public bool IsAvailable => timer > useCooldown;

            public Data(Item item)
            {
                useCooldown = item.UseCooldown;
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