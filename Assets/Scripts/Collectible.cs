using TMPro;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    [SerializeField]
    private TMP_Text text;

    int count = 0;

    [SerializeField]
    private AudioSource audioS;


    [SerializeField]
    private AudioClip clip;


    private void Awake()
    {
        count = int.Parse(text.text);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "coins")
        {
            count = count + 1;
            text.text = count.ToString();
            audioS.clip = clip;
            audioS.Play();

            Destroy(collision.gameObject);
        }
    }
}
