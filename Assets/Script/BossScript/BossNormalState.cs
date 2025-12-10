using UnityEngine;

public class BossNormalState : BossState
{
    private readonly EnemyBehavior enemy;
    private float waitTimer;
    private bool alreadystarted
    {
        get
        {
            if (enemy.alreadystarted)
            {
                return true;
            }
            else
            {
               return false;
            }
        }
    }

    public BossNormalState(EnemyBehavior enemyBehavior)
    {
        enemy = enemyBehavior;
    }

    public void enter()
    {
        waitTimer = 0f;
        if (enemy.shootCoroutine != null)
        {
            enemy.projectile = enemy.projectileBossA;
        }
        else if (enemy.shootCoroutine == null && alreadystarted)
        {
            enemy.boss();
            enemy.projectile = enemy.projectileBossA;
        }
    }

    public void tick()
    {
        waitTimer += Time.deltaTime;
        if (waitTimer >= 5f)
        {
            enemy.StateMachine.ChangeState(new BossCooldownState(enemy));
        }
    }
    public void exit()
    {
        enemy.alreadystarted = true;
        enemy.StopCoroutine(enemy.shootCoroutine);
        enemy.shootCoroutine = null;
    }
}
