public class Timer 
{
    private float m_CurrentTime; // ������� �����

    // ���������� �������� ������� (���������� ��� ���), ��� ������� ��� ������� ����� <= 0
    public bool isFinished => m_CurrentTime <= 0;

    public Timer(float startTime)
    {
        Start(startTime);
    }
    public void Start(float startTime)
    {
        m_CurrentTime = startTime; // ������� ����� = ���������� �������
    }
    
    public void RemoveTime(float deltatime)  // �������� ��������������� �������� �������
    {
        if (m_CurrentTime <= 0) return; // ���� ������� ����� <= 0 ������� �� ������

        m_CurrentTime -= deltatime; // ������� �������� ������� -= ��������������� �������� �������
    }
}
