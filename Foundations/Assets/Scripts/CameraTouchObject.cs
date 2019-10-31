using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchObject : MonoBehaviour
{
    public float life_time = 7.5f;
    public float current_timer = 0.0f;
    public MultiTouchCamera camera_script;
    private void Update()
    {
        current_timer += 1 * Time.deltaTime;
        if(current_timer > life_time)
        {
            camera_script.active_touch_locations.Remove(gameObject);
            gameObject.SetActive(false);
            current_timer = 0.0f;
        }
    }
}
