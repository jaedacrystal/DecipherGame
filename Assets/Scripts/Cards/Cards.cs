using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Cards : ScriptableObject
{
    public Sprite artwork;
    public string cardName;

    [TextArea(3, 3)]
    public string desc;

    public int bandwidth;
    public int effectValue;

    public ClassType classType;
    public EffectType effectType;
    public TargetType target;

    public void ApplyEffect(GameObject player, GameObject opponent)
    {
        GameObject targetObject = target == TargetType.Player ? player : opponent;
        Health targetHealth = targetObject.GetComponent<Health>();
        PlayerStats targetStats = targetObject.GetComponent<PlayerStats>();

        switch (effectType)
        {
            case EffectType.Attack:
                targetHealth.TakeDamage(effectValue);
                break;
            case EffectType.Defense:
                targetStats.IncreaseDefense(effectValue);
                break;
            case EffectType.Heal:
                targetHealth.Heal(effectValue);
                break;
            case EffectType.Draw:
                CardManager cardManager = FindObjectOfType<CardManager>();
                cardManager.DrawMultipleCards(effectValue);
                break;
            case EffectType.ShieldAndRetaliate:
                targetStats.StartCoroutine(ApplyShieldAndRetaliate(targetStats, targetHealth));
                break;
            default:
                break;
        }
    }

    private IEnumerator ApplyShieldAndRetaliate(PlayerStats targetStats, Health targetHealth)
    {
        targetStats.isInvulnerable = true;
        Debug.Log(targetStats.name + " is invulnerable this turn!");

        yield return new WaitForSeconds(1.5f);

        targetStats.isInvulnerable = false;
        Debug.Log(targetStats.name + " is no longer invulnerable.");

        targetHealth.TakeDamage(5);
        Debug.Log(targetHealth.name + " took 5 retaliate damage!");
    }
}

public enum EffectType
{
    None,
    Attack,
    Defense,
    Heal,
    Draw,
    ShieldAndRetaliate
}

public enum ClassType
{
    Defense,
    Offense,
    Balance
}

public enum TargetType
{
    Player,
    Opponent
}
