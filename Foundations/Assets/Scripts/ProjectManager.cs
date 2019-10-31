using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    public List<ProjectClass> _projects = new List<ProjectClass>();

    [SerializeField] private GridGenerator _gridGenerator;
    [SerializeField] private PlayerManager _playerManager;
    public int peopleAmount;

    private void Start()
    {
        _projects.Add(new ProjectClass("NewProject.Begin();", 100, 20, 1, RoomType.pc));
        _projects.Add(new ProjectClass("Can't see sharp", 250, 25, 3, RoomType.pc));
        _projects.Add(new ProjectClass("Income++", 500, 50, 5, RoomType.pc));
        _projects.Add(new ProjectClass("As green as grass", 100, 10, 1, RoomType.greenscreen));
        _projects.Add(new ProjectClass("e", 250, 20, 3, RoomType.greenscreen));
        _projects.Add(new ProjectClass("f", 500, 40, 5, RoomType.greenscreen));
        _projects.Add(new ProjectClass("Daily Stand Up", 100, 15, 1, RoomType.meeting));
        _projects.Add(new ProjectClass("Yet another sprint review", 250, 25, 3, RoomType.meeting));
        _projects.Add(new ProjectClass("Retrospective time", 500, 35, 5, RoomType.meeting));
        _projects.Add(new ProjectClass("Turn up the volume", 100, 20, 1, RoomType.audio));
        _projects.Add(new ProjectClass("Noise Removal", 250, 25, 3, RoomType.audio));
        _projects.Add(new ProjectClass("Auto-Tune it", 500, 40, 5, RoomType.audio));
        _projects.Add(new ProjectClass("m", 100, 30, 1, RoomType.workshop));
        _projects.Add(new ProjectClass("n", 250, 45, 3, RoomType.workshop));
        _projects.Add(new ProjectClass("o", 500, 60, 5, RoomType.workshop));
    }


    // find available projects
    private void StartProject(int index)
    {
         _projects[index].inProgress = true;
        // add current people

        // add timer here

        FinishProject(index);
    }



    [ContextMenu("CheckAvailableProjects")]
    private void CheckAvailableProjects()
    {
        List<ProjectClass> notInProgressProjects = _projects.Where(project => project.inProgress).ToList();
        // Check for the rooms that are available: isAvailable == true
        // List<GridObject> availableRooms = _gridGenerator.grid_list.Where(room => room.).ToList();
        // Check for the characters that are available: isAvailable == true
        // List<CharacterClass> availableCharacters = _characterList.character_list.Where(character => character.GetComponent<CharacterClass>().isAvailable).ToList();


        foreach (var v in notInProgressProjects)
        {
            // List<ProjectClass> availableProjects = _projects.Where(_projects => availableRooms.Contains(gameObject.GetComponent<GridObject>().grid_ui = v.RoomRequirement.GetType()) /*&& availableCharacters.Count<=v.PeopleRequirement*/).ToList();
        }
    }


    private void DisplayAvailableProjects()
    {
     // check availavble rooms and then check which projects can be started
     // isavailable && not combined || isavailable && right combined


        //  List<ProjectClass> availableProjects = _projects.Where(_projects => availableRooms.Contains(gameObject.GetComponent<GridObject>().grid_ui = v.RoomRequirement.GetType()) /*&& availableCharacters.Count<=v.PeopleRequirement*/).ToList();
    }

    private void AssignCharacterToTheProject(int projectIndex)
    {
        // check requirements of people
        // open the ui window, click to choose person, remove person from available, fill in all slots

        List<Player> availablePlayers = _playerManager.players.Where(player => player.avaliableForWork).ToList(); // stores all available players for the project
        // display the players
        // click on player to assign
        // isavailable false
        // add to the list project.currentppl
    }

    private void FinishProject(int index)
    {
        _projects[index].inProgress = false;
        // go through the project.currentppl and change their isavailable

// will call the function to add the income
    }
}