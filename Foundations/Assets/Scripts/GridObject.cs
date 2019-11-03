using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum RoomType
{
    empty,
    buildable,
    pc,
    greenscreen,
    lounge,
    meeting,
    audio,
    workshop,
    elevator
}

public class GridObject : MonoBehaviour
{
    public RoomType type;
    public int max_occupants;
    public int current_occupants;
    public Sprite sprite;
    private SpriteRenderer sprite_renderer;
    public int grid_y;
    public int grid_x;
    public GameObject grid_ui;
    public bool combined_left = false;
    public bool combined_right = false;
    public bool isAvailable;
    public List<GameObject> doors;

    private string sprite_name;

    public void SetComponents()
    {
        sprite = null;
        sprite_renderer = GetComponent<SpriteRenderer>();
    }
    public void OnMouseDown()
    {
        if (type == RoomType.buildable)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                ObjectPooler OP = grid_ui.GetComponent<ObjectPooler>();
                GameObject UI = OP.GetPooledObject();
                foreach (Transform child in UI.transform)
                {
                    if (child.gameObject.GetComponent<ButtonObject>())
                    {
                        ButtonObject button = child.gameObject.GetComponent<ButtonObject>();
                        button.room_y = grid_y;
                        button.room_x = grid_x;
                    }
                }
                UI.transform.SetParent(grid_ui.transform, false);
                UI.transform.position = transform.position;
                UI.SetActive(true);
            }
        }
    }

    public void SetRoomValues()
    {
        switch (type)
        {
            case RoomType.empty:
                sprite = null;
                max_occupants = 0;
                current_occupants = 0;
                isAvailable = false;
                break;
            case RoomType.buildable:
                sprite = Resources.Load<Sprite>("Sprites/RoomSprites/buildable");
                max_occupants = 0;
                current_occupants = 0;
                isAvailable = false;
                break;
            default:
                sprite = SetSprite(type);
                max_occupants = 5;
                current_occupants = 0;
                isAvailable = true;
                break;
        }
        sprite_renderer.sprite = sprite;
    }

    public Sprite SetSprite(RoomType type)
    {
        Sprite sprite = null;
        switch (type)
        {
            case RoomType.pc:
                sprite = Resources.Load<Sprite>("Sprites/RoomSprites/pcroom");
                sprite_name = "pcroom";
                break;
            case RoomType.lounge:
                sprite = Resources.Load<Sprite>("Sprites/RoomSprites/lounge");
                sprite_name = "lounge";
                break;
            case RoomType.greenscreen:
                sprite = Resources.Load<Sprite>("Sprites/RoomSprites/greenscreen");
                sprite_name = "greenscreen";
                break;
            case RoomType.meeting:
                sprite = Resources.Load<Sprite>("Sprites/RoomSprites/meetingroom");
                sprite_name = "meetingroom";
                break;
            case RoomType.workshop:
                sprite = Resources.Load<Sprite>("Sprites/RoomSprites/workshop");
                sprite_name = "workshop";
                break;
            case RoomType.audio:
                sprite = Resources.Load<Sprite>("Sprites/RoomSprites/audio");
                sprite_name = "audio";
                break;
            case RoomType.elevator:
                sprite = Resources.Load<Sprite>("Sprites/RoomSprites/elevator");
                sprite_name = "elevator";
                break;
        }
        return (sprite);
    }

    public void SetCombinedRoom(bool side)
    {
        Sprite sprite = null;
        if(side)
        {
            sprite = Resources.Load<Sprite>("Sprites/RoomSprites/" + sprite_name + "right");
            combined_left = true;
        }
        else
        {
            sprite = Resources.Load<Sprite>("Sprites/RoomSprites/" + sprite_name + "left");
            combined_right = true;
        }
        sprite_renderer.sprite = sprite;
    }

    public void SetDoors(bool state, bool side)
    {
        if(side)
        {
            doors[0].SetActive(state);
        }
        else
        {
            doors[1].SetActive(state);
        }
    }
}
