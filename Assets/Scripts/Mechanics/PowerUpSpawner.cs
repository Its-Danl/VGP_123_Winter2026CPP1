using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public enum PickupType
    {
        Life = 0,
        Powerup = 1
    }

    [SerializeField] private PickupType pickupType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController pc = collision.GetComponent<PlayerController>();

            switch (pickupType)
            {
                case PickupType.Life:
                    pc.lives++;
                    break;
                case PickupType.Powerup:
                    pc.JumpForceChange();
                    break;
            }
            Destroy(gameObject);
        }
    }
}
