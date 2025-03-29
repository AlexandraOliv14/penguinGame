using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float move = 2.5F;

    [SerializeField]
    private float jump = 5F;

    private Animator anim;
    private Rigidbody2D body;

    private int jumpCount = 2;

    [SerializeField]
    private Transform checker;

    [SerializeField]
    private float radioChecker;

    [SerializeField]
    private bool isGrounded;

    [SerializeField]
    private LayerMask ground;

    [SerializeField]
    public Vector3 iniPosition;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        if(Input.GetKey(KeyCode.RightArrow) )
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }

            if(isGrounded == true ) animationState("walk");// que no se sobrescriba el salto

            body.linearVelocity = new Vector2(+move, body.linearVelocity.y);
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }

            if (isGrounded == true ) animationState("walk");

            body.linearVelocity = new Vector2(-move, body.linearVelocity.y);

        }

        if (Input.GetKeyDown(KeyCode.Space) && (jumpCount > 1 || isGrounded == true))
        {
            animationState("jump");
            body.linearVelocity = new Vector2(body.linearVelocity.x, +jump);
            jumpCount--;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            animationState("attack");
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || anim.GetCurrentAnimatorStateInfo(0).IsName("penguin_jump") || anim.GetCurrentAnimatorStateInfo(0).IsName("penguin_atack"))
        {
            animationState("idle");
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

    private void animationState(string name)
    {
        switch (name)
        {

            case "walk":
                anim.SetBool("idle", false);
                anim.SetBool("jump", false);
                anim.SetBool("attack", false);
                anim.SetBool("walk", true);
                break;

            case "jump":
                anim.SetBool("walk", false);
                anim.SetBool("idle", false);
                anim.SetBool("attack", false);
                anim.SetBool("jump", true);
                break;

            case "attack":
                anim.SetBool("walk", false);
                anim.SetBool("idle", false);
                anim.SetBool("jump", false);
                anim.SetBool("attack", true);
                break;

            case "idle":
                anim.SetBool("walk", false);
                anim.SetBool("jump", false);
                anim.SetBool("attack", false);
                anim.SetBool("idle", true);
                break;

            default:
                break;
        }

    }
}