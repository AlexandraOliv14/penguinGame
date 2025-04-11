using TMPro;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private AudioClip soundCoins;

    int count = 0;

    private void Awake()
    {
        count = int.Parse(text.text);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "coins")
        {
            count = count + 1;
            text.text = count.ToString();

            SoundManager.instance.PlaySound(soundCoins);

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "shark")
        {
            //PausarJuego();
        }
    }

    private void PausarJuego()
    {
        Time.timeScale = 0f;
    }

}
