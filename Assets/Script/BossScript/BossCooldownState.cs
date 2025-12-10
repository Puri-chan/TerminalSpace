using UnityEngine;

public class BossCooldownState : BossState
{
    private readonly EnemyBehavior enemy;
    private float waitTimer;
    private int bulletswap
    {
        get
        {
            if (enemy.projectile == enemy.projectileBossA)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }

    public BossCooldownState(EnemyBehavior enemyBehavior)
    {
        enemy = enemyBehavior;
    }

    public void enter()
    {
        waitTimer = 0f;
    }

    public void tick()
    {
        waitTimer += Time.deltaTime;
        if (waitTimer >= 0.5f)
        {
            if (bulletswap == 0)
            {
                enemy.StateMachine.ChangeState(new BossEngageState(enemy));
            }
            else if (bulletswap == 1)
            {
                enemy.StateMachine.ChangeState(new BossNormalState(enemy));
            }
        }
    }
    public void exit()
    {

    }
}
