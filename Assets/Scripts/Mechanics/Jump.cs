using UnityEngine;

public class Jump : PickUp
{
    private Rigidbody2D rb;
    public override void OnPickup(GameObject player) => player.GetComponent<PlayerController>().JumpForceChange();

    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(-2f, 2f);
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(-2f, rb.linearVelocity.y);
    }
}
