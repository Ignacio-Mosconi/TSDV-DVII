using UnityEngine;
using UnityEngine.Events;

namespace Class8
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private enum State
        {
            Idle, Moving, Jumping, Shooting
        }

        [SerializeField] private float moveSpeed;
        [SerializeField] private float jumpForce;

        private new Rigidbody2D rigidbody2D;
        private FaceDirection currentFaceDirection = FaceDirection.Right; 
        private State currentState = State.Idle;
        private bool hasJustJumped;

        public UnityEvent<FaceDirection> OnStartedMoving { get; private set; } = new UnityEvent<FaceDirection>();
        public UnityEvent OnStoppedMoving { get; private set; } = new UnityEvent();
        public UnityEvent OnStartedJumping { get; private set; } = new UnityEvent();
        public UnityEvent OnFinishedJumping { get; private set; } = new UnityEvent();
        public UnityEvent OnFired { get; private set; } = new UnityEvent();


        void Awake ()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

        void Update ()
        {
            switch (currentState)
            {                
                case State.Idle:
                    UpdateIdle();
                    break;

                case State.Moving:
                    UpdateMoving();
                    break;
            }
        }

        private void Move (float horizontalInput)
        {
            transform.position += Vector3.right * (horizontalInput * moveSpeed * Time.deltaTime);
        }

        private void Jump (float horizontalInput)
        {
            Vector3 jumpDirection = transform.up;

            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            hasJustJumped = true;
            
            if (Mathf.Abs(horizontalInput) > 0f)
            {
                jumpDirection += Vector3.right * horizontalInput;
                jumpDirection.Normalize();
            }

            rigidbody2D.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
        }

        private void UpdateIdle ()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            bool isJumping = Input.GetButtonDown("Jump");
            bool isShooting = Input.GetButtonDown("Fire1");
            bool isMoving = Mathf.Abs(horizontalInput) > 0f;

            if (isShooting)
            {
                currentState = State.Shooting;
                OnFired.Invoke();
            }
            else if (isJumping)
            {
                currentState = State.Jumping;

                Jump(horizontalInput);
                OnStartedJumping.Invoke();
            }
            else if (isMoving)
            {
                currentState = State.Moving;
                currentFaceDirection = (horizontalInput > 0f) ? FaceDirection.Right : FaceDirection.Left;

                Move(horizontalInput);
                OnStartedMoving.Invoke(currentFaceDirection);
            }
        }

        private void UpdateMoving ()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            bool isJumping = Input.GetButtonDown("Jump");
            bool isShooting = Input.GetButtonDown("Fire1");
            bool isMoving = Mathf.Abs(horizontalInput) > 0f;

            if (isShooting)
            {
                currentState = State.Shooting;
                OnFired.Invoke();
            }
            if (isJumping)
            {
                currentState = State.Jumping;

                Jump(horizontalInput);
                OnStartedJumping.Invoke();
            }
            else if (isMoving)
            {
                FaceDirection newFaceDirection = (horizontalInput > 0f) ? FaceDirection.Right : FaceDirection.Left;

                if (newFaceDirection != currentFaceDirection)
                {
                    currentFaceDirection = newFaceDirection;
                    OnStartedMoving.Invoke(currentFaceDirection);
                }

                Move(horizontalInput);
            }
            else
            {
                currentState = State.Idle;
                OnStoppedMoving.Invoke();
            }
        }

        private void OnCollisionEnter2D (Collision2D collision)
        {
            if (currentState != State.Jumping || hasJustJumped)
                return;

            if (collision.gameObject.CompareTag("Ground"))
            {
                foreach (ContactPoint2D contact in collision.contacts)
                {
                    if (contact.normal.y > 0.5f)
                    {
                        currentState = State.Idle;
                        rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                        rigidbody2D.linearVelocity = Vector2.zero;
                        OnFinishedJumping.Invoke();
                        break;
                    }
                }
            }
        }

        public void OnAirborneAfterJump () => hasJustJumped = false;
        public void OnFinishShooting () => currentState = State.Idle;
    }
}