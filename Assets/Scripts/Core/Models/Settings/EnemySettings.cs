using System;
using UnityEngine;

namespace MVC.Core.Models.Settings
{
    [Serializable]
    public record EnemyGeneralSettings
    {
        [SerializeField]
        private int count;
        [SerializeField]
        private float respawnTime;

        public int Count => count;
        public float RespawnTime => respawnTime;
    }

    [Serializable]
    public record EnemyHealthSettings
    {
        [SerializeField]
        private int maxHealth;

        public int MaxHealth => maxHealth;
    }

    [Serializable]
    public record EnemyMovementSettings
    {
        [SerializeField]
        private int speed;

        public int Speed => speed;
    }

    [Serializable]
    public record EnemyCombatSettings
    {
        [SerializeField]
        private float attackSpeed;
        [SerializeField]
        private float damage;

        public float AttackSpeed => attackSpeed;
        public float Damage => damage;
    }

    [CreateAssetMenu(fileName = "DefaultEnemySettings", menuName = "MVC/Settings/EnemySettings")]
    public class EnemySettings : ScriptableObject
    {
        [SerializeField]
        private EnemyGeneralSettings general;
        [SerializeField]
        private EnemyHealthSettings health;
        [SerializeField]
        private EnemyMovementSettings movement;
        [SerializeField]
        private EnemyCombatSettings combat;

        public EnemyGeneralSettings General => general;
        public EnemyHealthSettings Health => health;
        public EnemyMovementSettings Movement => movement;
        public EnemyCombatSettings Combat => combat;
    }
}