using UnityEngine;

namespace Assets.Scripts
{
    public class Pistol : Item
    {
        public LineRenderer BulletTrail;
        
        
        public override void Use()
        {
            MuzzleFlash.Play();

            if (Physics.Raycast(MuzzleTransform.position, MuzzleTransform.forward, out var hit))
            {
                BulletTrail.gameObject.SetActive(true);
                BulletTrail.SetPosition(0, MuzzleTransform.position);
                BulletTrail.SetPosition(1, hit.point);
            }
        }
    }
}