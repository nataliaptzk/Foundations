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
        projectManager._projects[projectIndex.projectIndexHolder].inProgress = true;

        AssignRoom(projectIndex.projectIndexHolder);
        projectManager.FindAvailableProjects();
    }


    private void AssignRoom(int projectIndex)
    {
        List<Vector2Int> roomIndexes = new List<Vector2Int>();

        for (int i = 0; i < projectManager._gridGenerator.grid_list.Count; i++)
        {
            for (int j = 0; j < projectManager._gridGenerator.grid_list[i].Count; j++)
            {
                if (projectManager._projects[projectIndex].RoomRequirement == projectManager._gridGenerator.grid_list[i][j].GetComponent<GridObject>().type)
                {
                    roomIndexes.Add(new Vector2Int(i, j));
                }
            }
        }

        int randomRoom = Random.Range(0, roomIndexes.Count - 1);

        // Allow to have only one panel open at a time
        if (projectManager.currentlyOpenWindow != null)
        {
            // make the currently open null
            projectManager.currentlyOpenWindow.GetComponent<ProjectPanel>().CancelProject();
            PlaceTheProjectWindow(projectIndex, new Vector2Int(roomIndexes[randomRoom].x, roomIndexes[randomRoom].y));
            projectManager._projects[projectIndex].assignedRoom = roomIndexes[randomRoom];
            projectManager._gridGenerator.grid_list[roomIndexes[randomRoom].x][roomIndexes[randomRoom].y].GetComponent<GridObject>().isAvailable = false;
        }
        else
        {
            PlaceTheProjectWindow(projectIndex, new Vector2Int(roomIndexes[randomRoom].x, roomIndexes[randomRoom].y));
            projectManager._projects[projectIndex].assignedRoom = roomIndexes[randomRoom];
            projectManager._gridGenerator.grid_list[roomIndexes[randomRoom].x][roomIndexes[randomRoom].y].GetComponent<GridObject>().isAvailable = false;
        }
    }

    private void PlaceTheProjectWindow(int projectIndex, Vector2Int roomCoordinates)
    {
        // dont instantiate, just give new values
        GameObject projectPanel = Instantiate(_panelPrefab, _projectPanelParent.transform, true);
        projectPanel.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        projectPanel.transform.localPosition = new Vector3(0f, 0f, 0f);

        projectPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200f, -200);

        projectPanel.GetComponent<ProjectPanel>().projectManager = projectManager;
        projectPanel.GetComponent<ProjectIndexHolder>().projectIndexHolder = projectIndex;
        projectPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = projectManager._projects[projectIndex].JobRequirement.ToString();

        projectManager.currentlyOpenWindow = projectPanel;
    }
}