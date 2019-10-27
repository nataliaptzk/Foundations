using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    private string[] colours;
    public SpriteRenderer spriteRenderer;
    Color newColour;
    int currentIndex = 0;
    [SerializeField]
    Jobs currentJob = 0;
    public Dropdown jobDropdown;

    enum Jobs
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
        colours = new string[7];

        colours[0] = "#FFFFFF"; //white
        colours[1] = "#000000"; //black
        colours[2] = "#FF1200"; //red
        colours[3] = "#008BFF"; //blue
        colours[4] = "#00FF29"; //green
        colours[5] = "#FB489E"; //pink
        colours[6] = "#B148FA"; //purple

        SetColour(currentIndex);
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

    }

    public void OnClose()
    {

    }
}
