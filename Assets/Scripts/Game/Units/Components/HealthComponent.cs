using System;
using Zenject;

namespace Game.Level
{
    public class HealthComponent : IInitializable, IDisposable
    {
        [Inject] private SignalBus _signalBus;
        
        // shield
        private float _maxShield;
        private float _currentShield;
        // health
        private float _maxHealth;
        private float _currentHealth;
        //invincibility
        private float _maxInvincibleTimer;
        private float _currentInvincibleTimer;

        public float CurrentShield => _currentShield;
        public float CurrentHealth => _currentHealth;
        
        public void Initialize()
        {
            _signalBus.Subscribe<DamageSignal>(DealDamage);

            _maxShield = 100;
            _currentShield = _maxShield;
            _maxHealth = 1;
            _currentHealth = _maxHealth;
            _maxInvincibleTimer = 0;
            _currentInvincibleTimer = 0f;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<DamageSignal>(DealDamage);
        }

        private void DealDamage(DamageSignal damage)
        {
            if (_currentInvincibleTimer > 0f) return;
            
            var damageValue = damage.Value;
            if (_currentShield > 0f)
            {
                _currentShield = Math.Max(_currentShield - damageValue, 0f);
                if (_currentShield <= 0f)
                {
                    _currentInvincibleTimer = _maxInvincibleTimer;
                    _signalBus.Fire<ShieldDestroyedSignal>();
                }
            }
            else if (_currentHealth > 0)
            {
                _currentHealth = Math.Max(_currentHealth - damageValue, 0f);
                if (_currentHealth <= 0f)
                {
                    _signalBus.Fire<DeathSignal>();
                }
            }
        }

        private void HealShield(float value)
        {
            _currentShield = Math.Min(_currentShield + value, _maxShield);
        }
        
        private void HealHealth(float value)
        {
            _currentHealth = Math.Min(_currentHealth + value, _maxHealth);
        }
    }
}