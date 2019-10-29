using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonObject : MonoBehaviour
{
    public int room_num;
    public RoomType type;
    private GridGenerator generator;
    public GameObject parent_UI;

    private void Start()
    {
        generator = GameObject.Find("GridGenerator").GetComponent<GridGenerator>();
        parent_UI = transform.parent.gameObject;
    }


    private void OnMouseDown()
    {
        Debug.Log("Yeet");
        generator.AddNewRoom(type, room_num);
        parent_UI.SetActive(false);
    }

}
