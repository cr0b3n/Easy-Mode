using System;
using UnityEngine;

[DisallowMultipleComponent]
public class SwipeDetector : MonoBehaviour {
   
    [SerializeField]
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe = 20f;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private float curYSwipeLength;

    public static event Action<SwipeData> OnSwipe = delegate { };

    private void Update() {

        for (int i = 0; i < Input.touchCount; i++) {

            if (Input.touches[i].phase == TouchPhase.Began) {

                fingerUpPosition = Input.touches[i].position;
                fingerDownPosition = Input.touches[i].position;
            }

            if (!detectSwipeOnlyAfterRelease && Input.touches[i].phase == TouchPhase.Moved) {
                fingerDownPosition = Input.touches[i].position;
                DetectSwipe();
            }

            if (Input.touches[i].phase == TouchPhase.Ended) {
                fingerDownPosition = Input.touches[i].position;
                DetectSwipe();
            }
        }

        //foreach (Touch touch in Input.touches) {

        //    if (touch.phase == TouchPhase.Began) {

        //        fingerUpPosition = touch.position;
        //        fingerDownPosition = touch.position;
        //    }

        //    if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved) {
        //        fingerDownPosition = touch.position;
        //        DetectSwipe();
        //    }

        //    if (touch.phase == TouchPhase.Ended) {
        //        fingerDownPosition = touch.position;
        //        DetectSwipe();
        //    }
        //}
    }

    private void DetectSwipe() {

        if (SwipeDistanceCheckMet()) {

            //var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
            float direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? 1f : -1f;
            SendSwipe(direction);
            //if (IsVerticalSwipe())
            //{
            //    var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
            //    SendSwipe(direction);
            //}
            //else
            //{
            //    var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
            //    SendSwipe(direction);
            //}
            fingerUpPosition = fingerDownPosition;
        }
    }

    //private bool IsVerticalSwipe()
    //{
    //    return VerticalMovementDistance() > HorizontalMovementDistance();
    //}

    private bool SwipeDistanceCheckMet() {
        return VerticalMovementDistance() > minDistanceForSwipe;// || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance() {

        curYSwipeLength = Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
        return curYSwipeLength;
    }

    //private float HorizontalMovementDistance()
    //{
    //    return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    //}

    //private void SendSwipe(SwipeDirection direction)
    //{
    //    SwipeData swipeData = new SwipeData()
    //    {
    //        Direction = direction,
    //        StartPosition = fingerDownPosition,
    //        EndPosition = fingerUpPosition
    //    };
    //    OnSwipe(swipeData);
    //}

    private void SendSwipe(float yDirection) {

        SwipeData swipeData = new SwipeData() {

            StartPosition = fingerDownPosition,
            EndPosition = fingerUpPosition,
            YDirection = yDirection,
            YSwipeLength = curYSwipeLength
        };

        curYSwipeLength = 0f;

        OnSwipe(swipeData);
    }
}

public struct SwipeData {

    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public float YDirection;
    public float YSwipeLength;
}

//public struct SwipeData {
//    public Vector2 StartPosition;
//    public Vector2 EndPosition;
//    public SwipeDirection Direction;
//}

//public enum SwipeDirection
//{
//    Up,
//    Down,
//    Left,
//    Right
//}