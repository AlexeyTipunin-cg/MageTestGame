using UniRx;

namespace Skills
{
    public interface ISkillController
    {
        ReactiveProperty<SkillModel> CurrentSkill { get; }
        SkillModel[] GetSkillModels { get; }
    }
}