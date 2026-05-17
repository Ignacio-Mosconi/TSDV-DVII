using UnityEngine;

namespace Class8
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(PlayerController))]
    public class PlayerAnimatorController : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private PlayerController playerController;
        
        private static readonly int IsMovingHash = Animator.StringToHash("Is Moving");
        private static readonly int JumpHash = Animator.StringToHash("Jumped");
        private static readonly int LandedHash = Animator.StringToHash("Landed");
        private static readonly int FiredHash = Animator.StringToHash("Fired");


        void Awake ()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            playerController = GetComponent<PlayerController>();
        }

        void Start ()
        {
            playerController.OnStartedMoving.AddListener(OnStartMoving);
            playerController.OnStoppedMoving.AddListener(OnStopMoving);
            playerController.OnStartedJumping.AddListener(OnStartJumping);
            playerController.OnFinishedJumping.AddListener(OnFinishJumping);
            playerController.OnFired.AddListener(OnFire);
        }

        void OnDestroy()
        {
            playerController.OnStartedMoving.RemoveListener(OnStartMoving);
            playerController.OnStoppedMoving.RemoveListener(OnStopMoving);
            playerController.OnStartedJumping.RemoveListener(OnStartJumping);
            playerController.OnFinishedJumping.RemoveListener(OnFinishJumping);
            playerController.OnFired.RemoveListener(OnFire);
        }


        private void OnStartMoving (FaceDirection faceDirection)
        {
            spriteRenderer.flipX = faceDirection != FaceDirection.Right;
            animator.SetBool(IsMovingHash, true);
        }

        private void OnStopMoving ()
        {
            animator.SetBool(IsMovingHash, false);
        }

        private void OnStartJumping ()
        {
            animator.SetTrigger(JumpHash);
            animator.SetBool(IsMovingHash, false);
        }

        private void OnFinishJumping ()
        {
            animator.SetTrigger(LandedHash);
        }

        private void OnFire ()
        {
            animator.SetTrigger(FiredHash);
            animator.SetBool(IsMovingHash, false);
        }
    }
}