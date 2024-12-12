public static class MAINGame
{
    private static bool enemyFollow = false;
    static int m_star = 0;
    static int m_health = 5;

    public static int GoldStars;
    public static int Stars { get { return m_star; } }
    public static int Health { get { return m_health * 10; } }

    public static void IncStar(int value)
    {
        m_star += value;
        if (m_star >= 50)
        {
            m_star = 0;
            IncHealth(1);
        }
    }

    public static void DecStar(int value)
    {
        m_star -= value;
        if (m_star < 0) m_star = 0;
    }

    public static void ResetStar()
    {
        m_star = 0;
    }

    public static void ResetHealth()
    {
        m_health = 5;
    }

    public static void IncHealth(int value)
    {
        m_health += value;
        if (m_health > 5) m_health = 5;
    }

    public static void DecHealth(int value)
    {
        m_health -= value;
        if (m_health < 0) m_health = 0;
    }

    public static void EnemyFollow(bool flag)
    {
        enemyFollow = flag;
    }

    public static bool IsEnemyFollow()
    {
        return enemyFollow;
    }
}