using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTouchCamera : MonoBehaviour
{
    public List<GameObject> active_touch_locations;
    public float zoom_speed;
    public float max_size = 25.0f;
    public float min_size = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touch_location = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

            for (int j = 0; j < active_touch_locations.Count; j++)
            {
                Vector3 existing_location = active_touch_locations[j].transform.position;

                // checks if new touch is far enough away from existing ones
                if (Vector3.Distance(touch_location,existing_location) > 15.0f) 
                {
                    AddTouchLocation(touch_location);
                }

            }

            if(active_touch_locations.Count < 1)
            {
                AddTouchLocation(touch_location);
            }
        }
    }

    public void AddTouchLocation(Vector3 location)
    {
        GameObject new_location = new GameObject("TouchLocation");
        new_location.transform.position = location;
        active_touch_locations.Add(new_location);
    }

    public void SmoothCameraPositionAndSize()
    {

    }
}
