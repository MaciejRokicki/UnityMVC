using UnityEngine;
using UnityEngine.AI;

namespace MVC.Models
{
    public class EnemyModel : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent navMeshAgent;
        public NavMeshAgent NavMeshAgent => navMeshAgent;
    }
}
