using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus
{
    private IBonus bonus;
    private BonusSettings settings;
    public BonusSettings Settings
    {
        get { return settings; }
        set 
        { 
            settings = value;
            bonus = settings.type switch
            {
                BonusType.Speed => new SpeedBonus(),
                BonusType.Invincible => new InvincibleBonus(),
                _ => null
            };
        }
    }
    public void StartBonus()
    {
        PlayerBehavior player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        player.StartCoroutine(bonus.Action(player, settings.duration));
    }

}
public class SpeedBonus : IBonus
{
    public IEnumerator Action(PlayerBehavior player, float duration)
    {
        Debug.Log("speed");
        player.CurrentSpeed += 1.5f;
        yield return new WaitForSeconds(duration);
        player.CurrentSpeed -= 1.5f;
    }
}
public class InvincibleBonus : IBonus
{
    public IEnumerator Action(PlayerBehavior player, float duration)
    {
        Debug.Log("inv");
        player.IsInvincible = true;
        yield return new WaitForSeconds(duration);
        player.IsInvincible = false;
    }
}
public interface IBonus
{
    public IEnumerator Action(PlayerBehavior player, float duration);
}
