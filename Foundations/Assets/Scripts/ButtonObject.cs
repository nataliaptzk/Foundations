using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonObject : MonoBehaviour
{
    public int room_y;
    public int room_x;
    public RoomType type;
    private GridGenerator generator;
    private ProjectManager projectManager;
    public GameObject parent_UI;

    private void Start()
    {
        generator = GameObject.Find("GridGenerator").GetComponent<GridGenerator>();
        projectManager = GameObject.Find("ProjectManager").GetComponent<ProjectManager>();
        parent_UI = transform.parent.gameObject;
    }


    private void OnMouseDown()
    {
        generator.AddNewRoom(type, room_y, room_x);
        projectManager.FindAvailableProjects();
        parent_UI.SetActive(false);
    }

}
