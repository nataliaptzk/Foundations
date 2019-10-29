using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    public List<GameObject> grid_list;
    public float x_dist = 1.5f;
    public float y_dist = 1.5f;
    public int x_count = 10;
    public int y_count = 4;
    public ObjectPooler OP;
    public GameObject grid_UI_obj;
    public GameObject room_prefab;

    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for(int y = 0; y < y_count; y++)
        {
            for(int x = 0; x < x_count; x++)
            {
                GameObject grid_obj = OP.GetPooledObject();
                GridObject grid_script = grid_obj.GetComponent<GridObject>();
                grid_script.grid_ID = (y * x_count) + x;
                grid_script.grid_ui = grid_UI_obj;
                grid_obj.transform.position = new Vector3(x * x_dist, y * y_dist, 0);
                grid_obj.transform.parent = transform;
                grid_script.SetComponents();
                grid_script.SetRoomValues();
                grid_list.Add(grid_obj);
            }
        }
    }

    public void AddNewRoom(RoomType room_type, int grid_num)
    {
        GameObject obj = grid_list[grid_num];
        GridObject grid = obj.GetComponent<GridObject>();
        grid.type = room_type;
        grid.SetRoomValues();
    }
}
