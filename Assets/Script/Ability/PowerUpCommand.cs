using UnityEngine;

public class PowerUpCommand : ICommand
{
    private PlayerController _player;
    private PowerUpType _type;
    private float _duration;
    public PowerUpCommand(PlayerController player, PowerUpType t, float d)
    {
        _player = player;
        _type = t;
        _duration = d;

    }
    public void Execute()
    {
        _player.ApplyPowerUp(_type, _duration);
    }
    public void Undo()
    {

    }
}
