using UnityEngine;

[CreateAssetMenu(fileName = "SideToSide", menuName = "Acedia/MovingTransion/SideToSideTransition")]
public class SideToSideTransitionSO : TransitionSO {

    public float minXPosition = -2800.0f;
    public float maxXposition = 2800.0f;
    [Range(0.0f, 50.0f)]
    public float maxHidePosition = 15.0f;

    public override (Vector3, Vector3) GetStartingAndFinalPosition(Vector3 rectTransformPos) {

        float x = (Random.value > 0.5f) ? minXPosition : maxXposition; 
        float y = rectTransformPos.y;

        return (new Vector3(x, y), new Vector3(0f, y));
    }

    protected override bool ReadyToDisable(RectTransform rectTransform, Vector3 targetPosition) {

        float x = Mathf.Abs(rectTransform.anchoredPosition.x - targetPosition.x);

        return (x < maxHidePosition);
    }
}
