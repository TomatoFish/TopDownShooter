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
        
        public override Transform AimSightTransform => _aimSightTransform;
        
        public override bool Use()
        {
            _muzzleFlash.Play();
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

        // todo: change to vfx manager
        private void SpawnBulletTrail(Vector3 targetPoint)
        {
            var trail = Instantiate(_bulletTrail);
            trail.gameObject.SetActive(true);
            trail.SetPosition(0, _muzzleTransform.position);
            trail.SetPosition(1, targetPoint);
            Destroy(trail.gameObject, 0.1f);
        }
        
        // todo: change to vfx manager
        private void SpawnHitEffect(Vector3 targetPoint, Vector3 targetnormal)
        {
            var effect = Instantiate(_hitEffect);
            effect.transform.position = targetPoint;
            effect.transform.forward = targetnormal;
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);
        }
    }
}