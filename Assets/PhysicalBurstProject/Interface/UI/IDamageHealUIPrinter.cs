using UnityEngine;

public interface IDamageHealUIPrinter
{
    public void PrintDamage(Vector2 pos, int damage);

    public void PrintHeal(Vector2 pos, int heal);
}
