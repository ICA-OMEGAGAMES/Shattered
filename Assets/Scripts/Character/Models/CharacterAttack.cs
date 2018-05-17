public class CharacterAttack
{
    public float damage;
    public float cooldown;
    public bool rootable;

    public CharacterAttack(float damage, float cooldown, bool rootable)
    {
        this.damage = damage;
        this.cooldown = cooldown;
        this.rootable = rootable;
    }
}