using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProjectPanel : MonoBehaviour
{
    public ProjectManager projectManager;
    private PlayerManager _playerManager;
    [SerializeField] private GameObject _panelForButtons;
    [SerializeField] private GameObject _buttonPrefab;
    public GameObject lastPressed;


    private void Start()
    {
        _playerManager = projectManager._playerManager;
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

        projectManager._gridGenerator.grid_list[projectManager._projects[projectIndex].assignedRoom.x][projectManager._projects[projectIndex].assignedRoom.y].GetComponent<GridObject>().isAvailable =
            true;
        projectManager.FindAvailableProjects();
        Destroy(gameObject);
    }

    //TODO start the project when all character slots are filled in
    private void StartProject()
    {
    }

    public void CreateButtons(List<int> availablePlayers)
    {
        RemoveButtons();

        foreach (var player in availablePlayers)
        {
            GameObject button = Instantiate(_buttonPrefab, _panelForButtons.transform, true);
            button.transform.localScale = new Vector3(1f, 1f, 1f);
            //  TODO button.GetComponent<Image>().sprite = player.
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