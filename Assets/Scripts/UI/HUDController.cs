using System;
using DefaultNamespace.UI;
using Skills;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.SceneManagement;
using Zenject;
using Assets.Scripts.Player;

namespace DefaultNamespace
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private Transform _playerHealthProgressBar;
        [SerializeField] private EndGameScreen _endScreen;
        [SerializeField] private SkillIcon[] _skillIcons;

        private float _maxHealth;

        [Inject]
        private void Init(PlayerModel playerModel, SkillController skillController)
        {
            _maxHealth = playerModel.MaxHealth;

            SetProgressBar(playerModel.health.Value);

            playerModel.health.Subscribe(SetProgressBar).AddTo(this);

            _endScreen.restartButton.onClick.AddListener(RestartScene);


            playerModel.isDead.Subscribe(isDead =>
            {
                if (isDead)
                {
                    _endScreen.gameObject.SetActive(true);
                }
            });

            for (int i = 0; i < skillController.GetSkillModels.Length; i++)
            {
                _skillIcons[i].Init(skillController.GetSkillModels[i], skillController._currentSkill);
            }
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