using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    public List<GameObject> Grid;
    public float x_dist = 1.5f;
    public float y_dist = 1.5f;
    public int x_count = 10;
    public int y_count = 4;
    public ObjectPooler OP;
    public GameObject grid_UI_obj;

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
                grid_script.grid_x = x;
                grid_script.grid_y = y;
                grid_script.position = new Vector3(x * x_dist, y * y_dist, 0);
                grid_script.grid_ui = grid_UI_obj;
                grid_obj.transform.position = grid_script.position;
                grid_obj.transform.parent = transform;
                Grid.Add(grid_obj);
            }
        }
    }

}
