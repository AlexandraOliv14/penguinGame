using TMPro;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    [SerializeField]
    private TMP_Text text;

    int count = 0;


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
            Destroy(collision.gameObject);
        }
    }
}
