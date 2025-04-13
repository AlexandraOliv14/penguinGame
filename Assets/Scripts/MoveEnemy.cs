using System.Collections;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{

    [SerializeField] private GameObject iniPose;
    [SerializeField] private GameObject finPos;

    [SerializeField] private float velocidad;

    private Animator anim;

    private bool moveRight = true;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        StartCoroutine(MoveLoop());
    }

    private IEnumerator MoveLoop()
    {
        while (true)
        {
            Vector3 targetPos = moveRight ? finPos.transform.position : iniPose.transform.position;

            anim.SetBool("walk", true);

            // Ajustar orientación
            float scaleX = moveRight ? 0.6f : -0.6f;
            transform.localScale = new Vector3(scaleX, 0.6f, 0.6f);

            // Caminar hasta el extremo
            while (Vector3.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, velocidad * Time.deltaTime);
                yield return null;
            }

            // Detener movimiento
            anim.SetBool("walk", false);

            // Atacar
            anim.SetTrigger("attack");

            // Esperar a que comience la animación
            while (!anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
                yield return null;

            // Esperar a que termine la animación
            while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                yield return null;

            moveRight = !moveRight;
        }
    }

}
