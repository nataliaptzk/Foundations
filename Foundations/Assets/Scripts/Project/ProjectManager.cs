using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProjectManager : MonoBehaviour
{
    public List<ProjectClass> _projects = new List<ProjectClass>();
    public GridGenerator _gridGenerator;
    public PlayerManager _playerManager;

    [SerializeField] private GameObject _rowPrefab;
    [SerializeField] private GameObject _panelForProjects;
    [SerializeField] private GameObject _projectPanel;
    [SerializeField] private GameObject _inProgressPanel;
    public GameObject currentlyOpenWindow;

    public GameObject InProgressPanel => _inProgressPanel;

    private void Start()
    {
        CreateProjects();
    }

    private void CreateProjects()
    {
        _projects.Add(new ProjectClass("Code review", 100, 15, 1, RoomType.pc, CreateCharacter.Jobs.programmer));
        _projects.Add(new ProjectClass("Implement new features", 250, 25, 3, RoomType.pc, CreateCharacter.Jobs.programmer));
        _projects.Add(new ProjectClass("Design the serious game", 500, 50, 5, RoomType.pc, CreateCharacter.Jobs.gameDesigner));
        _projects.Add(new ProjectClass("Interpret serious role", 100, 10, 1, RoomType.greenscreen, CreateCharacter.Jobs.actor));
        _projects.Add(new ProjectClass("Manage creative components", 250, 20, 3, RoomType.greenscreen, CreateCharacter.Jobs.producer));
        _projects.Add(new ProjectClass("Read and edit scripts", 500, 40, 5, RoomType.greenscreen, CreateCharacter.Jobs.producer));
        _projects.Add(new ProjectClass("Daily Stand Up", 100, 15, 1, RoomType.meeting, CreateCharacter.Jobs.manager));
        _projects.Add(new ProjectClass("Sprint review", 250, 25, 3, RoomType.meeting, CreateCharacter.Jobs.projectLeader));
        _projects.Add(new ProjectClass("Retrospective meeting", 500, 35, 5, RoomType.meeting, CreateCharacter.Jobs.projectLeader));
        _projects.Add(new ProjectClass("Soundtrack Recording", 100, 20, 1, RoomType.audio, CreateCharacter.Jobs.Musician));
        _projects.Add(new ProjectClass("Noise Removal", 250, 25, 3, RoomType.audio, CreateCharacter.Jobs.Musician));
        _projects.Add(new ProjectClass("Sound recording on set", 500, 40, 5, RoomType.audio, CreateCharacter.Jobs.soundTechnician));
        _projects.Add(new ProjectClass("Research and development", 100, 30, 1, RoomType.workshop, CreateCharacter.Jobs.engineer));
        _projects.Add(new ProjectClass("Solve complex problems", 250, 45, 3, RoomType.workshop, CreateCharacter.Jobs.engineer));
        _projects.Add(new ProjectClass("Design proposal", 500, 60, 5, RoomType.workshop, CreateCharacter.Jobs.architect));
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
        foreach (var coordinate in availableRoomsCoordinates)
        {
            for (int j = 0; j < _projects.Count; j++)
            {
                if (_projects[j].RoomRequirement == _gridGenerator.grid_list[coordinate.x][coordinate.y].GetComponent<GridObject>().type && !_projects[j].inProgress)
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

    private void CreateProjectRows(List<int> availableProjects)
    {
        RemoveRows();

        foreach (var project in availableProjects)
        {
            GameObject row = Instantiate(_rowPrefab, _panelForProjects.transform, true);
            row.transform.localScale = new Vector3(1f, 1f, 1f);
            row.transform.localPosition = new Vector3(1f, 1f, 1f);
            row.GetComponent<ProjectIndexHolder>().projectIndexHolder = project;
            row.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _projects[project].Title;
            row.GetComponent<AssignTheProjectIndexToTheStartButton>().projectManager = this;
            row.GetComponent<AssignTheProjectIndexToTheStartButton>()._projectPanelParent = _projectPanel;
        }
    }

    private void RemoveRows()
    {
        // remove all current project rows
        foreach (Transform child in _panelForProjects.transform)
        {
            Destroy(child.gameObject);
        }
    }
}