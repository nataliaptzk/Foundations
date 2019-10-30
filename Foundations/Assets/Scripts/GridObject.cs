using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridObject : MonoBehaviour
{
    public RoomType type;
    public int max_occupants;
    public int current_occupants;
    public Sprite sprite;
    private SpriteRenderer sprite_renderer;
    public int grid_ID;
    public GameObject grid_ui;

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
                        child.gameObject.GetComponent<ButtonObject>().room_num = grid_ID;
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
                break;
            case RoomType.buildable:
                sprite = Resources.Load<Sprite>("RoomSprites/Buildable");
                max_occupants = 0;
                current_occupants = 0;
                break;
            default:
                sprite = SetSprite(type);
                max_occupants = 5;
                current_occupants = 0;
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
                sprite = Resources.Load<Sprite>("RoomSprites/PCRoom");
                break;
            case RoomType.lounge:
                sprite = Resources.Load<Sprite>("RoomSprites/lounge");
                break;
            case RoomType.greenscreen:
                sprite = Resources.Load<Sprite>("RoomSprites/Greenscreen");
                break;
            case RoomType.meeting:
                sprite = Resources.Load<Sprite>("RoomSprites/meetingroom");
                break;
            case RoomType.workshop:
                sprite = Resources.Load<Sprite>("RoomSprites/workshop");
                break;
            case RoomType.audio:
                sprite = Resources.Load<Sprite>("RoomSprites/audio");
                break;
        }
        return sprite;
    }
}
