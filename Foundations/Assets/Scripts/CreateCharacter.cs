using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    private string[] colours;
    public SpriteRenderer spriteRenderer;
    public GameObject characterCreation;
    public PlayerManager playerManager;
    public GridGenerator rooms;
    public Button characterCreationButton;
    string roomType;
    Color newColour;
    int currentIndex = 0;
    [SerializeField]
    Jobs currentJob = 0;
    public Dropdown jobDropdown;
    [SerializeField]
    List<Player> playerList;

    public enum Jobs
    {
        programmer,
        gameDesigner,
        manager,
        projectLeader,
        actor,
        producer,
        soundTechnician,
        Musician,
        engineer,
        architect
    }

  
    // Start is called before the first frame update
    void Start()
    {
        //playerList = new List<Player>();

        colours = new string[7];

        colours[0] = "#FFFFFF"; //white
        colours[1] = "#000000"; //black
        colours[2] = "#FF1200"; //red
        colours[3] = "#008BFF"; //blue
        colours[4] = "#00FF29"; //green
        colours[5] = "#FB489E"; //pink
        colours[6] = "#B148FA"; //purple

        SetColour(currentIndex);

        CheckForAvaliableCharacterCreation();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeftArrowPressed()
    {
        currentIndex--;

        if(currentIndex < 0)
        {
            currentIndex = 6;
        }

        SetColour(currentIndex);
    }

   public void RightArrowPressed()
    {
        currentIndex++;

        if (currentIndex > 6)
        {
            currentIndex = 0;
        }

        SetColour(currentIndex);
    }

    void SetColour(int i)
    {
        //will convert hex to RGBA
        if (ColorUtility.TryParseHtmlString(colours[i], out newColour))
        {
            spriteRenderer.color = newColour;
        }
    }

    public void SetJob()
    {
        int changedValue = jobDropdown.value;
        currentJob = (Jobs)changedValue;
    }

    public void OnCreate()
    {
        roomType = "unknown";
        FindAvaliableRoom();
        playerManager.AddNewPlayer(colours[currentIndex], currentJob, roomType.ToString());

        CheckForAvaliableCharacterCreation();
        characterCreation.SetActive(false);
        ResetPanel();
    }

    public void OnClose()
    {
        characterCreation.SetActive(false);
        ResetPanel();
        
    }

    public void OpenPanel()
    {
        characterCreation.SetActive(true);
    }

    void ResetPanel()
    {
        jobDropdown.value = 0;
        currentJob = (Jobs)0;

        currentIndex = 0;
        SetColour(0);
    }

    void FindAvaliableRoom()
    {
        for(int i = 0; i < rooms.built_rooms.Count; i++)
        {
            if(rooms.built_rooms[i].current_occupants != rooms.built_rooms[i].max_occupants)
            {
                roomType = rooms.built_rooms[i].type.ToString();
                rooms.built_rooms[i].current_occupants++;

                //Add player into screen here
                // player pos rooms.builtRooms[i].transform.localPosition;
                break;
            }
        }
       
    }

    public void CheckForAvaliableCharacterCreation()
    {
        int spareSpaces = 0;
        for (int i = 0; i < rooms.built_rooms.Count; i++)
        {
            if (rooms.built_rooms[i].current_occupants != rooms.built_rooms[i].max_occupants)
            {
                for(int j = rooms.built_rooms[i].current_occupants; j < rooms.built_rooms[i].max_occupants; j++ )
                {
                    spareSpaces++;
                }
            }
        }

        Debug.Log("spare Spaces" + spareSpaces);

        if(spareSpaces == 0)
        {
            characterCreationButton.interactable = false;
            //blank out character creation till new room added
        }
        else
        {
            characterCreationButton.interactable = true;
        }
    }
}
