using System;
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

    public void AssignCharacterToTheProjectBip(CharacterIndexHolder indexHolder)
    {
        // TODO add character to the project list
         projectManager._projects[projectPanel.GetComponent<ProjectIndexHolder>().projectIndexHolder]._currentPeople.Add(indexHolder.characterIndex);
        _playerManager.players[indexHolder.characterIndex].avaliableForWork = false;
        Debug.Log(indexHolder.characterIndex);

       projectPanel.lastPressed.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _playerManager.players[indexHolder.characterIndex].job.ToString();
       projectPanel.lastPressed.GetComponent<Button>().interactable = false;
       projectPanel.RemoveButtons();
    }
}