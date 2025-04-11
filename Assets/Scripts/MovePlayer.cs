using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float speed = 2.5F;
    [SerializeField]private float jump = 5F;

    private Animator anim;
    private Rigidbody2D body;

    private int jumpCount = 2;


    [SerializeField] private Camera cameraPos;
    [SerializeField] private float velocidad = 1 ;

    [Header ("Ground")]
    [SerializeField] private Transform checker;
    [SerializeField] private float radioChecker;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask ground;

    private Vector3 iniPosCam;
    private Vector3 iniPosPlay;

    private string state="live";

    private void Awake()
    {
        iniPosCam = cameraPos.transform.position;
        iniPosPlay = transform.position;

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

        if (Input.GetKeyDown(KeyCode.Space) && (jumpCount > 1 || isGrounded == true))
        {
            Jump();
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


        if (transform.position.y <= -6)
        {
            state = "dead";
            transform.position = new Vector3(transform.position.x,0 ,transform.position.z);
            GetComponent<Collider2D>().enabled = false;
        }

        if(state == "dead")
        {
            cameraPos.transform.position = Vector3.MoveTowards(cameraPos.transform.position, iniPosCam, Time.deltaTime * velocidad);
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, 0, transform.position.z), iniPosPlay, Time.deltaTime * velocidad); ;

        }
        if (Vector3.Distance(transform.position, iniPosPlay) < 0.01f)
        {
            state = "live";
            GetComponent<Collider2D>().enabled = true;
        }

        //if (horizontalInput != 0) anim.SetBool("walk", true); else anim.SetBool("walk", false);
        anim.SetBool("walk", horizontalInput != 0);
        anim.SetBool("idle", isGrounded);


    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jump);
        anim.SetTrigger("jump");
        jumpCount--;
        isGrounded = false;
    }



}