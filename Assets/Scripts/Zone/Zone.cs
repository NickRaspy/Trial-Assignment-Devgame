using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] private Type type;
    private IZone zone;
    private void Start()
    {
        zone = type switch
        {
            Type.Slow => new SlowZone(),
            Type.Death => new DeathZone(),
            _ => null
        };
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) zone.EnterAction(other.GetComponent<PlayerBehavior>());
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) zone.ExitAction(other.GetComponent<PlayerBehavior>());
    }
    public enum Type
    {
        Slow, Death
    }
}
#region ZONES_AND_INTERFACES
public class SlowZone: IZone
{
    public void EnterAction(PlayerBehavior player) => player.CurrentSpeed *= 0.6f;
    public void ExitAction(PlayerBehavior player) => player.CurrentSpeed /= 0.6f;
}
public class DeathZone : IZone
{
    public void EnterAction(PlayerBehavior player)
    {
        player.MustDie = true;
        if(!player.IsInvincible) player.Death();
    }
    public void ExitAction(PlayerBehavior player) => player.MustDie = false;
}
public interface IZone
{
    public void EnterAction(PlayerBehavior player);
    public void ExitAction(PlayerBehavior player);
}
#endregion