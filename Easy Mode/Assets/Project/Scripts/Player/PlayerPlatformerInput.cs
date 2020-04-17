using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class PlayerPlatformerInput : MonoBehaviour {

    public bool useMobileInputs;

    [HideInInspector] public float horizontal;      //Float that stores horizontal input
    [HideInInspector] public bool jumpHeld;         //Bool that stores jump pressed
    [HideInInspector] public bool jumpPressed;      //Bool that stores jump held
    [HideInInspector] public bool attackPressed;
    [HideInInspector] public bool dashPressed;      //bool that store dash pressed
    [HideInInspector] public float xDirection;       //float to identify the facing direction
    [HideInInspector] public float targetXPosition;
    [HideInInspector] public float yDirection;
    private float lastPressedTime;                  //float to store last pressed time since dash was pressed
    private const float DOUBLE_PRESS_TIME = .2f;    //max time allowed to register dash

    private Camera cam;
    private bool readyToClear;                      //Bool used to keep input in sync
    [HideInInspector] public bool isActive;         //Bool check if game is still running

    //[SerializeField]
    //private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe = 20f;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private float curYSwipeLength;

    private void Start() {
        cam = Camera.main;
        xDirection = 1f;
        targetXPosition = transform.position.x;
        //SwipeDetector.OnSwipe += OnSwipe;
    }

    private void Update() {
        //Clear out existing input values
        ClearInput();

        //If the Game Manager says the game is over, exit
        //if (GameManager.IsGameOver())
        //    return;

        //if (!isActive) return;

        //Process keyboard, mouse, gamepad (etc) inputs
        ProcessInputs();

        //Clamp the horizontal input to be between -1 and 1
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);
    }

    private void FixedUpdate() {
        //In FixedUpdate() we set a flag that lets inputs to be cleared out during the 
        //next Update(). This ensures that all code gets to use the current inputs
        readyToClear = true;
    }

    //private void OnDisable() {
    //    SwipeDetector.OnSwipe -= OnSwipe;
    //}

    private void ClearInput() {
        //If we're not ready to clear input, exit
        if (!readyToClear)
            return;

        //Reset all inputs
        horizontal = 0f;
        jumpPressed = false;
        jumpHeld = false;

        attackPressed = false;
        dashPressed = false;

        readyToClear = false;
    }

    private void ProcessInputs() {
        //Accumulate horizontal axis input
        //horizontal += Input.GetAxis("Horizontal");

        //if (Time.timeScale == 0)
        //    return;

        float lastDirection = xDirection;

        if (useMobileInputs)
            CheckMobileInputs(lastDirection);
        else
            CheckEditorInputs(lastDirection);
        
    }

    //private bool IsPointerOverUIObject() {
    //    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    //    eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //    List<RaycastResult> results = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
    //    return results.Count > 0;
    //}

    private void CheckEditorInputs(float lastDirection) {

        //Avoid processing inputs when ui elements are press
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        //Accumulate button inputs
        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");
        //jumpHeld = jumpHeld || Input.GetButton("Jump");

        if (Input.GetMouseButtonDown(0)) {

            UpdateTargetXPosition(Input.mousePosition);
            CheckDashInput(lastDirection);
        }

    }

    private void CheckMobileInputs(float lastDirection) {

        for (int i = 0; i < Input.touchCount; i++) {

            Touch touch = Input.GetTouch(i);

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) {
                //Debug.Log("UI pressed");
                return;
            }

            if (touch.phase == TouchPhase.Began) {

                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
                UpdateTargetXPosition(fingerUpPosition);
                CheckDashInput(lastDirection);

                //Debug.Log("Touch Began!!!");

            } else if (touch.phase == TouchPhase.Moved) {

                fingerDownPosition = touch.position;
               
                UpdateTargetXPosition(fingerDownPosition);
                DetectSwipe();
                //Debug.Log("Touch Moved!!!");

            } 
            //else if (touch.phase == TouchPhase.Ended) { //DO NOT USE WILL CAUSE MOVEMENT ON UNPAUSED!!!

            ////fingerDownPosition = touch.position;

            //UpdateTargetXPosition(fingerDownPosition);
            ////DetectSwipe();
            ////Debug.Log("Touch Ended!!!");
            //}
        }
    }

    private void CheckDashInput(float lastDirection) {

        float curX = transform.position.x;

        if (targetXPosition > curX)
            xDirection = 1f;
        else if (targetXPosition < curX)
            xDirection = -1f;

        float timeSinceLastPressed = Time.time - lastPressedTime;

        if (timeSinceLastPressed <= DOUBLE_PRESS_TIME && lastDirection == xDirection)
            dashPressed = true;

        lastPressedTime = Time.time;
    }

    private void UpdateTargetXPosition(Vector3 inputPos) {
        targetXPosition = cam.ScreenToWorldPoint(inputPos).x;
    }

    private void DetectSwipe() {

        if (SwipeDistanceCheckMet()) {

            //float direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? 1f : -1f;
            SendSwipe();
            fingerUpPosition = fingerDownPosition;
        }
    }

    private bool SwipeDistanceCheckMet() {
        return VerticalMovementDistance() > minDistanceForSwipe;// || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance() {

        curYSwipeLength = Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
        //Debug.Log(curYSwipeLength);
        return curYSwipeLength;
    }

    private void SendSwipe() {

        curYSwipeLength = 0f;
        jumpPressed = true;
    }
}
