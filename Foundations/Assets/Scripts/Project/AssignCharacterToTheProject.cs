﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AssignCharacterToTheProject : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    public ProjectManager projectManager;
    public ProjectPanel projectPanel;

    private void Start()
    {
        _playerManager = projectManager._playerManager;
    }

    public void AssignCharacterToTheProjectList(CharacterIndexHolder indexHolder)
    {
        projectManager._projects[projectPanel.GetComponent<ProjectIndexHolder>().projectIndexHolder]._currentPeople.Add(indexHolder.characterIndex);
        _playerManager.players[indexHolder.characterIndex].avaliableForWork = false;

        projectPanel.lastPressed.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _playerManager.players[indexHolder.characterIndex].job.ToString();
        projectPanel.lastPressed.GetComponent<Button>().interactable = false;

        projectPanel.lastPressed.transform.GetChild(1).GetComponent<Image>().sprite = projectPanel.playerSprite;
        ColorUtility.TryParseHtmlString(_playerManager.players[indexHolder.characterIndex].colour, out var newColour);
        projectPanel.lastPressed.transform.GetChild(1).GetComponent<Image>().color = newColour;

        projectPanel.RemoveButtons();
        projectPanel.DisplayHelpText(0);

        if (projectManager._projects[projectPanel.GetComponent<ProjectIndexHolder>().projectIndexHolder]._currentPeople.Count ==
            projectManager._projects[projectPanel.GetComponent<ProjectIndexHolder>().projectIndexHolder].PeopleRequirement)
        {
            projectPanel.DisplayHelpText(5);
        }
    }
}