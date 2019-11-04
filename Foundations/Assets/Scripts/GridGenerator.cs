using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGenerator : MonoBehaviour
{

    public List<List<GameObject>> grid_list;
    public List<GridObject> built_rooms;
    public PlayerManager playerManager;
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
                CheckDoors();
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
        obj.AddComponent<Elevator>();
        GridObject grid = obj.GetComponent<GridObject>();
        grid.type = RoomType.elevator;
        grid.SetRoomValues();
       // built_rooms.Add(grid);

        GameObject obj1 = grid_list[1][0];
        obj1.AddComponent<Elevator>();
        GridObject grid1 = obj1.GetComponent<GridObject>();
        grid1.type = RoomType.elevator;
        grid1.SetRoomValues();
      //  built_rooms.Add(grid1);

        GameObject obj2 = grid_list[2][0];
        obj2.AddComponent<Elevator>();
        GridObject grid2 = obj2.GetComponent<GridObject>();
        grid2.type = RoomType.elevator;
        grid2.SetRoomValues();
      //  built_rooms.Add(grid2);

        GameObject obj3 = grid_list[0][1];
        GridObject grid3 = obj3.GetComponent<GridObject>();
        grid3.type = RoomType.pc;
        grid3.SetRoomValues();
        built_rooms.Add(grid3);

        obj.GetComponent<Elevator>().AssignPortal(true, obj1);
        obj1.GetComponent<Elevator>().AssignPortal(true, obj2);
        obj1.GetComponent<Elevator>().AssignPortal(false, obj);
        obj2.GetComponent<Elevator>().AssignPortal(false, obj1);
        
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

    public void CheckDoors()
    {
        for (int y = 0; y < y_count; y++)
        {
            for (int x = 0; x < x_count; x++)
            {
                GridObject obj = grid_list[y][x].GetComponent<GridObject>();
                if (obj.type == RoomType.empty || obj.type == RoomType.buildable)
                {
                    continue;
                }

                //first on left
                if(x == 0)
                {
                    obj.SetDoors(true, true);
                    CheckRightRoomDoors(y, x, obj);
                    continue;
                }
                //first on right
                if(x == x_count - 1)
                {
                    obj.SetDoors(true, false);
                    CheckLeftRoomDoors(y, x, obj);
                    continue;
                }
                //inbetween
                CheckLeftRoomDoors(y, x, obj);
                CheckRightRoomDoors(y, x, obj);
            }
        }
    }

    public void CheckLeftRoomDoors(int grid_y, int grid_x, GridObject obj)
    {
        if (CheckRoomIsBuilt(grid_y, grid_x - 1))
        {
            obj.SetDoors(false, true);
        }
        else
        {
            obj.SetDoors(true, true);
        }
    }

    public void CheckRightRoomDoors(int grid_y, int grid_x, GridObject obj)
    {
        if (CheckRoomIsBuilt(grid_y, grid_x + 1))
        {
            obj.SetDoors(false, false);
        }
        else
        {
            obj.SetDoors(true, false);
        }
    }

    public bool CheckRoomIsBuilt(int grid_y, int grid_x)
    {
        bool built = false;
        GridObject checked_obj = grid_list[grid_y][grid_x].GetComponent<GridObject>();
        if (checked_obj.type != RoomType.empty && checked_obj.type != RoomType.buildable)
        {
            built = true;
        }
        return built;
    }

    public void AddNewRoom(RoomType room_type, int grid_y, int grid_x)
    {
        GameObject obj = grid_list[grid_y][grid_x];
        GridObject grid = obj.GetComponent<GridObject>();
        grid.type = room_type;
        grid.SetRoomValues();
        built_rooms.Add(grid);
        CheckForAvaliableCharacterCreation();
        playerManager.SetCharactersText();
    }

    //checks to see if theres enough space for a character to be created
    public int CheckForAvaliableCharacterCreation()
    {
        int spareSpaces = 0;
        for (int i = 0; i < built_rooms.Count; i++)
        {
            if (built_rooms[i].current_occupants != built_rooms[i].max_occupants)
            {
                for (int j = built_rooms[i].current_occupants; j < built_rooms[i].max_occupants; j++)
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
        return spareSpaces;
    }
}
