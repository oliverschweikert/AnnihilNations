using UnityEngine;
public class CharacterMovement : MonoBehaviour
{
    [Header("Attributes:")]
    public float moveSpeed = 5f;
    public float aimSpeed = 1f;

    [Space]
    [Header("Statistics:")]
    public Vector2 movementV;
    public Vector2 mousePositionV;
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
        MoveCharacter();
        crosshair.transform.position = mousePositionV;
    }
    private void MoveCharacter()
    {
        if (aiming)
        {
            rb.MovePosition(rb.position + movementV.normalized * aimSpeed * Time.fixedDeltaTime);
        }
        if (!aiming)
        {
            rb.MovePosition(rb.position + movementV.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
    private void ProcessInput()
    {
        movementV.x = Input.GetAxisRaw("Horizontal");
        movementV.y = Input.GetAxisRaw("Vertical");
        mousePositionV = cam.ScreenToWorldPoint(Input.mousePosition);
        aiming = Input.GetKey(KeyCode.Mouse1);
    }
    private void AnimateCharacter()
    {
        Vector2 lookDirection = mousePositionV - rb.position;
        animator.SetFloat("MouseX", lookDirection.normalized.x);
        animator.SetFloat("MouseY", lookDirection.normalized.y);
        if (!aiming)
        {
            animator.SetFloat("Horizontal", movementV.x);
            animator.SetFloat("Vertical", movementV.y);
            animator.SetFloat("Speed", movementV.sqrMagnitude);
            animator.SetFloat("Direction", Vector2.Dot(lookDirection.normalized, movementV.normalized));
        }
        if (aiming)
        {
            animator.SetFloat("Speed", 0);
        }
    }
}
