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

    public List<Player> _currentPeople;
    public bool inProgress;

    public ProjectClass(string title, int income, float duration, int peopleRequirement, RoomType roomRequirement)
    {
        _title = title;
        _income = income;
        _duration = duration;
        _peopleRequirement = peopleRequirement;
        _roomRequirement = roomRequirement;
        inProgress = false;
        _currentPeople = new List<Player>();
    }

    public int Income => _income;
    public string Title => _title;
    public float Duration => _duration;
    public int PeopleRequirement => _peopleRequirement;
    public RoomType RoomRequirement => _roomRequirement;
}