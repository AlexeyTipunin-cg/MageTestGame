using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Player;
using UniRx;
using UnityEngine;
using Zenject;

namespace Skills
{
    public class SkillController : MonoBehaviour
    {
        [SerializeField] private PlayerInputController _playerInput;
        [SerializeField] private SkillView[] _skillViews;

        private SkillModel[] _skills;
        public ReactiveProperty<SkillModel> _currentSkill = new ReactiveProperty<SkillModel>();
        private int _currentSkillIndex;

        public SkillModel[] GetSkillModels => _skills;

        [Inject]
        private void Init(CreatureConfig wizardConfig, PlayerModel playerModel)
        {
            _skills = new SkillModel[wizardConfig.Skills.Length];

            for (int i = 0; i < wizardConfig.Skills.Length; i++)
            {
                SkillModel model = new SkillModel(wizardConfig.Skills[i]);
                _skills[i] = model;

                SkillView prefab = _skillViews.First(view => view.GetSkillType == wizardConfig.Skills[i].skillType);
                SkillView skillView = Instantiate(prefab, gameObject.transform);
                skillView.Init(playerModel, model);

            }

            _playerInput.OnAttack += Attack;
            _playerInput.OnNextSkill += ChooseNextSkill;
            _playerInput.OnPreviousSkill += ChoosePrevSkill;

            _currentSkill.Value = _skills[0];
            _currentSkillIndex = 0;
        }

        private void Attack()
        {
            if (_currentSkill.Value.IsActive)
            {
                _currentSkill.Value.Attack();
                _currentSkill.Value.SetState(SkillState.Cooldown);
            }
        }

        private void Update()
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

            _currentSkill.Value = _skills[_currentSkillIndex];
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

            _currentSkill.Value = _skills[_currentSkillIndex];
        }

        private void OnDisable()
        {
            _playerInput.OnAttack -= Attack;
            _playerInput.OnNextSkill -= ChooseNextSkill;
            _playerInput.OnPreviousSkill -= ChoosePrevSkill;
        }
    }
}