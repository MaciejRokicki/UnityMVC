using System;
using UnityEngine;

namespace MVC.Core.Models.Settings
{
    [Serializable]
    public record PlayerHealthSettings
    {
        [SerializeField]
        private float maxHealth;

        public float MaxHealth => maxHealth;
    }

    [Serializable]
    public record PlayerMovementSettings
    {
        [SerializeField]
        private float speed;

        public float Speed => speed;
    }

    [Serializable]
    public record PlayerCombatSettings
    {
        [SerializeField]
        private float minDistance;
        [SerializeField]
        private float damage;

        public float MinDistance => minDistance;
        public float Damage => damage;
    }

    [CreateAssetMenu(fileName = "DefaultPlayerSettings", menuName = "MVC/Settings/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField]
        private PlayerHealthSettings health;
        [SerializeField]
        private PlayerMovementSettings movement;
        [SerializeField]
        private PlayerCombatSettings combat;

        public PlayerHealthSettings Health => health;
        public PlayerMovementSettings Movement => movement;
        public PlayerCombatSettings Combat => combat;
    }
}