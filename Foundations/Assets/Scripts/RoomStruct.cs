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


public struct RoomStruct
{
    public string Type;
    public int Capacity;
    public int Occupents;
    public GameObject RoomPrefab;
    //public int Income;
    //public bool Upgradable;
}
