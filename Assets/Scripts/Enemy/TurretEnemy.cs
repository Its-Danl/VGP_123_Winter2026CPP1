using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class TurretEnemy : BaseEnemy
{
    [SerializeField] private float fireRate = 2f;
    private float timeSinceLastFire = 0f;
    public float range = 5f;

    private PlayerController playerRef;

    public override void Start()
    {
        base.Start();

        if (fireRate <= 0f )
        {
            fireRate = 2f;
            Debug.LogWarning("Fire rate must be greater than 0, defaulting.");
        }

        //Shoot.OnProjectileFired += () => timeSinceLastFire = Time.deltaTime;
        GameManager.Instance.OnPlayerSpawned += (PlayerController player) => playerRef = player;

    }

    private void Update()
    {
        if (playerRef == null) return;
 
        if (!CheckDistance())
        {
            sr.color = Color.white;
            return;
        }

        sr.flipX = playerRef.transform.position.x < transform.position.x;
        sr.color = Color.red;

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Idle"))
        {
            if (Time.time >= timeSinceLastFire + fireRate)
            {
                anim.SetTrigger("Fire");
                timeSinceLastFire = Time.time;
            }
        }
    }

    bool CheckDistance()
    {
        float distanceToPlayer = Mathf.Abs(transform.position.x - playerRef.transform.position.x);
        return distanceToPlayer <= range;
    }
}
