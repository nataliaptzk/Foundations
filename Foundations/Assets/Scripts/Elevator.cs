using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    public GameObject Portal;
    public GameObject Portal2;
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
            disableTimer = 4;
            PersonMovement PM = other.gameObject.GetComponent<PersonMovement>();
            if(!PM.transfer_direction && PM.transfering)
            {
                TransferUP(other.gameObject);
            }
            else
            {
                TransferDown(other.gameObject);
            }
        }
    }

    IEnumerator TransferUP(GameObject player)
    {
        yield return new WaitForSeconds(1);
        player.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y);
    }

    IEnumerator TransferDown(GameObject player)
    {
        yield return new WaitForSeconds(1);
        player.transform.position = new Vector2(Portal2.transform.position.x, Portal2.transform.position.y);
    }

    public void AssignPortal(bool up, GameObject obj)
    {
        if(up)
        {
            Portal = obj;
        }
        else
        {
            Portal2 = obj;
        }
    }

}
