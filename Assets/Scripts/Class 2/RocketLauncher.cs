using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RocketLauncher : MonoBehaviour
{
    [SerializeField] private ForceMode launchMode;
    [SerializeField] private ForceMode torqueMode;
    [SerializeField, Range(0f, 100f)] private float launchForce;
    [SerializeField, Range(0f, 100f)] private float torqueForce;

    private Rigidbody rigidBody;


    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
            rigidBody.AddForce(transform.up * launchForce, launchMode);

        if (Input.GetKey(KeyCode.A))
            rigidBody.AddTorque(-transform.right * torqueForce, torqueMode);

        if (Input.GetKey(KeyCode.D))
            rigidBody.AddTorque(transform.right * torqueForce, torqueMode);
    }

    // Collision events (physically collides)
    void OnCollisionEnter (Collision collision)
    {
        Debug.Log($"Rocket entered collision with {collision.gameObject.name} at {collision.contactCount} contact points");
    }
    
    void OnCollisionStay (Collision collision)
    {
        Debug.Log($"Rocket stays in collision with {collision.gameObject.name} at {collision.contactCount} contact points");
    }
    
    void OnCollisionExit (Collision collision)
    {
        Debug.Log($"Rocket exited collision with {collision.gameObject.name}");
    }

    // Trigger events (enters an area without proper collision)
    void OnTriggerEnter (Collider other)
    {
        Debug.Log($"Trigger enter with: {other.gameObject.name}");
    }
    
    void OnTriggerStay (Collider other)
    {
        Debug.Log($"Trigger stay with: {other.gameObject.name}");
    }
    
    void OnTriggerExit (Collider other)
    {
        Debug.Log($"Trigger exit with: {other.gameObject.name}");
    }
}