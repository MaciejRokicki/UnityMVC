using UnityEngine;
using UnityEngine.AI;

namespace MVC.Core.Models
{
    public class EnemyModel : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent navMeshAgent;
        public NavMeshAgent NavMeshAgent => navMeshAgent;
    }
}
