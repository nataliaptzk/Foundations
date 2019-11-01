using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProjectPanel : MonoBehaviour
{
    public ProjectManager projectManager;
    private PlayerManager _playerManager;
    [SerializeField] private GameObject _panelForButtons;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private Sprite _playerSprite;
    public GameObject lastPressed;

    public List<GameObject> buttons = new List<GameObject>();

    private void Start()
    {
        _playerManager = projectManager._playerManager;
        buttons.Add(gameObject.transform.GetChild(1).gameObject);
        buttons.Add(gameObject.transform.GetChild(2).gameObject);
        buttons.Add(gameObject.transform.GetChild(3).gameObject);
        buttons.Add(gameObject.transform.GetChild(4).gameObject);
        buttons.Add(gameObject.transform.GetChild(5).gameObject);

        foreach (var button in buttons)
        {
            button.SetActive(false);
        }

        DisplayButtons();
    }

    private void DisplayButtons()
    {
        for (int i = 0; i < projectManager._projects[this.GetComponent<ProjectIndexHolder>().projectIndexHolder].PeopleRequirement; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
    }

    public void DisplayAvailableCharactersRequired(ProjectIndexHolder indexProject)
    {
        List<int> availablePlayersIndexes = new List<int>();
        for (int i = 0; i < _playerManager.players.Count; i++)
        {
            if (_playerManager.players[i].avaliableForWork && _playerManager.players[i].job == projectManager._projects[indexProject.projectIndexHolder].JobRequirement)
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
        lastPressed = button;
    }

    public void CancelProject()
    {
        var projectIndex = gameObject.GetComponent<ProjectIndexHolder>().projectIndexHolder;
        projectManager._projects[projectIndex].inProgress = false;
        foreach (var person in projectManager._projects[projectIndex]._currentPeople)
        {
            _playerManager.players[person].avaliableForWork = true;
        }

        projectManager._projects[projectIndex]._currentPeople.Clear();

        projectManager._gridGenerator.grid_list[projectManager._projects[projectIndex].assignedRoom.x][projectManager._projects[projectIndex].assignedRoom.y].GetComponent<GridObject>().isAvailable =
            true;
        projectManager.FindAvailableProjects();
        Destroy(gameObject);
    }

    public void StartProject()
    {
        if (projectManager._projects[GetComponent<ProjectIndexHolder>().projectIndexHolder]._currentPeople.Count ==
            projectManager._projects[GetComponent<ProjectIndexHolder>().projectIndexHolder].PeopleRequirement)
        {
            StartCoroutine(StartProjectProgress(GetComponent<ProjectIndexHolder>().projectIndexHolder));
        }
    }

    private IEnumerator StartProjectProgress(int indexProject)
    {
        // not a nice way to close that window
        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        gameObject.GetComponent<Image>().enabled = false;

        yield return new WaitForSecondsRealtime(projectManager._projects[indexProject].Duration);

        FinishProject(indexProject);
    }

    private void FinishProject(int indexProject)
    {
        projectManager._projects[indexProject].inProgress = false;

        foreach (var person in projectManager._projects[indexProject]._currentPeople)
        {
            _playerManager.players[person].avaliableForWork = true;
        }

        projectManager._projects[indexProject]._currentPeople.Clear();
        projectManager._gridGenerator.grid_list[projectManager._projects[indexProject].assignedRoom.x][projectManager._projects[indexProject].assignedRoom.y].GetComponent<GridObject>().isAvailable =
            true;
        projectManager.FindAvailableProjects();

        //todo income doesnt work
        Income.addIncomeAmount(projectManager._projects[indexProject].Income);
        Destroy(gameObject);
    }


    private void CreateButtons(List<int> availablePlayers)
    {
        RemoveButtons();

        foreach (var player in availablePlayers)
        {
            GameObject button = Instantiate(_buttonPrefab, _panelForButtons.transform, true);
            button.transform.localScale = new Vector3(1f, 1f, 1f);

            button.GetComponent<Image>().sprite = _playerSprite;
            ColorUtility.TryParseHtmlString(_playerManager.players[player].colour, out var newColour);
            button.GetComponent<Image>().color = newColour;

            button.GetComponent<AssignCharacterToTheProject>().projectManager = projectManager;
            button.GetComponent<AssignCharacterToTheProject>().projectPanel = this;
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