using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.Game.View.MonoBehaviorus
{
    public class PlayerHealthVMB : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private TextMeshProUGUI text;

        public Slider Slider => slider;
        public TextMeshProUGUI Text => text;
    }
}
