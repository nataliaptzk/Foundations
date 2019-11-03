using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBehaviour : MonoBehaviour
{
    public GameObject speechBubble;
    public SpriteRenderer iconSprite;
    public Sprite[] icons = new Sprite[4];

    float timer = 15;
    float time = 0;
    bool startTimer = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startTimer)
        {
            time += Time.deltaTime;

            if(time > timer)
            {
                speechBubble.SetActive(false);
                time = 0;
                startTimer = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            bool randomchance = (Random.value > 0.5f);

            if(randomchance && startTimer!= true)
            {
                speechBubble.SetActive(true);

                int randomnumber = Random.Range(0, 3);
                iconSprite.sprite = icons[randomnumber];
                startTimer = true;

            }
        }
    }
}
