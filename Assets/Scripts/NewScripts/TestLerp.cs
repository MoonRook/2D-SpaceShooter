using UnityEngine;

public class TestLerp : MonoBehaviour
{
    /*public Transform Target;
    public float Speed;

    /// <summary>
    /// ƒвижение объекта от текущей позиции до позиции цели со скоростью * на врем€ обновлени€ кадра
    /// </summary>
    private void Update()
    {
        // ѕр€молинейное движени€ к цели с плавным торможением в конце
        //transform.position = Vector3.Lerp(transform.position, Target.position, Time.deltaTime);
        
        // ƒвижение по окружности с использованием градусов векторов направлени€ 
        //transform.position = Vector3.Slerp(transform.position, Target.position, Time.deltaTime);
        
        // ѕр€молинейное плавное движени€ к цели 
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
