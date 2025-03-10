using MVC.Core.Models;

namespace MVC.Core.Data.Enemy
{
    public record EnemyData
    {
        public EnemyModel Model { get; set; }
        public EnemyHealthData HealthData { get; set; }
        public EnemyMovementData MovementData { get; set; }
        public EnemyCombatData CombatData { get; set; }
    }
}