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
    private bool set_start_values = true;
    private float direction_speed = 0.0f;
    public bool transfering = false;
    public bool transfer_direction = false;

    //call this when you add a new person to the scene
    public void SetStartValues()
    {
        direction_speed = speed;
        grid_generator = GameObject.Find("GridGenerator").GetComponent<GridGenerator>();
        grid_list = grid_generator.grid_list;
        Invoke("SetRandomRoomTarget", 1.0F);
        SetCurrentTarget();
    }

    void Update()
    {
        if(set_start_values) //for testing
        {
            SetStartValues();
            set_start_values = false;
        }
        SetCurrentTarget();
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
        if (current_room && end_target)
        {
            if (current_room.grid_y != end_target.grid_y)
            {
                //if person isn't on same floor as end target, go to elevator
                current_target = grid_list[current_room.grid_y][0].GetComponent<GridObject>();
            }
            else
            {
                current_target = end_target;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
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
        if (current_room)
        {
            if (current_room.grid_y == grid_generator.built_rooms[rand_num].grid_y)
            {
                end_target = grid_generator.built_rooms[rand_num];
                reached_target = false;
            }
            else
            {
                SetRandomRoomTarget();
            }
        }
    }

    public void MovePerson()
    {
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
                Vector2 room_x = new Vector2(end_target.gameObject.transform.position.x, transform.position.y);
                if(Vector2.Distance(transform.position, room_x) < 0.2f)
                {
                    reached_target = true;
                    direction_speed = 0.0f;
                    Invoke("SetRandomRoomTarget", 3.0f);
                    return;
                }
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
