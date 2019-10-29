/* 
 * Room Structure Scrypt Version:   1.0
 * Contributors                 :   Stephen W
 * Last Edit                    :   25/10/19 16:27
 * ChangeLog                    :   No Changes
 * This code creates instances of the room structure
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    empty,
    buildable,
    pc,
    greenscreen,
    lounge,
    meeting,
    audio,
    workshop
}

public class RoomClass : MonoBehaviour
{
    public RoomType type;
    public int max_occupants;
    public int current_occupants;
    public Sprite sprite;

    public void SetRoomValues()
    {
        switch(type)
        {
            case RoomType.empty:
                sprite = null;
                max_occupants = 0;
                current_occupants = 0;
                break;
            case RoomType.buildable:
                sprite = null;
                max_occupants = 0;
                current_occupants = 0;
                break;
            default:
                sprite = SetSprite(type);
                max_occupants = 5;
                current_occupants = 0;
                break;
        }
    }

    public Sprite SetSprite(RoomType type)
    {
        Sprite sprite = null;
        switch(type)
        {
            case RoomType.pc:
                sprite = null;
                break;
            case RoomType.lounge:
                sprite = null;
                break;
            case RoomType.greenscreen:
                sprite = null;
                break;
            case RoomType.meeting:
                sprite = null;
                break;
            case RoomType.workshop:
                sprite = null;
                break;
            case RoomType.audio:
                sprite = null;
                break;
        }
        return sprite;
    }
}
