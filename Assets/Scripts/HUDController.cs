using System;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.SceneManagement;
using Zenject;

namespace DefaultNamespace
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private Transform _playerHealthProgressBar;
        [SerializeField] private EndGameScreen _endScreen;

        private float _maxHealth;

        [Inject]
        private void Init(CharacterWizardController characterWizard)
        {
            _maxHealth = characterWizard.PlayerModel.MaxHealth;

            SetProgressBar(characterWizard.PlayerModel.health.Value);

            characterWizard.PlayerModel.health.Subscribe(SetProgressBar).AddTo(this);

            _endScreen.restartButton.onClick.AddListener(RestartScene);


            characterWizard.PlayerModel.isDead.Subscribe(isDead =>
            {
                if (isDead)
                {
                    _endScreen.gameObject.SetActive(true);
                }
            });
        }

        private void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            _endScreen.gameObject.SetActive(false);
        }

        private void SetProgressBar(float health)
        {
            var scale = _playerHealthProgressBar.localScale;
            scale.x = Mathf.Max(health / _maxHealth, 0);
            _playerHealthProgressBar.localScale = scale;
        }
    }
}