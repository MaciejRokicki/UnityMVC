using Cysharp.Text;
using MVC.Common;
using MVC.Core.Data.Enemy;
using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.Core.ViewModels
{
    public class EnemyHealthViewModel : MonoBehaviour
    {
        private EnemyData enemyData;
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private TextMeshProUGUI text;
        private IDisposable disposable;

        public EnemyData EnemyData
        {
            get => enemyData;
            set
            {
                if (value == null)
                {
                    if (enemyData != null)
                    {
                        disposable?.Dispose();
                    }
                }

                enemyData = value;

                if (enemyData != null)
                {
                    disposable = Disposable.Combine(
                        enemyData.HealthData.OnHealthChanged.Subscribe(HealthData_OnHealthChanged),
                        enemyData.HealthData.OnMaxHealthChanged.Subscribe(HealthData_OnMaxHelathChanged)
                    );

                    HealthData_OnHealthChanged(new ChangedValue<float>(0.0f, enemyData.HealthData.Health));
                    HealthData_OnMaxHelathChanged(new ChangedValue<float>(0.0f, enemyData.HealthData.MaxHealth));
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
            slider.maxValue = enemyData.HealthData.MaxHealth;
            slider.value = enemyData.HealthData.Health;
            text.SetTextFormat("{0}/{1}", enemyData.HealthData.Health, enemyData.HealthData.MaxHealth);
        }

        private void HealthData_OnMaxHelathChanged(ChangedValue<float> changedValue)
        {
            slider.maxValue = enemyData.HealthData.MaxHealth;
            slider.value = enemyData.HealthData.Health;
            text.SetTextFormat("{0}/{1}", enemyData.HealthData.Health, enemyData.HealthData.MaxHealth);
        }
    }
}
