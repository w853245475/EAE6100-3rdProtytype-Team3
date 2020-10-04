using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    Vector3 touchStart;
    public float minZoom = 1;
    public float maxZoom = 15;

    public float minX, minY, maxX, maxY;
    public bool CanMoveCamera = true;
    void Update()
    {
        if(CanMoveCamera)
        {
            if(Input.GetMouseButtonDown(0))
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Debug.Log(touchStart);
            }
            if(Input.touchCount >= 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position = touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;
                //zoom(difference * 0.01f);
            }

            else if(Input.GetMouseButton(0))
            {
                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                
                //Camera.main.transform.position += direction;
                
                
                //Camera.main.transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), -10);
            }
            zoom(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, minZoom, maxZoom);
    }

}
