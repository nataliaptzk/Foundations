﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGenerator : MonoBehaviour
{

    public List<GameObject> grid_list;
    public List<GridObject> builtRooms;
    public Button characterCreationButton;
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

    private void Update()
    {
        if(grid_list != null)
        {
            CheckEmpty();
        }
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
                grid_obj.transform.position = new Vector3(transform.position.x + (x * x_dist), transform.position.y + (y * y_dist), 0);
                grid_obj.transform.parent = transform;
                grid_script.SetComponents();
                grid_script.SetRoomValues();
                grid_list.Add(grid_obj);
            }
        }

        GameObject obj = grid_list[0];
        GridObject grid = obj.GetComponent<GridObject>();
        grid.type = RoomType.pc;
        grid.SetRoomValues();
        builtRooms.Add(grid);
    }

    public void CheckEmpty()
    {
        for(int i = 0; i < grid_list.Count; i++)
        {
            if(i - 1 > 0)
            {
                CheckIfBuildable(i, i - 1);
            }
            if (i + 1 < grid_list.Count - 1)
            {
                CheckIfBuildable(i, i + 1);
            }
            if(i - x_count > 0)
            {
                CheckIfBuildable(i, i - x_count);
            }
            if (i + x_count < grid_list.Count)
            {
                CheckIfBuildable(i, i + x_count);
            }
        }

    }

    public void CheckIfBuildable(int index, int index_to_check)
    {
        GridObject grid_obj = grid_list[index].GetComponent<GridObject>();
        if (grid_obj.type != RoomType.empty && grid_obj.type != RoomType.buildable)
        {
            GridObject obj1 = grid_list[index_to_check].GetComponent<GridObject>();
            if(obj1.type == RoomType.empty)
            {
                obj1.type = RoomType.buildable;
                obj1.SetRoomValues();
            }
        }
    }

    public void AddNewRoom(RoomType room_type, int grid_num)
    {
        GameObject obj = grid_list[grid_num];
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

        Debug.Log("spare Spaces" + spareSpaces);

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
