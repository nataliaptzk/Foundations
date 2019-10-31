using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTouchCamera : MonoBehaviour
{
    public List<GameObject> active_touch_locations;
    public float size_lerp_time = 5.0f;
    public float position_lerp_time = 5.0f;
    public float max_size = 25.0f;
    public float min_size = 5.0f;
    public float inactive_timer = 15.0f;
    public GameObject reset_target;
    public bool mouse_usable = false;

    private Camera cam;
    private float current_position_lerp;
    private float current_size_lerp;
    private float old_size;
    private Vector3 old_position;
    private int old_count = 0;
    private ObjectPooler OP;
    private Vector3 target_position;
    private float target_size;
    private bool currently_lerping = false;

    void Start()
    {
        cam = GetComponent<Camera>();
        OP = GetComponent<ObjectPooler>();
        SetLerpStartValues();
        old_position = transform.position;
        target_position = reset_target.transform.position;
        old_size = max_size;
    }

    void Update()
    {
        CheckForInput();
        SmoothCameraPositionAndSize();
    }

    public void SetLerpStartValues()
    {
        old_position = transform.position;
        old_size = cam.orthographicSize;
        old_count = active_touch_locations.Count;
        current_position_lerp = 0.0f;
        current_size_lerp = 0.0f;
    }

    public void SetLerpDuringValues()
    {
        old_position = transform.position;
        old_size = cam.orthographicSize;
        old_count = active_touch_locations.Count;
        if (current_position_lerp > 0.5)
        {
            current_position_lerp = 0.5f;
        }
        if(current_size_lerp > 0.5f)
        {
            current_size_lerp = 0.5f;
        }
    }

    public void CheckForInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touch_location = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            touch_location.z = -10;
            bool valid_touch = true;
            for (int j = 0; j < active_touch_locations.Count; j++)
            {
                Vector3 existing_location = active_touch_locations[j].transform.position;
                existing_location.z = -10;

                // checks if new touch is far enough away from existing ones
                if (Vector3.Distance(touch_location, existing_location) < 10.0f)
                {
                    active_touch_locations[j].GetComponent<CameraTouchObject>().current_timer = 0.0f;
                    valid_touch = false;
                }
            }
            if (active_touch_locations.Count < 1 || valid_touch)
            {
                AddTouchLocation(touch_location);
                SetLerpStartValues();
            }

        }

        //for testing on pc - this also works with touch so set bool to false if not using mouse
        if (mouse_usable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouse_location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouse_location.z = -10;
                bool valid_touch = true;
                for (int j = 0; j < active_touch_locations.Count; j++)
                {
                    Vector3 existing_location = active_touch_locations[j].transform.position;
                    existing_location.z = -10;

                    // checks if new touch is far enough away from existing ones to be valid
                    if (Vector3.Distance(mouse_location, existing_location) < 10.0f)
                    {
                        active_touch_locations[j].GetComponent<CameraTouchObject>().current_timer = 0.0f;
                        valid_touch = false;
                    }
                }
                if (active_touch_locations.Count < 1 || valid_touch)
                {
                    AddTouchLocation(mouse_location);
                    SetLerpStartValues();
                }
            }
        }
    }

    public void AddTouchLocation(Vector3 location)
    {
        GameObject new_touch_obj = OP.GetPooledObject();
        CameraTouchObject touch_script = new_touch_obj.GetComponent<CameraTouchObject>();
        touch_script.life_time = inactive_timer;
        touch_script.camera_script = this;
        new_touch_obj.transform.position = location;
        new_touch_obj.transform.parent = reset_target.transform;
        new_touch_obj.SetActive(true);
        active_touch_locations.Add(new_touch_obj);
    }

    public void SmoothCameraPositionAndSize()
    {
        //if no active touches, it zooms out to show entire building
        if (active_touch_locations.Count < 1 && currently_lerping == false)
        {
            target_position = reset_target.transform.position;
            target_size = max_size;
            old_count = 0;
            SetLerpStartValues();
            currently_lerping = true;
        }

        //get center position from active touches and get distance of touches for zooming
        if (active_touch_locations.Count > 0)
        {
            currently_lerping = true;
            UpdateSizeAndPositionOfTouches();
            if (active_touch_locations.Count < old_count)
            {
                old_count = active_touch_locations.Count;
                Debug.Log("yo");
                SetLerpDuringValues();
            }
        }

        //lerp position
        current_position_lerp += 1 * Time.deltaTime;
        if(current_position_lerp > position_lerp_time)
        {
            current_position_lerp = position_lerp_time;
            old_position = transform.position;
            currently_lerping = false;
        }

        float pos_t = current_position_lerp / position_lerp_time;
        pos_t = pos_t * pos_t * pos_t * (pos_t * (6f * pos_t - 15f) + 10f);

        Vector3 cam_position = Vector2.Lerp(old_position, target_position, pos_t);
        cam_position.z = -10;
        transform.position = cam_position;

        //lerp camera size
        current_size_lerp += 1 * Time.deltaTime;
        if(current_size_lerp > size_lerp_time)
        {
            current_size_lerp = size_lerp_time;
            old_size = cam.orthographicSize;
        }
        //smooth lerping - smoothstep
        float size_t = current_size_lerp / size_lerp_time;
        size_t = size_t * size_t * size_t * (size_t * (6f * size_t - 15f) + 10f);
        float cam_size = Mathf.Lerp(old_size, target_size, size_t);
        cam.orthographicSize = cam_size;

        //clamp position within game area
        Vector3 v = transform.position - reset_target.transform.position;
        v = Vector2.ClampMagnitude(v, 20);
        v.z = -10;
        transform.position = reset_target.transform.position + v;

        //clamp camera size with min/max
        if (cam.orthographicSize > max_size)
        {
            cam.orthographicSize = max_size;
        }
        if(cam.orthographicSize < min_size)
        {
            cam.orthographicSize = min_size;
        }

    }

    public void UpdateSizeAndPositionOfTouches()
    {
        Vector2 combined_position = Vector2.zero;
        float greatest_distance = min_size;
        float next_greatest = min_size;
        bool use_greatest = false;

        for (int i = 0; i < active_touch_locations.Count; i++)
        {
            Vector2 touch_pos = (Vector2)active_touch_locations[i].transform.position;
            combined_position += touch_pos;
            //check the distance between touches, find the greatest distance to use for camera size
            for (int j = 0; j < active_touch_locations.Count; j++)
            {
                Vector2 next_pos = (Vector2)active_touch_locations[j].transform.position;
                if (touch_pos != next_pos)
                {
                    float current_distance = Vector2.Distance(touch_pos, next_pos);
                    if (current_distance > greatest_distance)
                    {
                        use_greatest = true;
                        greatest_distance = current_distance;
                    }
                    else if (current_distance > next_greatest)
                    {
                        next_greatest = current_distance;
                    }
                }
            }
            //set target size
            if (use_greatest)
            {
                target_size = greatest_distance;
            }
            else
            {
                target_size = next_greatest;
            }
        }
        target_position = (combined_position / active_touch_locations.Count);
    }
}
