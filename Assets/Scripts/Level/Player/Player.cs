using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f, aimSpeed = 1f;
    public bool aiming, dead;
    public int totalHealth, currentHealth;
    public Vector2 movementV, mousePositionV, lookDir;
    public Rigidbody2D rb;
    public Camera cam;
    public Animator animator;
    public GameObject crosshair;
    public Image damageFlashScreen;
    public HealthManager hm;
    float timeSinceMelee;
    private void Start()
    {
        timeSinceMelee = 0;
        Cursor.visible = false;
        animator.SetBool("Dead", false);
    }
    private void Update()
    {
        timeSinceMelee += Time.deltaTime;
        ProcessInput();
        if (!PauseMenu.gameIsPaused && !dead)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) animator.SetBool("Shooting", true);
            if (Input.GetKeyUp(KeyCode.Mouse0)) animator.SetBool("Shooting", false);
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
        lookDir = mousePositionV - rb.position;
        animator.SetFloat("MouseX", lookDir.normalized.x);
        animator.SetFloat("MouseY", lookDir.normalized.y);
        if (!aiming)
        {
            animator.SetFloat("Horizontal", movementV.x);
            animator.SetFloat("Vertical", movementV.y);
            animator.SetFloat("Speed", movementV.sqrMagnitude);
            animator.SetFloat("Direction", Vector2.Dot(lookDir.normalized, movementV.normalized));
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
        if (!dead)
        {
            StartCoroutine(ScreenFlash());
            FindObjectOfType<AudioManager>().Play("TakeDamage");
        }
        if (currentHealth == 0)
        {
            animator.SetBool("Dead", true);
            AudioManager am = FindObjectOfType<AudioManager>();
            if (Array.Find(am.sounds, sound => sound.name == "BossMusic").source.isPlaying)
            {
                StartCoroutine(am.FadeOut("BossMusic", 10f));
                StartCoroutine(am.FadeIn("BackgroundMusic", 10f));

            }
        }
    }
    public void KillPlayer()
    {
        dead = true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
            if (timeSinceMelee > 1)
            {
                TakeDamage(1);
                timeSinceMelee = 0;
            }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EBullet")
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
    private IEnumerator ScreenFlash()
    {
        Color c = damageFlashScreen.color;
        for (float alpha = .8f; alpha > 0; alpha -= .02f)
        {
            c.a = alpha;
            damageFlashScreen.color = c;
            yield return new WaitForFixedUpdate();
        }
        c.a = 0;
        damageFlashScreen.color = c;
    }
}
