using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AssignCharacterToTheProject : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private ProjectManager _projectManager;

    private void Start()
    {
        var parent = gameObject.transform.parent;
        _playerManager = parent.GetComponent<ProjectManager>()._playerManager;
        _projectManager = parent.GetComponent<ProjectManager>();
        // need project indexHolder from the main panel
    }

    public void AssignCharacterToTheProjectBip(CharacterIndexHolder indexHolder)
    {
        _projectManager._projects[_projectManager.MainPanel.GetComponent<ProjectIndexHolder>().projectIndexHolder]._currentPeople.Add(indexHolder.characterIndex);
        _playerManager.players[indexHolder.characterIndex].avaliableForWork = false;
        Debug.Log(indexHolder.characterIndex);

        _projectManager._lastPressed.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _playerManager.players[indexHolder.characterIndex].job.ToString();
        _projectManager._lastPressed.GetComponent<Button>().interactable = false;
        _projectManager.RemoveButtons();
    }
}