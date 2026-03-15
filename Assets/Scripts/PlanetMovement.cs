using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    [SerializeField] private Transform rotationPivot;
    [SerializeField] private float translationRadius = 10f;
    [SerializeField] private float translationAngularSpeed = 15f;
    [SerializeField] private float rotationSpeed = 10f;

    private float translationAngle = 0f;

   
    void Update ()
    {
        if (!rotationPivot)
            return;

        Quaternion forwardRotation = Quaternion.AngleAxis(translationAngle, Vector3.up);
        Vector3 newPosition = rotationPivot.position + forwardRotation * rotationPivot.forward * translationRadius;

        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        transform.Translate(newPosition - transform.position, Space.World);
        
        translationAngle += translationAngularSpeed * Time.deltaTime;

        if (translationAngle >= 360f)
            translationAngle -= 360f;
    }
}