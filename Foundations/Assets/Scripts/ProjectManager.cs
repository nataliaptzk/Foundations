using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProjectManager : MonoBehaviour
{
    public List<ProjectClass> _projects = new List<ProjectClass>();

    [SerializeField] private GridGenerator _gridGenerator;
    [SerializeField] private PlayerManager _playerManager;
    public int peopleAmount;

    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private GameObject _panelForButtons;
    [SerializeField] private GameObject _mainPanel; // this is used to pass it to assigncharactertotheproject script in buttons

    public PlayerManager Manager => _playerManager;

    public GameObject MainPanel => _mainPanel;

    private void Start()
    {
        CreateProjects();
    }

    private void CreateProjects()
    {
        _projects.Add(new ProjectClass("NewProject.Begin();", 100, 20, 1, RoomType.pc, CreateCharacter.Jobs.programmer));
        _projects.Add(new ProjectClass("Can't see sharp", 250, 25, 3, RoomType.pc, CreateCharacter.Jobs.programmer));
        _projects.Add(new ProjectClass("Income++", 500, 50, 5, RoomType.pc, CreateCharacter.Jobs.gameDesigner));
        _projects.Add(new ProjectClass("As green as grass", 100, 10, 1, RoomType.greenscreen, CreateCharacter.Jobs.actor));
        _projects.Add(new ProjectClass("e", 250, 20, 3, RoomType.greenscreen, CreateCharacter.Jobs.producer));
        _projects.Add(new ProjectClass("f", 500, 40, 5, RoomType.greenscreen, CreateCharacter.Jobs.producer));
        _projects.Add(new ProjectClass("Daily Stand Up", 100, 15, 1, RoomType.meeting, CreateCharacter.Jobs.manager));
        _projects.Add(new ProjectClass("Yet another sprint review", 250, 25, 3, RoomType.meeting, CreateCharacter.Jobs.projectLeader));
        _projects.Add(new ProjectClass("Retrospective time", 500, 35, 5, RoomType.meeting, CreateCharacter.Jobs.projectLeader));
        _projects.Add(new ProjectClass("Turn up the volume", 100, 20, 1, RoomType.audio, CreateCharacter.Jobs.Musician));
        _projects.Add(new ProjectClass("Noise Removal", 250, 25, 3, RoomType.audio, CreateCharacter.Jobs.Musician));
        _projects.Add(new ProjectClass("Auto-Tune it", 500, 40, 5, RoomType.audio, CreateCharacter.Jobs.soundTechnician));
        _projects.Add(new ProjectClass("m", 100, 30, 1, RoomType.workshop, CreateCharacter.Jobs.engineer));
        _projects.Add(new ProjectClass("n", 250, 45, 3, RoomType.workshop, CreateCharacter.Jobs.engineer));
        _projects.Add(new ProjectClass("o", 500, 60, 5, RoomType.workshop, CreateCharacter.Jobs.architect));
    }

    [ContextMenu("CheckAvailableProjects")]
    private void FindAvailableProjects()
    {
        List<ProjectClass> notInProgressProjects = _projects.Where(project => !project.inProgress).ToList();
        // Check for the rooms that are available: isAvailable == true
        // List<GridObject> availableRooms = _gridGenerator.grid_list.Where(room => room.).ToList();
        // Check for the characters that are available: isAvailable == true
        // List<CharacterClass> availableCharacters = _characterList.character_list.Where(character => character.GetComponent<CharacterClass>().isAvailable).ToList();


        foreach (var v in notInProgressProjects)
        {
            // check availavble rooms and then check which projects can be started
            // isavailable && not combined || isavailable && right combined

            // List<ProjectClass> availableProjects = _projects.Where(_projects => availableRooms.Contains(gameObject.GetComponent<GridObject>().grid_ui = v.RoomRequirement.GetType()) /*&& availableCharacters.Count<=v.PeopleRequirement*/).ToList();
        }
    }

    private void DisplayAvailableProjects(List<ProjectClass> availableProjects)
    {
        //  List<ProjectClass> availableProjects = _projects.Where(_projects => availableRooms.Contains(gameObject.GetComponent<GridObject>().grid_ui = v.RoomRequirement.GetType()) /*&& availableCharacters.Count<=v.PeopleRequirement*/).ToList();
    }

    public void StartProject(int indexProject)
    {
        _projects[indexProject].inProgress = true;
        // clone the charcter_panel_assignemnt
        // assign holderindex to indexproject

        // add timer here

        FinishProject(indexProject);
    }

    public void DisplayAvailableCharactersRequired(ProjectIndexHolder indexProject)
    {
        List<int> availablePlayersIndexes = new List<int>();
        for (int i = 0; i < Manager.players.Count; i++)
        {
            if (Manager.players[i].avaliableForWork && Manager.players[i].job == _projects[indexProject.projectIndexHolder].JobRequirement)
            {
                availablePlayersIndexes.Add(i);
            }
        }

        // List<Player> availableRequiredCharacters = _playerManager.players.Where(character => character.avaliableForWork && character.job.Equals(_projects[indexProject.projectIndexHolder].JobRequirement)).ToList();
        CreateButtons(availablePlayersIndexes);
    }


    public void DisplayAvailableCharactersGeneral()
    {
        List<int> availablePlayersIndexes = new List<int>();
        for (int i = 0; i < Manager.players.Count; i++)
        {
            if (Manager.players[i].avaliableForWork)
            {
                availablePlayersIndexes.Add(i);
            }
        }

        //List<Player> availableGeneralCharacters = _playerManager.players.Where(character => character.avaliableForWork).ToList();
        CreateButtons(availablePlayersIndexes);
    }


    // find available projects


    private void FinishProject(int indexProject)
    {
        _projects[indexProject].inProgress = false;

        // go through the project.currentppl and change their isavailable

        // will call the function to add the income
    }

    public void CancelProject(ProjectIndexHolder indexProject)
    {
        _projects[indexProject.projectIndexHolder].inProgress = false;
    }

    public void CreateButtons(List<int> availablePlayers)
    {
        // remove all current buttons
        foreach (Transform child in _panelForButtons.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var player in availablePlayers)
        {
            GameObject button = Instantiate(_buttonPrefab, _panelForButtons.transform, true);
            button.transform.localScale = new Vector3(1f, 1f, 1f);
            //  button.GetComponent<Image>().sprite = player.
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Manager.players[player].job.ToString(); //Changing text
            button.GetComponent<CharacterIndexHolder>().characterIndex = player;
        }
    }
}