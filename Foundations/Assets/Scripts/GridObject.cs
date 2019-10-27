using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridObject : MonoBehaviour
{
    //positions of the grid in roomgenerator
    public Vector3 position;
    public int grid_x;
    public int grid_y;
    public GameObject grid_ui;

    public void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log(grid_y + " " + grid_x);
            ObjectPooler OP = grid_ui.GetComponent<ObjectPooler>();
            GameObject UI = OP.GetPooledObject();
            UI.transform.SetParent(grid_ui.transform, false);
            UI.transform.position = transform.position;
            UI.SetActive(true);
        }
    }
}
