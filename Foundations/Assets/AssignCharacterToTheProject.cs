using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignCharacterToTheProject : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private ProjectManager _projectManager;

    private void Start()
    {
        var parent = gameObject.transform.parent;
        _playerManager = parent.GetComponent<ProjectManager>().Manager;
        _projectManager = parent.GetComponent<ProjectManager>();
        // need project indexHolder from the main panel
    }

    public void AssignCharacterToTheProjectBip(CharacterIndexHolder indexHolder)
    {
        // check requirements of people
        // open the ui window, click to choose person, remove person from available, fill in all slots

        // List<Player> availablePlayers = _playerManager.players.Where(player => player.avaliableForWork).ToList(); // stores all available players for the project
        // click on player to assign
        // player.isavailable false
        // add to the list project.currentppl
        _projectManager._projects[_projectManager.MainPanel.GetComponent<ProjectIndexHolder>().projectIndexHolder]._currentPeople.Add(indexHolder.characterIndex);
        _playerManager.players[indexHolder.characterIndex].avaliableForWork = false;
        Debug.Log(indexHolder.characterIndex);
        // deactivate the button that can be used to assign the character
    }
}
