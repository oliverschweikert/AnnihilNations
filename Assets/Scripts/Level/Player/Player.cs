using UnityEngine;
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f, aimSpeed = 1f;
    public bool aiming, dead;
    public int totalHealth, currentHealth;
    public Vector2 movementV, mousePositionV;
    public Rigidbody2D rb;
    public Camera cam;
    public Animator animator;
    public GameObject crosshair;
    public HealthManager hm;
    public DeathScreen ds;

    private void Start()
    {
        Cursor.visible = false;
        animator.SetBool("Dead", false);
    }
    private void Update()
    {
        ProcessInput();
        if (!PauseMenu.gameIsPaused && !dead)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) animator.SetBool("Shooting", true);
            if (Input.GetKeyUp(KeyCode.Mouse0)) animator.SetBool("Shooting", false);
            if (Input.GetKeyDown(KeyCode.Mouse1)) TakeDamage(1);
            AnimateCharacter();
        }
    }
    private void FixedUpdate()
    {
        if (currentHealth > 0)
        {
            MoveCharacter();
            crosshair.transform.position = mousePositionV;
        }
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
    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage < 0 ? 0 : currentHealth - damage;
        hm.RedrawHearts();
        if (currentHealth == 0)
        {
            animator.SetBool("Dead", true);
        }
    }
    public void KillPlayer()
    {
        dead = true;
    }
}
