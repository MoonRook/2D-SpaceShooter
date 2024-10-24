public class Timer 
{
    private float m_CurrentTime; // “екущее врем€

    // ¬озвращает свойство таймера (завершилс€ или нет), при условии что текущее врем€ <= 0
    public bool isFinished => m_CurrentTime <= 0;

    public Timer(float startTime)
    {
        Start(startTime);
    }
    public void Start(float startTime)
    {
        m_CurrentTime = startTime; // “екущее врем€ = стартовому времени
    }
    
    public void RemoveTime(float deltatime)  // ќтнимает колличественное значение времени
    {
        if (m_CurrentTime <= 0) return; // ≈сли текущее врем€ <= 0 выходит из метода

        m_CurrentTime -= deltatime; // “екущее значени€ времени -= колличественное значение времени
    }
}
