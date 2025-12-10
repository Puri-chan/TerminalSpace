using UnityEngine;

public class PowerUpPickUp : MonoBehaviour
{
    public PowerUpType power;
    public float duration = 15f;
    public float speedprojectile = 5f;

    private PowerUpManager powerUpManager;
    private PlayerController playerController;

    private void Awake()
    {
        powerUpManager = FindFirstObjectByType<PowerUpManager>();
        playerController = FindFirstObjectByType<PlayerController>();
    }
    private void Update()
    {
        transform.position += transform.up * -speedprojectile * Time.deltaTime;
        if (transform.position.y < -9)
            {
                Destroy(gameObject);
            }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ICommand command = new PowerUpCommand(playerController, power, duration);
            powerUpManager.ExcuteCommand(command);
            Destroy(gameObject);
        }
    }
}
