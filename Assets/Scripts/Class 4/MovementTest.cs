using UnityEngine;

public class MovementTest : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float speed;
    [SerializeField, Range(1f, 10f)] private float travelDistance;

    private float distanceTraveled;

    
    void Update ()
    {
        float displacement = speed * Time.deltaTime;

        transform.Translate(transform.forward * displacement);

        distanceTraveled += displacement;

        if (distanceTraveled >= travelDistance)
        {
            distanceTraveled -= travelDistance;
            transform.Rotate(transform.up, 45f);
        }
    }
}
