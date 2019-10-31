using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGenerator : MonoBehaviour
{

    public List<List<GameObject>> grid_list;
    public List<GridObject> builtRooms;
    public Button characterCreationButton;
    public float x_dist = 1.5f;
    public float y_dist = 1.5f;
    public int x_count = 10;
    public int y_count = 4;
    public ObjectPooler OP;
    public GameObject grid_UI_obj;
    public GameObject room_prefab;
    private float tick_rate = 0.1f; //used so that some functions arn't called every single frame
    private float current_tick = 0.0f;

    void Start()
    {
        GenerateGrid();
    }

    private void Update()
    {
        current_tick += 1 * Time.deltaTime;
        if(current_tick > tick_rate)
        {
            if (grid_list != null)
            {
                CheckEmpty();
                CheckDoubleRooms();
            }
            current_tick = 0.0f;
        }
    }

    private void GenerateGrid()
    {
        grid_list = new List<List<GameObject>>();
        for(int y = 0; y < y_count; y++)
        {
            List<GameObject> grid_row = new List<GameObject>();
            for(int x = 0; x < x_count; x++)
            {
                GameObject grid_obj = OP.GetPooledObject();
                GridObject grid_script = grid_obj.GetComponent<GridObject>();
                grid_script.grid_y = y;
                grid_script.grid_x = x;
                grid_script.grid_ui = grid_UI_obj;
                grid_obj.transform.position = new Vector3(transform.position.x + (x * x_dist), transform.position.y + (y * y_dist), 0);
                grid_obj.transform.parent = transform;
                grid_script.SetComponents();
                grid_script.SetRoomValues();
                grid_row.Add(grid_obj);
            }
            grid_list.Add(grid_row);
        }

        GameObject obj = grid_list[0][0];
        GridObject grid = obj.GetComponent<GridObject>();
        grid.type = RoomType.pc;
        grid.SetRoomValues();
        builtRooms.Add(grid);
    }

    public void CheckEmpty()
    {
        for(int y = 0; y < y_count; y++)
        {
            for(int x = 0; x < x_count; x++)
            {
                if(x > 0)
                {
                    CheckIfBuildable(y, x, y, x - 1);
                }
                if(x < (x_count - 1 ))
                {
                    CheckIfBuildable(y, x, y, x + 1);
                }
                if(y > 0)
                {
                    CheckIfBuildable(y, x, y - 1, x);
                }
                if(y < (y_count - 1))
                {
                    CheckIfBuildable(y, x, y + 1, x);
                }
            }
        }

    }

    public void CheckIfBuildable(int index_y, int index_x, int y_check, int x_check)
    {
        GridObject grid_obj = grid_list[index_y][index_x].GetComponent<GridObject>();
        if (grid_obj.type != RoomType.empty && grid_obj.type != RoomType.buildable)
        {
            GridObject obj1 = grid_list[y_check][x_check].GetComponent<GridObject>();
            if(obj1.type == RoomType.empty)
            {
                obj1.type = RoomType.buildable;
                obj1.SetRoomValues();
            }
        }
    }

    public void CheckDoubleRooms()
    {
        for(int y = 0; y < y_count; y++)
        {
            for(int x = 0; x < x_count; x++)
            {
                GridObject obj = grid_list[y][x].GetComponent<GridObject>();
                if(obj.combined_left == true || obj.combined_right == true || obj.type == RoomType.empty || obj.type == RoomType.buildable)
                {
                    continue;
                }
                bool combined = false;
                if(x > 0 && combined == false)
                {
                    GridObject checked_obj = grid_list[y][x - 1].GetComponent<GridObject>();
                    if (checked_obj.type == obj.type && checked_obj.combined_left == false)
                    {
                        obj.SetCombinedRoom(true);
                        combined = true;
                    }
                }
                if(x < x_count - 1 && combined == false)
                {
                    GridObject checked_obj = grid_list[y][x + 1].GetComponent<GridObject>();
                    if (checked_obj.type == obj.type && checked_obj.combined_right == false)
                    {
                        obj.SetCombinedRoom(false);
                        combined = true;
                    }
                }
            }
        }
    }

    public void AddNewRoom(RoomType room_type, int grid_y, int grid_x)
    {
        GameObject obj = grid_list[grid_y][grid_x];
        GridObject grid = obj.GetComponent<GridObject>();
        grid.type = room_type;
        grid.SetRoomValues();
        builtRooms.Add(grid);
        CheckForAvaliableCharacterCreation();
    }

    //checks to see if theres enough space for a character to be created
    public void CheckForAvaliableCharacterCreation()
    {
        int spareSpaces = 0;
        for (int i = 0; i < builtRooms.Count; i++)
        {
            if (builtRooms[i].current_occupants != builtRooms[i].max_occupants)
            {
                for (int j = builtRooms[i].current_occupants; j < builtRooms[i].max_occupants; j++)
                {
                    spareSpaces++;
                }
            }
        }


        if (spareSpaces == 0)
        {
            characterCreationButton.interactable = false;
            //blank out character creation till new room added
        }
        else
        {
            characterCreationButton.interactable = true;
        }
    }
}
