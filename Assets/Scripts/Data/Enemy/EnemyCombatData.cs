namespace MVC.Data.Enemy
{
    public record EnemyCombatData
    {
        public float AttackSpeed { get; set; }
        public float Damage { get; set; }
        public float HitTimer { get; set; }
    }
}