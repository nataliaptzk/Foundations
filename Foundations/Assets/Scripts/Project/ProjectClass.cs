using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectClass
{
    [SerializeField] private string _title;
    [SerializeField] private int _income;
    [SerializeField] private float _duration;
    [SerializeField] private int _peopleRequirement;
    [SerializeField] private RoomType _roomRequirement;
    [SerializeField] private CreateCharacter.Jobs _jobRequirement;

  //  public int locX, locY;
    public List<int> _currentPeople;
    public Vector2Int assignedRoom;
    public bool inProgress;
    public bool isStarted;

    public ProjectClass(string title, int income, float duration, int peopleRequirement, RoomType roomRequirement, CreateCharacter.Jobs jobRequirement)
    {
        _title = title;
        _income = income;
        _duration = duration;
        _peopleRequirement = peopleRequirement;
        _roomRequirement = roomRequirement;
        _jobRequirement = jobRequirement;

        inProgress = false;
        isStarted = false;
        _currentPeople = new List<int>();
    }

    public int Income => _income;
    public string Title => _title;
    public float Duration => _duration;
    public int PeopleRequirement => _peopleRequirement;
    public RoomType RoomRequirement => _roomRequirement;
    public CreateCharacter.Jobs JobRequirement => _jobRequirement;
}