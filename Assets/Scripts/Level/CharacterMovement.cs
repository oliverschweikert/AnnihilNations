using UnityEngine;
public class CharacterMovement : MonoBehaviour
{
    [Header("Attributes:")]
    public float moveSpeed = 5f;
    public float aimSpeed = 1f;

    [Space]
    [Header("Statistics:")]
    public Vector2 movement;
    public Vector2 mousePosition;
    public bool aiming;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Camera cam;
    public Animator animator;
    public GameObject crosshair;

    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        ProcessInput();
        if (!PauseMenu.gameIsPaused)
        {
            AnimateCharacter();
        }
    }
    private void FixedUpdate()
    {
        SetPositions();
    }
    private void SetPositions()
    {
        if (aiming)
        {
            rb.MovePosition(rb.position + movement.normalized * aimSpeed * Time.fixedDeltaTime);
        }
        if (!aiming)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
        crosshair.transform.position = mousePosition;
    }
    private void ProcessInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        aiming = Input.GetKey(KeyCode.Mouse1);
    }
    private void AnimateCharacter()
    {
        LookAnimation();
        RunAnimation();
        AimAnimation();

        void AimAnimation()
        {
            if (aiming)
            {
                animator.SetFloat("Speed", 0);
            }
        }

        void RunAnimation()
        {
            if (!aiming)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);
            }
        }

        void LookAnimation()
        {
            Vector2 lookDirection = mousePosition - rb.position;
            animator.SetFloat("MouseX", lookDirection.x);
            animator.SetFloat("MouseY", lookDirection.y);
        }
    }
}
