using UnityEngine;

public class TestLerp : MonoBehaviour
{
    /*public Transform Target;
    public float Speed;

    /// <summary>
    /// �������� ������� �� ������� ������� �� ������� ���� �� ��������� * �� ����� ���������� �����
    /// </summary>
    private void Update()
    {
        // ������������� �������� � ���� � ������� ����������� � �����
        //transform.position = Vector3.Lerp(transform.position, Target.position, Time.deltaTime);
        
        // �������� �� ���������� � �������������� �������� �������� ����������� 
        //transform.position = Vector3.Slerp(transform.position, Target.position, Time.deltaTime);
        
        // ������������� ������� �������� � ���� 
        transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);

    }*/

    public float a_f;
    public float b_f;
    public float r_f;

    [Range(0.0f, 1.0f)]
    public float t;

    private void Update()
    {
        r_f = Mathf.Lerp(a_f, b_f, t);
    }
}
