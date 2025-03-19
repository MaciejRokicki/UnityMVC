namespace MVC.Game.Model.Enemy
{
    public record EnemyCombatModel
    {
        public float AttackSpeed { get; set; }
        public float Damage { get; set; }
        public float HitTimer { get; set; }
    }
}