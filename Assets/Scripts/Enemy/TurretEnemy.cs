using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class TurretEnemy : BaseEnemy
{
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private Transform target;
    private float timeSinceLastFire = 0f;
    public float range = 1f;

    public override void Start()
    {
        base.Start();
        GameManager.Instance.OnPlayerSpawned += OnPlayerSpawnedCallback;

        if (fireRate <= 0f )
        {
            fireRate = 2f;
            Debug.LogWarning("Fire rate must be greater than 0, defaulting.");
        }
    }
    private void OnPlayerSpawnedCallback(PlayerController player)
    {
        target = player.transform;
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (target.position.x < transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        if (stateInfo.IsName("Idle") && distanceToPlayer <= range)
        {
            if (Time.time >= timeSinceLastFire + fireRate)
            {
                anim.SetTrigger("Fire");
                timeSinceLastFire = Time.time;
            }
        }
    }
}
