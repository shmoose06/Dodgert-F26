using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float phaseDuration = 1f;
    [SerializeField] private float phaseAlpha = 0.5f;

    private bool isInvulnerable = false;
    private float invulnerabilityTimer = 0f;
    [SerializeField]private float phaseCooldown = 5f;
    private bool phaseAvail = true;

    private float timer = 0f;

    public LogicScript logic;

    //AUDIO
    AudioManager audioManager;
    private bool hasSoundPlayed = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    } //END AUDIO

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        //Death Condition
        if(logic.playerHealth <=0)
        {
            Destroy(gameObject);
        } 

        rb.linearVelocity = moveInput * moveSpeed;

        // Handle invulnerability timer
        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;

            //Make temporary
            if (invulnerabilityTimer <= 0)
            {

                isInvulnerable = false;

                Color color = spriteRenderer.color;
                color.a = 1f;
                spriteRenderer.color = color;

            } 
            if (!hasSoundPlayed)
            {
                audioManager.PlaySFX(audioManager.phaseIn);
                hasSoundPlayed= true; 
            }


        }

        //Phase Cooldown
        if (!phaseAvail)
        {
            timer += Time.deltaTime;
            

            if (timer >= phaseCooldown)
            {
                phaseAvail = true;
                timer = 0f;
                hasSoundPlayed = false;
            }
        }
       

    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);

        // If player stopped moving
        if (context.canceled)
        {
            animator.SetBool("isWalking", false);

            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);

            // Start invulnerability
            if (phaseAvail)
            {
                isInvulnerable = true;
                invulnerabilityTimer = phaseDuration;

                phaseAvail = false;
                timer = 0f; // Start cooldown timer

                Color color = spriteRenderer.color;
                color.a = phaseAlpha;
                spriteRenderer.color = color;
                phaseAvail = false;
            }
        }

        moveInput = context.ReadValue<Vector2>();

        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }
}