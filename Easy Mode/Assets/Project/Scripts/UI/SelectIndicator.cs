using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class SelectIndicator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [Range(0.0f, 10.0f)]
    public float smoothDamp = 8f;

    [Range(1f, 10f)]
    public float maxScale = 1.2f;

    private Vector3 originaScale = Vector3.one;
    //private const float SCALE_RATE = 0.15f;

    //private float timer;
    //Do not place on Start could cause originalScale to become 0,0,0 Change to Awake if problem arrise
    //D not put onEnable since some objects has to be disable could cause unncessary re-assignment
    //private void Awake() {
    //    originaScale = transform.localScale;
    //}

    private IEnumerator Scale(Vector3 from, Vector3 target) {

        float progress = 0;

        while (progress <= 1) {

            transform.localScale = Vector3.Lerp(from, target, progress);
            progress += (smoothDamp * Time.unscaledDeltaTime);
            yield return null;
        }

        transform.localScale = target;
    }

    //private bool CheckRate() => Time.unscaledTime > timer;

    public void OnPointerEnter(PointerEventData eventData) {

        //if (!CheckRate()) return;

        //timer = Time.unscaledTime + SCALE_RATE;  
        StopAllCoroutines();
        transform.localScale = originaScale;
        StartCoroutine(Scale(transform.localScale, originaScale * maxScale));
    }

    public void OnPointerExit(PointerEventData eventData) {

        StopAllCoroutines();
        StartCoroutine(Scale(transform.localScale, originaScale));
    }

    public void OnDisable() {
        transform.localScale = originaScale;
    }
}
