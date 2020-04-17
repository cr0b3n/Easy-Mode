using UnityEngine;

[CreateAssetMenu(fileName = "TopDown", menuName = "Acedia/MovingTransion/TopDownTransition")]
public class TopDownTransitionSO : TransitionSO {

    public float minYPosition = -1600.0f;
    public float maxYposition = 1600.0f;
    [Range(0.0f, 50.0f)]
    public float maxHidePosition = 15.0f;

    public override (Vector3, Vector3) GetStartingAndFinalPosition(Vector3 rectTransformPos) {

        float x = rectTransformPos.x;
        float y = (Random.value > 0.5f) ? minYPosition : maxYposition;

        return (new Vector3(x, y), new Vector3(x, 0f));
    }

    protected override bool ReadyToDisable(RectTransform rectTransform, Vector3 targetPosition) {
      
        float y = Mathf.Abs(rectTransform.anchoredPosition.y - targetPosition.y);
        
        return (y < maxHidePosition);
    }
}
