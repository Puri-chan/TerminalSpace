using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public void ExcuteCommand(ICommand command)
    {
        if (command == null) return;
        command.Execute();
    }
}
