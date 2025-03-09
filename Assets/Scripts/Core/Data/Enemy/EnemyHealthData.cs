using MVC.Common;
using R3;
using UnityEngine;

namespace MVC.Core.Data.Enemy
{
    public record EnemyHealthData
    {
        private float health;
        private float maxHealth;

        public float Health => health;
        public float MaxHealth => maxHealth;

        public Subject<ChangedValue<float>> OnHealthChanged;
        public Subject<ChangedValue<float>> OnMaxHealthChanged;

        public EnemyHealthData(float maxHealth)
        {
            OnHealthChanged = new Subject<ChangedValue<float>>();
            OnMaxHealthChanged = new Subject<ChangedValue<float>>();

            SetMaxHealth(maxHealth);
            IncreaseHealth(maxHealth);
        }

        public void IncreaseHealth(float health)
        {
            if (health == 0.0f)
                return;

            float previousHealth = this.health;
            this.health += health;
            this.health = Mathf.Clamp(this.health, 0.0f, maxHealth);
            OnHealthChanged.OnNext(new ChangedValue<float>(previousHealth, this.health));
        }

        public void SetMaxHealth(float maxHealth)
        {
            if (this.maxHealth == maxHealth)
                return;

            float previousMaxHealth = this.maxHealth;
            this.maxHealth = maxHealth;
            health = Mathf.Clamp(health, 0.0f, maxHealth);
            OnMaxHealthChanged.OnNext(new ChangedValue<float>(previousMaxHealth, this.maxHealth));
        }
    }
}