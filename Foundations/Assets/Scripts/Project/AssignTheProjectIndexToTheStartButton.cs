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

        PlaceTheProjectWindow(projectIndex, new Vector2Int(roomIndexes[randomRoom].x, roomIndexes[randomRoom].y));
        projectManager._projects[projectIndex].assignedRoom = roomIndexes[randomRoom];
        projectManager._gridGenerator.grid_list[roomIndexes[randomRoom].x][roomIndexes[randomRoom].y].GetComponent<GridObject>().isAvailable = false;
    }

    private void PlaceTheProjectWindow(int projectIndex, Vector2Int roomCoordinates)
    {
        GameObject projectPanel = Instantiate(_panelPrefab, _projectPanelParent.transform, true);
        projectPanel.transform.localScale = new Vector3(1f, 1f, 1f);

        Vector3 newWindowPosition = projectManager._gridGenerator.grid_list[roomCoordinates.x][roomCoordinates.y].gameObject.transform.position;
        projectPanel.transform.localPosition = newWindowPosition; // todo whereever the picked room is
        projectPanel.GetComponent<ProjectPanel>().projectManager = projectManager;
        projectPanel.GetComponent<ProjectIndexHolder>().projectIndexHolder = projectIndex;
        projectPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = projectManager._projects[projectIndex].JobRequirement.ToString();
    }
}