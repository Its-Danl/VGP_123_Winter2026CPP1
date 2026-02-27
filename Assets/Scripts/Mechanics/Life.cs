using UnityEngine;

public class Life : PickUp
{
    private Rigidbody2D rb;
    public int livesToAdd = 1;
    public override void OnPickup(GameObject player) => GameManager.Instance.Lives += livesToAdd;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(-2f,2f);
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(-2f,rb.linearVelocity.y);
    }
}
