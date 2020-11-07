using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointController : MonoBehaviour {

    [SerializeField] int speed = 2;
    [SerializeField] float moveDistance = 0.5f;
    bool canMove = false;
    Vector2 dirVector = Vector2.zero;


	void Update () {
        if(canMove)
        {
            transform.Translate(dirVector * Time.deltaTime * speed);
        }
	}

    public void Swipe(Vector2 v)
    {
        dirVector = v;
        StartCoroutine(SwipeMove());

    }

    IEnumerator SwipeMove()
    {
        canMove = true;
        yield return new WaitForSeconds(moveDistance);
        canMove = false;
    }
}
