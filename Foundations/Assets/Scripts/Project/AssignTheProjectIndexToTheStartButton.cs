using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssignTheProjectIndexToTheStartButton : MonoBehaviour
{
    public ProjectManager projectManager;
    public GameObject _projectPanelParent;
    [SerializeField] private GameObject _panelPrefab;

    public void AssignTheProjectIndex(ProjectIndexHolder projectIndex)
    {
        // open the project window
        // assign the project index to the window
        // start button has to get where to child the window

        GameObject projectPanel = Instantiate(_panelPrefab, _projectPanelParent.transform, true);
        projectPanel.transform.localScale = new Vector3(1f, 1f, 1f);
        projectPanel.transform.localPosition = new Vector3(1f, 1f, 1f);
        projectPanel.GetComponent<ProjectPanel>().projectManager = projectManager;


        //   projectManager.MainPanel.gameObject.SetActive(true);
        projectPanel.GetComponent<ProjectIndexHolder>().projectIndexHolder = projectIndex.projectIndexHolder;
        projectPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = projectManager._projects[projectIndex.projectIndexHolder].JobRequirement.ToString();
        projectManager._projects[projectIndex.projectIndexHolder].inProgress = true;
        projectManager.FindAvailableProjects();
    }
}