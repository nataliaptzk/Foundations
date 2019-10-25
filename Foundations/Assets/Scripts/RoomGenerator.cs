using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{

    public List<GameObject> rooms;
    public float room_distance = 1.5f;
    public int x_count = 10;
    public int y_count = 4;
    public ObjectPooler OP;

    void Start()
    {
        GenerateRooms();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateRooms()
    {
        for(int y = 0; y < y_count; y++)
        {
            for(int x = 0; x < x_count; x++)
            {
                GameObject room_obj = OP.GetPooledObject();
                Room room = room_obj.GetComponent<Room>();
                room.grid_x = x;
                room.grid_y = y;
                room.position = new Vector3(x * room_distance, y * room_distance, 0);
                room_obj.transform.position = room.position;
                room_obj.transform.parent = transform;
                rooms.Add(room_obj);
            }
        }
    }
}
