/* 
 * Room Manager Scrypt Version  :   1.0
 * Contributors                 :   Stephen W
 * Last Edit                    :   25/10/19 16:27
 * ChangeLog                    :   No Changes
 * This code controlls the creation and spawning of new rooms
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //Creates a List of all Structures Created
    public List<RoomClass> Rooms = new List<RoomClass>();

    //Intigers to count count the number of references of each type of room
    private int PCRoomNum = 0;
    private int LoungeNum = 0;
    private int MeetingRoomNum = 0;
    private int GreenScreenNum = 0;
    private int AudioLabNum = 0;
    private int WorkshopNum = 0;

    //instanciates all prefabs in the editor
    public GameObject room_prefab;
    public GameObject PCRoomPrefab;
    public GameObject LoungePrefab;
    public GameObject MeetingRoomPrefab;
    public GameObject GreenScreenPrefab;
    public GameObject AudioLabPrefab;
    public GameObject WorkshopPrefab;


    public void AddNewRoom(RoomType room_type, int grid_num)
    {
        GameObject obj = Instantiate(room_prefab) as GameObject;
        RoomClass new_room = obj.GetComponent<RoomClass>();
        new_room.type = room_type;
    }

    ////creates a new instance of the PCROOM, spawns it, adds it to the list, then increments the room count
    //public void AddPCRoom() 
    //{
    //    var PCRoom = new RoomClass();
    //    PCRoom.Type = "PCRoom" + PCRoomNum;
    //    PCRoom.Capacity = 5;
    //    PCRoom.Occupents = 0;
    //    PCRoom.room_prefab = PCRoomPrefab;
    //    Instantiate(PCRoom.room_prefab, new Vector3(PCRoomNum, -1, 0), Quaternion.identity);
    //    Rooms.Add(PCRoom);
    //    PCRoomNum++;
    //    Debug.Log("Added PC Room");
    //}

    ////creates a new instance of the LOUNGE, spawns it, adds it to the list, then increments the room count
    //public void AddLounge() 
    //{
    //    var Lounge = new RoomClass();
    //    Lounge.Type = "Lounge" + LoungeNum;
    //    Lounge.Capacity = 5;
    //    Lounge.Occupents = 0;
    //    Lounge.room_prefab = LoungePrefab;
    //    Instantiate(Lounge.room_prefab, new Vector3(LoungeNum, 1, 0), Quaternion.identity);
    //    Rooms.Add(Lounge);
    //    LoungeNum++;
    //    Debug.Log("Added Lounge");
    //}

    ////creates a new instance of the MEETINGROOM, spawns it, adds it to the list, then increments the room count
    //public void AddMeetingRoom() 
    //{
    //    var MeetingRoom = new RoomClass();
    //    MeetingRoom.Type = "MeetingRoom" + MeetingRoomNum;
    //    MeetingRoom.Capacity = 5;
    //    MeetingRoom.Occupents = 0;
    //    MeetingRoom.room_prefab = MeetingRoomPrefab;
    //    Instantiate(MeetingRoom.room_prefab, new Vector3(MeetingRoomNum, 0, 0), Quaternion.identity);
    //    Rooms.Add(MeetingRoom);
    //    MeetingRoomNum++;
    //    Debug.Log("Added Meeting Room");
    //}

    ////creates a new instance of the GREENSCREEN, spawns it, adds it to the list, then increments the room count
    //public void AddGreenScreen() 
    //{
    //    var GreenScreen = new RoomClass();
    //    GreenScreen.Type = "GreenScreen" + GreenScreenNum;
    //    GreenScreen.Capacity = 5;
    //    GreenScreen.Occupents = 0;
    //    GreenScreen.room_prefab = GreenScreenPrefab;
    //    Instantiate(GreenScreen.room_prefab, new Vector3(GreenScreenNum, 2, 0), Quaternion.identity);
    //    Rooms.Add(GreenScreen);
    //    GreenScreenNum++;
    //    Debug.Log("Added Green Screen");
    //}

    ////creates a new instance of the AUDIOLAB, spawns it, adds it to the list, then increments the room count
    //public void AddAudioLab() 
    //{
    //    var AudioLab = new RoomClass();
    //    AudioLab.Type = "AudioLab" + AudioLabNum;
    //    AudioLab.Capacity = 5;
    //    AudioLab.Occupents = 0;
    //    AudioLab.room_prefab = AudioLabPrefab;
    //    Instantiate(AudioLab.room_prefab, new Vector3(AudioLabNum, 3, 0), Quaternion.identity);
    //    Rooms.Add(AudioLab);
    //    AudioLabNum++;
    //    Debug.Log("Added Audio Lab");
    //}

    ////creates a new instance of the WORKSHOP, spawns it, adds it to the list, then increments the room count
    //public void AddWorkshop() 
    //{
    //    var Workshop = new RoomClass();
    //    Workshop.Type = "Workshop" + WorkshopNum;
    //    Workshop.Capacity = 5;
    //    Workshop.Occupents = 0;
    //    Workshop.room_prefab = WorkshopPrefab;
    //    Instantiate(Workshop.room_prefab, new Vector3(WorkshopNum, -2, 0), Quaternion.identity);
    //    Rooms.Add(Workshop);
    //    WorkshopNum++;
    //    Debug.Log("Added Workshop");
    //}

    public void PrintList()
    {
        int i = Rooms.Count;
        Debug.Log("the list contains " + i + " Members");
    }
}
