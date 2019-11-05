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
    [SerializeField] private GameObject _inProgressSliderPrefab;
    public Sprite playerSprite;
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
        DisplayHelpText(0);
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
        DisplayHelpText(3);
        List<int> availablePlayersIndexes = new List<int>();
        for (int i = 0; i < _playerManager.players.Count; i++)
        {
            if (_playerManager.players[i].avaliableForWork && _playerManager.players[i].job == projectManager._projects[indexProject.projectIndexHolder].JobRequirement)
            {
                availablePlayersIndexes.Add(i);
            }
        }

        if (availablePlayersIndexes.Count == 0)
        {
            DisplayHelpText(1);
        }

        CreateButtons(availablePlayersIndexes);
    }


    public void DisplayAvailableCharactersGeneral()
    {
        DisplayHelpText(3);

        List<int> availablePlayersIndexes = new List<int>();
        for (int i = 0; i < _playerManager.players.Count; i++)
        {
            if (_playerManager.players[i].avaliableForWork)
            {
                availablePlayersIndexes.Add(i);
            }
        }

        if (availablePlayersIndexes.Count == 0)
        {
            DisplayHelpText(2);
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
        projectManager.currentlyOpenWindow = null;
        Destroy(gameObject);
    }

    public void StartProject()
    {
        if (projectManager._projects[GetComponent<ProjectIndexHolder>().projectIndexHolder]._currentPeople.Count ==
            projectManager._projects[GetComponent<ProjectIndexHolder>().projectIndexHolder].PeopleRequirement)
        {
            StartCoroutine(StartProjectProgress(GetComponent<ProjectIndexHolder>().projectIndexHolder));
        }
        else
        {
            DisplayHelpText(4);
        }
    }

    private IEnumerator StartProjectProgress(int indexProject)
    {
        projectManager.currentlyOpenWindow = null; // allow new projects to be open

        // not a nice way to "close" that window
        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        gameObject.GetComponent<Image>().enabled = false;

        var inProgressWindow = AddProjectToInProgress(indexProject);

        float duration = projectManager._projects[indexProject].Duration;

        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            inProgressWindow.transform.GetChild(1).GetComponent<Slider>().value = normalizedTime;
            yield return null;
        }

        FinishProject(indexProject, inProgressWindow);
    }

    private GameObject AddProjectToInProgress(int projectIndex)
    {
        GameObject inProgress = Instantiate(_inProgressSliderPrefab, projectManager.InProgressPanel.transform, true);
        inProgress.transform.localScale = new Vector3(1f, 1f, 1f);
        inProgress.transform.localPosition = new Vector3(0f, 0f, 0f);

        inProgress.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = projectManager._projects[projectIndex].Title;
        return inProgress;
    }

    private void FinishProject(int indexProject, GameObject inProgressWindow)
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
        Destroy(inProgressWindow);
        Destroy(gameObject);
    }


    private void CreateButtons(List<int> availablePlayers)
    {
        RemoveButtons();

        foreach (var player in availablePlayers)
        {
            GameObject button = Instantiate(_buttonPrefab, _panelForButtons.transform, true);
            button.transform.localScale = new Vector3(1f, 1f, 1f);

            button.GetComponent<Image>().sprite = playerSprite;
            ColorUtility.TryParseHtmlString(_playerManager.players[player].colour, out var newColour);
            button.GetComponent<Image>().color = newColour;

            button.GetComponent<AssignCharacterToTheProject>().projectManager = projectManager;
            button.GetComponent<AssignCharacterToTheProject>().projectPanel = this;
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _playerManager.players[player].job.ToString(); //Changing text
            button.GetComponent<CharacterIndexHolder>().characterIndex = player;
        }
    }

    public void DisplayHelpText(int messageIndex)
    {
        var childCount = gameObject.transform.childCount;
        var textBox = gameObject.transform.GetChild(childCount - 1);

        switch (messageIndex)
        {
            case 0:
                textBox.gameObject.SetActive(true);
                textBox.GetComponent<TextMeshProUGUI>().text =
                    "Click on the icons above to display available characters and choose one to assign to the project. Once all characters are assigned you can start the project.";
                break;
            case 1:
                textBox.gameObject.SetActive(true);
                textBox.GetComponent<TextMeshProUGUI>().text = "There are no characters available for the current requirement.";
                break;
            case 2:
                textBox.gameObject.SetActive(true);
                textBox.GetComponent<TextMeshProUGUI>().text = "Not enough characters available, please create more characters.";
                break;
            case 3:
                textBox.gameObject.SetActive(false);
                textBox.GetComponent<TextMeshProUGUI>().text = "";
                break;
            case 4:
                textBox.gameObject.SetActive(true);
                textBox.GetComponent<TextMeshProUGUI>().text = "Please assign all characters by clicking on the windows above.";
                break;
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