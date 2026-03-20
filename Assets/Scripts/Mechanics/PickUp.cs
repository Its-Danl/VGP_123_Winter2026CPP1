using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public abstract class PickUp : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    protected AudioSource audioSource;
    abstract public void OnPickup(GameObject player);

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource> ();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPickup(collision.gameObject);
            audioSource.PlayOneShot(pickupSound);
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, pickupSound.length);
        }
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.collider.CompareTag("Player"))
       {
            OnPickup(collision.collider.gameObject);
            audioSource.PlayOneShot(pickupSound);
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, pickupSound.length);
        }
    }
}
