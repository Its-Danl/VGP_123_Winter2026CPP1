using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    private Rigidbody2D rb;
    public int livesToAdd = 1;
    public enum PickupType
    {
        Life = 0,
        Powerup = 1
    }

    //[SerializeField] private PickupType pickupType;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        PlayerController pc = collision.GetComponent<PlayerController>();

    //        switch (pickupType)
    //        {
    //            case PickupType.Life:
    //                pc.lives++;
    //                break;
    //            case PickupType.Powerup:
    //                pc.JumpForceChange();
    //                break;
    //        }
    //        Destroy(gameObject);
    //    }
    //}

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(-2f, 2f);
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(-2f, rb.linearVelocity.y);
    }
}
