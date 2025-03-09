using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.Core.ViewModels
{
    public class PlayerHealthViewModel : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private TextMeshProUGUI text;

        public Slider Slider => slider;
        public TextMeshProUGUI Text => text;
    }
}
