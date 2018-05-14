public class CharacterAttack
{
    public float damage;
    public float cooldown;
    public bool rootAble;

    public CharacterAttack(float damage, float cooldown, bool rootAble)
    {
        this.damage = damage;
        this.cooldown = cooldown;
        this.rootAble = rootAble;
    }
}