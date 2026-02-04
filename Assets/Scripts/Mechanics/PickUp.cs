using UnityEngine;

public abstract class PickUp : MonoBehaviour
{    

    abstract public void OnPickup(GameObject player);

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPickup(collision.gameObject);
            Destroy(gameObject);
        }
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.collider.CompareTag("Player"))
       {
            OnPickup(collision.collider.gameObject);
            Destroy(gameObject);
       }
    }
}
