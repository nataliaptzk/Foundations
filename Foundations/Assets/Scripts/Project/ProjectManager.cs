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

    [SerializeField] private GameObject _rowPrefab;
    [SerializeField] private GameObject _panelForProjects;

    [SerializeField] private GameObject _projectPanel;

   // public GameObject MainPanel => _mainPanel;

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
    public void FindAvailableProjects()
    {
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
                if (_projects[j].RoomRequirement == _gridGenerator.grid_list[t.x][t.y].GetComponent<GridObject>().type && !_projects[j].inProgress)
                {
                    if (!availableProjectsIndexes.Contains(j))
                    {
                        availableProjectsIndexes.Add(j);
                    }
                }
            }
        }

        DisplayAvailableProjects(availableProjectsIndexes);
    }

   private void DisplayAvailableProjects(List<int> availableProjectsIndexes)
    {
        CreateProjectRows(availableProjectsIndexes);
    }

    public void AssignRoom()
    {
        // TODO pick a random room and assign it to the project
    }
 
    private void ProjectProgress()
    {
        // TODO add timer here
    }



    // TODO change parameter
    private void FinishProject(int indexProject)
    {
        _projects[indexProject].inProgress = false;

        foreach (var person in _projects[indexProject]._currentPeople)
        {
            _playerManager.players[person].avaliableForWork = true;
        }

        // TODO remove the coordinates from the project
        // TODO make room available again
        // TODO will call the function to add the income
    }

   


    public void CreateProjectRows(List<int> availableProjects)
    {
        RemoveRows();

        // foreach (var project in availableProjects)
        {
            for (int i = 0; i < availableProjects.Count; i++)
            {
                GameObject row = Instantiate(_rowPrefab, _panelForProjects.transform, true);
                row.transform.localScale = new Vector3(1f, 1f, 1f);
                row.transform.localPosition = new Vector3(1f, 1f, 1f);
                row.GetComponent<ProjectIndexHolder>().projectIndexHolder = availableProjects[i];
                row.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _projects[availableProjects[i]].Title;
                row.GetComponent<AssignTheProjectIndexToTheStartButton>().projectManager = this;
                row.GetComponent<AssignTheProjectIndexToTheStartButton>()._projectPanelParent = _projectPanel;

                //  button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _playerManager.players[player].job.ToString(); //Changing text
                //  button.GetComponent<CharacterIndexHolder>().characterIndex = player;
            }
        }
    }

    public void RemoveRows()
    {
        // remove all current project rows
        foreach (Transform child in _panelForProjects.transform)
        {
            Destroy(child.gameObject);
        }
    }
}