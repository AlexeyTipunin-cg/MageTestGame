using UniRx;

namespace Skills
{
    public class SkillModel
    {
        public ReactiveCommand<SkillData> command = new ReactiveCommand<SkillData>();
        public ReactiveProperty<SkillState> skillState = new ReactiveProperty<SkillState>();
        public ReactiveProperty<float> CurrentCoolDownTime = new ReactiveProperty<float>();
        
        public bool IsActive => skillState.Value == SkillState.Active;
        public bool CanActivate => CurrentCoolDownTime.Value <= 0;

        public float CoolDownTime => _skill.cooldownTimeSec;
        
        private Skill _skill;
        
        public SkillModel(Skill skill)
        {
            _skill = skill;
            skillState.Value = SkillState.Active;
        }

        public void SetState(SkillState state)
        {
            CurrentCoolDownTime.Value = _skill.cooldownTimeSec;
            skillState.Value = state;
        }

        public void Attack()
        {
            command.Execute(new SkillData());
        }
    }
}