using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssignTheProjectIndexToTheStartButton : MonoBehaviour
{
    public ProjectManager projectManager;

    public void AssignTheProjectIndex(ProjectIndexHolder projectIndex)
    {
        // open the project window
        // assign the project index to the window
        // start button has to get where to child the window
        projectManager.MainPanel.gameObject.SetActive(true);
        projectManager.MainPanel.GetComponent<ProjectIndexHolder>().projectIndexHolder = projectIndex.projectIndexHolder;
        projectManager.MainPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = projectManager._projects[projectIndex.projectIndexHolder].JobRequirement.ToString();
        projectManager._projects[projectIndex.projectIndexHolder].inProgress = true;
        projectManager.FindAvailableProjects();
    }
}