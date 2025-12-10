using UnityEngine;

public class BossEngageState : BossState
{
    private readonly EnemyBehavior enemy;
    private float waitTimer;
    private float basebulletspeed;
    private float basecooldown;

    public BossEngageState(EnemyBehavior enemyBehavior)
    {
        enemy = enemyBehavior;
    }

    public void enter()
    {
        basecooldown = enemy.shotcooldown;
        enemy.shotcooldown = enemy.shotcooldown / 3;
        enemy.boss();
        enemy.projectile = enemy.projectileBossB;
        waitTimer = 0f;
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
        enemy.shotcooldown = basecooldown;
        enemy.StopCoroutine(enemy.shootCoroutine);
        enemy.shootCoroutine = null;
    }
}
