using UnityEngine;

[DisallowMultipleComponent]
public class Disabler : MonoBehaviour {

    public float lifeSpan = 2.0f;

    private float disableTimer = 0f;
    private bool hasDisabled;

    private void OnEnable() {
        hasDisabled = false;
        disableTimer = Time.time + lifeSpan;
    }

    private void Update() {
        if (disableTimer <= Time.time && !hasDisabled) {

            hasDisabled = true;
            gameObject.SetActive(false);
        }
    }
}
