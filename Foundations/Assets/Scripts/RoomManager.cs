using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    public List<RoomStruct> Rooms = new List<RoomStruct>();

    private int PCRoomNum = 0;
    private int LoungeNum = 0;
    private int MeetingRoomNum = 0;
    private int GreenScreenNum = 0;
    private int AudioLabNum = 0;
    private int WorkshopNum = 0;

    public GameObject PCRoomPrefab;
    public GameObject LoungePrefab;
    public GameObject MeetingRoomPrefab;
    public GameObject GreenScreenPrefab;
    public GameObject AudioLabPrefab;
    public GameObject WorkshopPrefab;

    public void AddPCRoom() 
    {
        var PCRoom = new RoomStruct();
        PCRoom.Type = "PCRoom" + PCRoomNum;
        PCRoom.Capacity = 5;
        PCRoom.Occupents = 0;
        PCRoom.RoomPrefab = PCRoomPrefab;
        Instantiate(PCRoom.RoomPrefab, new Vector3(PCRoomNum, -1, 0), Quaternion.identity);
        Rooms.Add(PCRoom);
        PCRoomNum++;
        Debug.Log("Added PC Room");
    }

    public void AddLounge() 
    {
        var Lounge = new RoomStruct();
        Lounge.Type = "Lounge" + LoungeNum;
        Lounge.Capacity = 5;
        Lounge.Occupents = 0;
        Lounge.RoomPrefab = LoungePrefab;
        Instantiate(Lounge.RoomPrefab, new Vector3(LoungeNum, 1, 0), Quaternion.identity);
        Rooms.Add(Lounge);
        LoungeNum++;
        Debug.Log("Added Lounge");
    }
    public void AddMeetingRoom() 
    {
        var MeetingRoom = new RoomStruct();
        MeetingRoom.Type = "MeetingRoom" + MeetingRoomNum;
        MeetingRoom.Capacity = 5;
        MeetingRoom.Occupents = 0;
        MeetingRoom.RoomPrefab = MeetingRoomPrefab;
        Instantiate(MeetingRoom.RoomPrefab, new Vector3(MeetingRoomNum, 0, 0), Quaternion.identity);
        Rooms.Add(MeetingRoom);
        MeetingRoomNum++;
        Debug.Log("Added Meeting Room");
    }

    public void AddGreenScreen() 
    {
        var GreenScreen = new RoomStruct();
        GreenScreen.Type = "GreenScreen" + GreenScreenNum;
        GreenScreen.Capacity = 5;
        GreenScreen.Occupents = 0;
        GreenScreen.RoomPrefab = GreenScreenPrefab;
        Instantiate(GreenScreen.RoomPrefab, new Vector3(GreenScreenNum, 2, 0), Quaternion.identity);
        Rooms.Add(GreenScreen);
        GreenScreenNum++;
        Debug.Log("Added Green Screen");
    }

    public void AddAudioLab() 
    {
        var AudioLab = new RoomStruct();
        AudioLab.Type = "AudioLab" + AudioLabNum;
        AudioLab.Capacity = 5;
        AudioLab.Occupents = 0;
        AudioLab.RoomPrefab = AudioLabPrefab;
        Instantiate(AudioLab.RoomPrefab, new Vector3(AudioLabNum, 3, 0), Quaternion.identity);
        Rooms.Add(AudioLab);
        AudioLabNum++;
        Debug.Log("Added Audio Lab");
    }

    public void AddWorkshop() 
    {
        var Workshop = new RoomStruct();
        Workshop.Type = "Workshop" + WorkshopNum;
        Workshop.Capacity = 5;
        Workshop.Occupents = 0;
        Workshop.RoomPrefab = WorkshopPrefab;
        Instantiate(Workshop.RoomPrefab, new Vector3(WorkshopNum, -2, 0), Quaternion.identity);
        Rooms.Add(Workshop);
        WorkshopNum++;
        Debug.Log("Added Workshop");
    }

    public void PrintList()
    {
        int i = Rooms.Count;
        Debug.Log("the list contains " + i + " Members");
    }

    private void CreateRoom(Vector3 pos)
    {
        
    }
}
