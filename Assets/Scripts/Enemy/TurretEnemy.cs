using UnityEngine;


public class TurretEnemy : BaseEnemy
{
    [SerializeField] private float fireRate = 2f;
    private float timeSinceLastFire = 0f;

    public override void Start()
    {
      base.Start();
      
        if (fireRate <= 0f )
        {
            fireRate = 2f;
            Debug.LogWarning("Fire rate must be greater than 0, defaulting.");
        }
    }

    private void Update()
    {
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

}
