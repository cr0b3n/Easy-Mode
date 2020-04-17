using UnityEngine;

public abstract class TransitionSO : ScriptableObject {

    /// <summary>
    /// Returns anchored position first then a target postion
    /// </summary>
    /// <param name="rectTransform"></param>
    /// <returns>Returns a starting vector 3 position first then a final position</returns>
    public abstract (Vector3, Vector3) GetStartingAndFinalPosition(Vector3 rectTransformPos);

    public virtual void Behave(RectTransform rectTransform, Vector3 targetPosition, float speed, bool mustDisable) {

        rectTransform.anchoredPosition3D = Vector3.MoveTowards(rectTransform.anchoredPosition, targetPosition, speed * Time.unscaledDeltaTime);

        //Debug.Log(Mathf.Abs(rectTransform.anchoredPosition.y - targetPosition.y));
        if (!mustDisable) return;

        if (ReadyToDisable(rectTransform, targetPosition))
            rectTransform.gameObject.SetActive(false);
    }

    protected abstract bool ReadyToDisable(RectTransform rectTransform, Vector3 targetPosition);
}
