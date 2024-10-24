using System.Linq;
using Assets.Scripts.Player;
using Assets.Scripts.Scene;
using UniRx;
using UnityEngine;
using Zenject;

namespace Skills
{
    public class SkillController : MonoBehaviour, ISkillController
    {
        [SerializeField] private SkillView[] _skillViews;
        
        private PlayerInputController _playerInput;

        private SkillModel[] _skills;
        private int _currentSkillIndex;
        private CreatureConfig _levelConfig;

        public ReactiveProperty<SkillModel> CurrentSkill { get; private set; } = new ReactiveProperty<SkillModel>();
        public SkillModel[] GetSkillModels => _skills;

        [Inject]
        private void Init(PlayerInputController playerInputController)
        {
            _playerInput = playerInputController;
        }

        public void Lanch(LevelConfig levelConfig, PlayerModel playerModel)
        {
            CreatureConfig config = levelConfig.playerConfig;
            _skills = new SkillModel[config.Skills.Length];

            for (int i = 0; i < config.Skills.Length; i++)
            {
                SkillModel model = new SkillModel(config.Skills[i]);
                _skills[i] = model;

                SkillView prefab = _skillViews.First(view => view.GetSkillType == config.Skills[i].skillType);
                SkillView skillView = Instantiate(prefab, gameObject.transform);
                skillView.Init(playerModel, model);

            }

            _playerInput.OnAttack += Attack;
            _playerInput.OnNextSkill += ChooseNextSkill;
            _playerInput.OnPreviousSkill += ChoosePrevSkill;

            CurrentSkill.Value = _skills[0];
            _currentSkillIndex = 0;

            Observable.EveryUpdate().Subscribe(UpdateController).AddTo(this);
        }

        private void Attack(SkillType skillType)
        {
            SkillModel skill = _skills.Where(x => x.GetSkillType() == skillType).First();
            CurrentSkill.Value = skill;

            if (CurrentSkill.Value.IsActive)
            {
                CurrentSkill.Value.Attack();
                CurrentSkill.Value.SetState(SkillState.Cooldown);
            }
        }

        private void UpdateController(long time)
        {
            for (int i = 0; i < _skills.Length; i++)
            {
                SkillModel skillModel = _skills[i];

                if (skillModel.IsActive) continue;

                skillModel.CurrentCoolDownTime.Value -= Time.deltaTime;

                if (skillModel.CanActivate)
                {
                    skillModel.CurrentCoolDownTime.Value = 0;
                    skillModel.SetState(SkillState.Active);
                }
            }
        }



        private void ChooseNextSkill()
        {
            if (_currentSkillIndex < _skills.Length - 1)
            {
                _currentSkillIndex++;
            }
            else
            {
                _currentSkillIndex = 0;
            }

            CurrentSkill.Value = _skills[_currentSkillIndex];
        }

        private void ChoosePrevSkill()
        {
            if (_currentSkillIndex > 0)
            {
                _currentSkillIndex--;
            }
            else
            {
                _currentSkillIndex = _skills.Length - 1;
            }

            CurrentSkill.Value = _skills[_currentSkillIndex];
        }

        private void OnDestroy()
        {
            _playerInput.OnAttack -= Attack;
            _playerInput.OnNextSkill -= ChooseNextSkill;
            _playerInput.OnPreviousSkill -= ChoosePrevSkill;
        }
    }
}