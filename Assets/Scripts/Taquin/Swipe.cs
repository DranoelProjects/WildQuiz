using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {

    public PointController PtController;

    Vector2 startPosition, stopPosition;
    float dragDistance = 100f;

	void Update () {
		
        if(Input.touchCount==1)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase==TouchPhase.Began)
            {
                startPosition = touch.position;
                stopPosition = touch.position;
            }
            else if( touch.phase == TouchPhase.Moved)
            {
                stopPosition = touch.position;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                stopPosition = touch.position;

                if(Mathf.Abs(stopPosition.x - startPosition.x)> dragDistance ||
                   Mathf.Abs(stopPosition.y - startPosition.y) > dragDistance)
                {
                    if(Mathf.Abs(stopPosition.x-startPosition.x) > Mathf.Abs(stopPosition.y - startPosition.y))
                    {
                        if(stopPosition.x > startPosition.x)
                        {
                            PtController.Swipe(Vector2.right);
                            Debug.Log("Swipe Right");
                        }
                        else
                        {
                            PtController.Swipe(Vector2.left);
                            Debug.Log("Swipe Left");
                        }
                    }
                    else
                    {
                        if (stopPosition.y > startPosition.y)
                        {
                            PtController.Swipe(Vector2.up);
                            Debug.Log("Swipe Up");
                        }
                        else
                        {
                            PtController.Swipe(Vector2.down);
                            Debug.Log("Swipe Down");
                        }
                    }
                }
                else
                {
                    Debug.Log("Tap");
                }
            }
        }
	}
}
