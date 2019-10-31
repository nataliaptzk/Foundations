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
    public PlayerManager _playerManager;

    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private GameObject _panelForButtons;
    [SerializeField] private GameObject _mainPanel;

    public GameObject _lastPressed;

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
        // find rooms
        // then check which projects can be done
        // display the projects on the UI so the user can choose the project to start
        List<Vector2Int> availableRoomsCoordinates = new List<Vector2Int>();

        for (int i = 0; i < _gridGenerator.grid_list.Count; i++)
        {
            for (int j = 0; j < _gridGenerator.grid_list[i].Count; j++)
            {
                if ((_gridGenerator.grid_list[i][j].GetComponent<GridObject>().isAvailable && !_gridGenerator.grid_list[i][j].GetComponent<GridObject>().combined_left &&
                     !_gridGenerator.grid_list[i][j].GetComponent<GridObject>().combined_right) ||
                    (_gridGenerator.grid_list[i][j].GetComponent<GridObject>().isAvailable && _gridGenerator.grid_list[i][j].GetComponent<GridObject>().combined_right &&
                     !_gridGenerator.grid_list[i][j].GetComponent<GridObject>().combined_left))
                {
                    availableRoomsCoordinates.Add(new Vector2Int(i, j));
                }
            }
        }

        List<int> availableProjectsIndexes = new List<int>();
        foreach (var t in availableRoomsCoordinates)
        {
            for (int j = 0; j < _projects.Count; j++)
            {
                if (_projects[j].RoomRequirement == _gridGenerator.grid_list[t.x][t.y].GetComponent<GridObject>().type)
                {
                    availableProjectsIndexes.Add(j);
                }
            }
        }

        DisplayAvailableProjects(availableProjectsIndexes);
    }

    //TODO display the projects on ui
    private void DisplayAvailableProjects(List<int> availableProjectsIndexes)
    {
        foreach (var index in availableProjectsIndexes)
        {
            Debug.Log(_projects[index]);
        }
    }

    public void OpenProjectWindow(int indexProject)
    {
        _projects[indexProject].inProgress = true;
        _mainPanel.GetComponent<ProjectIndexHolder>().projectIndexHolder = indexProject;


        // TODO open the charcter_panel_assignemnt window
    }

    private void ProjectProgress()
    {
        // TODO add timer here
    }

    public void DisplayAvailableCharactersRequired(ProjectIndexHolder indexProject)
    {
        List<int> availablePlayersIndexes = new List<int>();
        for (int i = 0; i < _playerManager.players.Count; i++)
        {
            if (_playerManager.players[i].avaliableForWork && _playerManager.players[i].job == _projects[indexProject.projectIndexHolder].JobRequirement)
            {
                availablePlayersIndexes.Add(i);
            }
        }

        CreateButtons(availablePlayersIndexes);
    }


    public void DisplayAvailableCharactersGeneral()
    {
        List<int> availablePlayersIndexes = new List<int>();
        for (int i = 0; i < _playerManager.players.Count; i++)
        {
            if (_playerManager.players[i].avaliableForWork)
            {
                availablePlayersIndexes.Add(i);
            }
        }

        CreateButtons(availablePlayersIndexes);
    }

    public void SetLastPressed(GameObject button)
    {
        _lastPressed = button;
    }

    //TODO change parameter
    private void FinishProject(int indexProject)
    {
        _projects[indexProject].inProgress = false;

        foreach (var person in _projects[indexProject]._currentPeople)
        {
            _playerManager.players[person].avaliableForWork = true;
        }

        // TODO remove the coordinates from the project
        // TODO will call the function to add the income
    }

    public void CancelProject(ProjectIndexHolder indexProject)
    {
        _projects[indexProject.projectIndexHolder].inProgress = false;
        foreach (var person in _projects[indexProject.projectIndexHolder]._currentPeople)
        {
            _playerManager.players[person].avaliableForWork = true;
        }
        // TODO remove the coordinates from the project

        // TODO reset the project window
    }

    public void CreateButtons(List<int> availablePlayers)
    {
        RemoveButtons();

        foreach (var player in availablePlayers)
        {
            GameObject button = Instantiate(_buttonPrefab, _panelForButtons.transform, true);
            button.transform.localScale = new Vector3(1f, 1f, 1f);
            //  button.GetComponent<Image>().sprite = player.
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _playerManager.players[player].job.ToString(); //Changing text
            button.GetComponent<CharacterIndexHolder>().characterIndex = player;
        }
    }

    public void RemoveButtons()
    {
        // remove all current buttons
        foreach (Transform child in _panelForButtons.transform)
        {
            Destroy(child.gameObject);
        }
    }
}