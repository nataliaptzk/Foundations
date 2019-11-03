using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    public GameObject Portal;
    public GameObject Player;
    float disableTimer = 0; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (disableTimer > 0)
        {
            disableTimer = disableTimer - Time.deltaTime;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && disableTimer <= 0)
        {
            disableTimer = 2;
            StartCoroutine(Transfer());
        }
    }

    IEnumerator Transfer()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        yield return new WaitForSeconds(3);
        Player.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y);
    }
}
