using UnityEngine;

[DisallowMultipleComponent]
public class TransitionController : MonoBehaviour {
    
    public RectTransform rectTransform;
    public TransitionSO transition;
    //public float minPos;
    //public float maxPos;

    private float moveTime;
    private Vector3 targetPosition;
    private bool mustDisable;
    private float speed;

    private void OnEnable() {

        moveTime = SceneController.GetMoveTime();
        mustDisable = false;

        //float x = rectTransform.anchoredPosition.x;
        //float y = (Random.value > 0.5f) ? minPos : maxPos;
        //rectTransform.anchoredPosition = new Vector3(x, y);
        //targetPosition = new Vector3(x, 0);

        (rectTransform.anchoredPosition, targetPosition) = transition.GetStartingAndFinalPosition(rectTransform.anchoredPosition);
        
        speed = Vector3.Distance(rectTransform.anchoredPosition, targetPosition) / moveTime;
    }

    private void Update() {

        transition.Behave(rectTransform, targetPosition, speed, mustDisable);
        //rectTransform.anchoredPosition3D = Vector3.MoveTowards(rectTransform.anchoredPosition, targetPosition, speed * Time.unscaledDeltaTime);

        ////Debug.Log(Mathf.Abs(rectTransform.anchoredPosition.y - targetPosition.y));
        //if (!mustDisable) return;

        //float y = Mathf.Abs(rectTransform.anchoredPosition.y - targetPosition.y);

        //if (y < 15f)
        //    gameObject.SetActive(false);
    }

    public void Hide() {
        mustDisable = true;

        //float x = rectTransform.anchoredPosition.x;
        //float y = (Random.value > 0.5f) ? minPos : maxPos;
        //targetPosition = new Vector3(x, y);

        (targetPosition, _) = transition.GetStartingAndFinalPosition(rectTransform.anchoredPosition);

        speed = Vector3.Distance(rectTransform.anchoredPosition, targetPosition) / moveTime;
    }
}
