public enum BuffType
{
    None = -1,
    AddHp               = 0,
    AddDamage           = 1,
    AddSpeed            = 2,
    AddDefense          = 3,
    AddEvades           = 4,
    AddEndurance        = 5,
    BuffToSkill         = 6,
    EnhancePassive      = 7,
    ReduceDealtDamage   = 8,

    Anger = 100,

}

public class Buff : SkillDelivery
{
    public BuffType buffType;
}
