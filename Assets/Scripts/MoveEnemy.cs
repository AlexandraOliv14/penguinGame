using UnityEngine;

public class MoveEnemy : MonoBehaviour
{

    [SerializeField] private GameObject iniPose;
    [SerializeField] private GameObject finPos;

    [SerializeField] private float velocidad;

    private bool moveRight = true;

    private void Update()
    {
        if (Vector3.Distance(transform.position, finPos.transform.position) < 0.01f)
        {
            moveRight = false;
            gameObject.transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f);

        }
        else if (Vector3.Distance(transform.position, iniPose.transform.position) < 0.01f)
        {
            moveRight = true;
            gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        }

        if (moveRight)  MoveRight();
        else MoveLeft();

    }

    private void MoveRight()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, finPos.transform.position, Time.deltaTime * velocidad);
    }

    private void MoveLeft()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, iniPose.transform.position, Time.deltaTime * velocidad);
    }

}
