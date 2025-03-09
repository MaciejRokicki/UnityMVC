using UnityEngine;

namespace MVC.Data.Player
{
    public record PlayerHealthData
    {
        private float health;
        private float maxHealth;

        public float Health => health;
        public float MaxHealth => maxHealth;

        public PlayerHealthData(float maxHealth)
        {
            SetMaxHealth(maxHealth);
            IncreaseHealth(maxHealth);
        }

        public void IncreaseHealth(float health)
        {
            this.health += health;
            this.health = Mathf.Clamp(this.health, 0.0f, maxHealth);
        }

        public void SetMaxHealth(float maxHealth)
        {
            this.maxHealth = maxHealth;
            health = Mathf.Clamp(health, 0.0f, maxHealth);
        }
    }
}
