using MVC.Game.Model.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace MVC.Game.Logic.MonoBehaviours
{
    public class EnemyMB : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent navMeshAgent;
        public NavMeshAgent NavMeshAgent => navMeshAgent;

        public EnemyHealthModel Health { get; set; }
        public EnemyMovementModel Movement { get; set; }
        public EnemyCombatModel Combat { get; set; }
    }
}
