public static class MAINGame
{
    private static bool enemyFollow = false;

    public static void EnemyFollow(bool flag)
    {
        enemyFollow = flag;
    }

    public static bool IsEnemyFollow()
    {
        return enemyFollow;
    }
}