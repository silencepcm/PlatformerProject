using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Unity.FPS.Game
{
    public class Soif : MonoBehaviour
    {
        [Tooltip("Maximum amount of litres")] public float MaxLitresEstomac = 10f;


        public UnityAction<float, GameObject> OnDamaged;
        public UnityAction<float> OnHealed;
        public UnityAction OnDie;

        public float CurrentWater { get; set; }
        public bool Invincible { get; set; }
        public bool CanPickup() => CurrentWater < MaxLitresEstomac;

        public float GetRatio() => CurrentWater / MaxLitresEstomac;

        [Tooltip("Critical Health Ratio")] public float CriticalHealthRatio = 1f;
        public bool IsCritical() => GetRatio() <= CriticalHealthRatio;

        bool m_IsDead;

        void Start()
        {
            CurrentWater = MaxLitresEstomac;
        }

        public void Heal(float healAmount)
        {
            float WaterBefore = CurrentWater;
            CurrentWater += healAmount;
            CurrentWater = Mathf.Clamp(CurrentWater, 0f, MaxLitresEstomac);

            // call OnHeal action
            float trueHealAmount = CurrentWater - WaterBefore;
            if (trueHealAmount > 0f)
            {
                OnHealed?.Invoke(trueHealAmount);
            }
        }

        public void TakeDamage(float damage, GameObject damageSource)
        {
            if (Invincible)
                return;

            float WaterBefore = CurrentWater;
            CurrentWater -= damage;
            CurrentWater = Mathf.Clamp(CurrentWater, 0f, MaxLitresEstomac);

            // call OnDamage action
            float trueDamageAmount = WaterBefore - CurrentWater;
            if (trueDamageAmount > 0f)
            {
                OnDamaged?.Invoke(trueDamageAmount, damageSource);
            }

            HandleDeath();
        }

        public void Kill()
        {
            CurrentWater = 0f;

            // call OnDamage action
            OnDamaged?.Invoke(MaxLitresEstomac, null);

            HandleDeath();
        }

        void HandleDeath()
        {
            if (m_IsDead)
                return;

            // call OnDie action
            if (CurrentWater <= 0f)
            {
                m_IsDead = true;
                OnDie?.Invoke();
            }
        }
    }
}
