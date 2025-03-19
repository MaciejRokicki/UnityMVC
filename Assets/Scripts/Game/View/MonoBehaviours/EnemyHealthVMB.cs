using Cysharp.Text;
using MVC.Common;
using MVC.Game.Logic.MonoBehaviours;
using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.Game.View.MonoBehaviorus
{
    public class EnemyHealthVMB : MonoBehaviour
    {
        private EnemyMB enemy;
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private TextMeshProUGUI text;
        private IDisposable disposable;

        public EnemyMB Enemy
        {
            get => enemy;
            set
            {
                if (value == null)
                {
                    if (enemy != null)
                    {
                        disposable?.Dispose();
                    }
                }

                enemy = value;

                if (enemy != null)
                {
                    disposable = Disposable.Combine(
                        enemy.Health.OnHealthChanged.Subscribe(HealthData_OnHealthChanged),
                        enemy.Health.OnMaxHealthChanged.Subscribe(HealthData_OnMaxHelathChanged)
                    );

                    HealthData_OnHealthChanged(new ChangedValue<float>(0.0f, enemy.Health.Health));
                    HealthData_OnMaxHelathChanged(new ChangedValue<float>(0.0f, enemy.Health.MaxHealth));
                }
            }
        }

        public Slider Slider => slider;
        public TextMeshProUGUI Text => text;

        private void OnDestroy()
        {
            disposable.Dispose();
        }

        private void HealthData_OnHealthChanged(ChangedValue<float> changedValue)
        {
            slider.maxValue = enemy.Health.MaxHealth;
            slider.value = enemy.Health.Health;
            text.SetTextFormat("{0}/{1}", enemy.Health.Health, enemy.Health.MaxHealth);
        }

        private void HealthData_OnMaxHelathChanged(ChangedValue<float> changedValue)
        {
            slider.maxValue = enemy.Health.MaxHealth;
            slider.value = enemy.Health.Health;
            text.SetTextFormat("{0}/{1}", enemy.Health.Health, enemy.Health.MaxHealth);
        }
    }
}
