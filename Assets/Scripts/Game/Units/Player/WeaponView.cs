using UnityEngine;
using Zenject;

namespace Game.Level
{
    public class WeaponView : ItemView
    {
        [SerializeField] private Transform _muzzleTransform;
        [SerializeField] private ParticleSystem _muzzleFlash;
        [SerializeField] private LineRenderer _bulletTrail;
        [SerializeField] private Transform _aimSightTransform;
        [SerializeField] private ParticleSystem _hitEffect;

        [Inject] private SignalBus _signalBus;
        [Inject] private VFXManager _vfxManager;
        
        public override Transform AimSightTransform => _aimSightTransform;
        
        public override bool Use()
        {
            var muzzleFlash = _vfxManager.Get<ParticleSystem>("bullet_muzzle_flash");
            muzzleFlash.transform.SetPositionAndRotation(_muzzleTransform.position, _muzzleTransform.rotation);
            muzzleFlash.Play();
            _vfxManager.ReturnToPoolDelayed(muzzleFlash.gameObject, muzzleFlash.main.duration);
            
            if (Physics.Raycast(_muzzleTransform.position, _muzzleTransform.forward, out var hit))
            {
                SpawnBulletTrail(hit.point);

                var hittable = hit.collider.GetComponent<IHittable>();
                if (hittable != null)
                {
                    hittable.Hit(hit.point, hit.normal);
                    _signalBus.Fire(new DamageSignal(1, hit.point, hit.normal));
                }
                else
                {
                    SpawnHitEffect(hit.point, hit.normal);
                }
            }

            return true;
        }

        private void SpawnBulletTrail(Vector3 targetPoint)
        {
            var trail = _vfxManager.Get<LineRenderer>("bullet_trail", 0.1f);
            trail.gameObject.SetActive(true);
            trail.SetPosition(0, _muzzleTransform.position);
            trail.SetPosition(1, targetPoint);
        }
        
        private void SpawnHitEffect(Vector3 targetPoint, Vector3 targetnormal)
        {
            var effect = _vfxManager.Get<ParticleSystem>("bullet_hit_metal");
            effect.transform.position = targetPoint;
            effect.transform.forward = targetnormal;
            effect.Play();
            _vfxManager.ReturnToPoolDelayed(effect.gameObject, effect.main.duration);
        }
    }
}