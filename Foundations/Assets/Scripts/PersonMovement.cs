using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public GridObject end_target;
    public GridObject current_target;
    public GridObject current_room;
    public List<List<GameObject>> grid_list;
    public bool reached_target = false;
    public bool using_elevator = false;
    public float tick_rate = 0.5f;
    private float current_tick = 0.0f;
    private GridGenerator grid_generator;
    public bool set_start_values = false;

    public bool transfering = false;
    public bool transfer_direction = false;

    //call this when you add a new person to the scene
    public void SetStartValues()
    {
        grid_generator = GameObject.Find("GridGenerator").GetComponent<GridGenerator>();
        grid_list = grid_generator.grid_list;
        current_room = grid_list[0][0].GetComponent<GridObject>();
        SetCurrentTarget();
    }

    void Update()
    {
        if(set_start_values) //for testing
        {
            SetStartValues();
            set_start_values = false;
        }

        if(!reached_target && current_target)
        {
            if(current_room)
            {
                MovePerson();
            }
        }
    }

    public void SetCurrentTarget()
    {
        if(current_room.grid_y != end_target.grid_y)
        {
            //if person isn't on same floor as end target, go to elevator
            current_target = grid_list[current_room.grid_y][0].GetComponent<GridObject>();
        }
        else
        {
            current_target = end_target;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<GridObject>())
        {
            current_room = other.GetComponent<GridObject>();
        }
    }

    //give random target if you want people walking around when not working
    public void SetRandomRoomTarget()
    {
        int rand_num = Random.Range(0, grid_generator.built_rooms.Count - 1);
        end_target = grid_generator.built_rooms[rand_num];
    }

    public void MovePerson()
    {
        float direction_speed = 0.0f;
        if(current_room.grid_x < current_target.grid_x)
        {
            direction_speed = speed;
        }
        else if(current_room.grid_x > current_target.grid_x)
        {
            direction_speed = -speed;
        }
        else
        {
            //reached current target, add them to this room/however they are used for projects
            if(current_room == end_target)
            {
                reached_target = true;
                return;
            }
            else
            {
                if(current_room.grid_y < current_target.grid_y)
                {
                    transfer_direction = true;
                    transfering = true;
                }
                else if(current_room.grid_y > current_target.grid_y)
                {
                    transfer_direction = false;
                    transfering = true;
                }
                else
                {
                    transfering = false;
                }
                return;
            }
        }
        transform.Translate(Vector3.right * direction_speed * Time.deltaTime);
    }
}
