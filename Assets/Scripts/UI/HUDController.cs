using DefaultNamespace.UI;
using Skills;
using UnityEngine;
using UniRx;
using Zenject;
using Assets.Scripts.Player;
using Assets.Scripts.GameStates;

namespace DefaultNamespace
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private Transform _playerHealthProgressBar;
        [SerializeField] private EndGameScreen _endScreen;
        [SerializeField] private SkillIcon[] _skillIcons;

        private float _maxHealth;
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Init(GameStateMachine stateMachine)
        {
            _gameStateMachine = stateMachine;
            _endScreen.restartButton.onClick.AddListener(RestartScene);
        }

        public void Launch(PlayerModel playerModel)
        {
            _maxHealth = playerModel.MaxHealth;
            SetProgressBar(playerModel.health.Value);

            playerModel.health.Subscribe(SetProgressBar).AddTo(this);

            playerModel.isDead.Subscribe(isDead =>
            {
                if (isDead)
                {
                    _endScreen.gameObject.SetActive(true);
                }
            });
        }

        public void SetUpSkillIcons(ISkillController skillController)
        {
            for (int i = 0; i < skillController.GetSkillModels.Length; i++)
            {
                _skillIcons[i].Init(skillController.GetSkillModels[i], skillController.CurrentSkill);
            }
        }

        private void RestartScene()
        {
            _endScreen.gameObject.SetActive(false);
            _gameStateMachine.ChangeState(nameof(CoreGameState));
        }

        private void SetProgressBar(float health)
        {
            var scale = _playerHealthProgressBar.localScale;
            scale.x = Mathf.Max(health / _maxHealth, 0);
            _playerHealthProgressBar.localScale = scale;
        }
    }
}