using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TouchInput : MonoBehaviour
{
    private Vector3 _touchStart;
    private Camera _cam;
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Pan();
        PinchToZoom();
    }

    // Pan the screen with one finger
    void Pan()
    {
        // Check for touch input
        if (Input.GetMouseButtonDown(0))
        {
            // Get the position where the touch started
            _touchStart = _cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            // Move the camera accordingly to the new position
            Vector3 direction = _touchStart - _cam.ScreenToWorldPoint(Input.mousePosition);
            _cam.transform.position += direction;
        }
    }

    // Pinch to zoom
    void PinchToZoom()
    {
        // If there are two touches on the device...
        if (Input.touchCount != 2) return;

        var touchZero = Input.GetTouch(0);
        var touchOne = Input.GetTouch(1);

        // Find the position in the previous frame of each touch
        var touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        // Find the magnitude of the vector between the touches in each frame
        var prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        var touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

        // Difference in the distances between each frame
        var deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

        // Zoom
        _cam.orthographicSize += deltaMagnitudeDiff;
    }

    public void TestClick()
    {
        text.text = "The button works";
    }
}