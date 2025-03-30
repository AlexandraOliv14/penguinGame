using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float speed = 2.5F;
    [SerializeField]private float jump = 5F;

    private Animator anim;
    private Rigidbody2D body;

    private int jumpCount = 2;

    [SerializeField]
    public Vector3 iniPosition;

    [Header ("Ground")]
    [SerializeField] private Transform checker;
    [SerializeField] private float radioChecker;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask ground;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        if(horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        if (horizontalInput != 0) anim.SetBool("walk", true); else anim.SetBool("walk", false);

        if (Input.GetKeyDown(KeyCode.Space) && (jumpCount > 1 || isGrounded == true))
        {
            anim.SetBool("jump", true);
            body.linearVelocity = new Vector2(body.linearVelocity.x, jump);
            jumpCount--;
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("attack", true);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || anim.GetCurrentAnimatorStateInfo(0).IsName("penguin_jump") || anim.GetCurrentAnimatorStateInfo(0).IsName("penguin_atack"))
        {
            anim.SetBool("jump", false);
            anim.SetBool("attack", false);
        }

        if (isGrounded == true)
        {
            jumpCount = 2;
        }

        isGrounded = Physics2D.OverlapCircle(checker.position, radioChecker, ground);

        float state = transform.position.y;
        if (state <= -6)
        {
            transform.position = iniPosition;
        }


    }

}