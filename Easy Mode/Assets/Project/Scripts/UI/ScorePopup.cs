using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class ScorePopup : MonoBehaviour {

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;

    private Vector3 spawnPosition = Vector3.zero;
    private Camera cam;

    private void Start() {
        cam = Camera.main;
    }

    private void LateUpdate() {
        transform.position = cam.WorldToScreenPoint(spawnPosition);
    }

    public void SetText(Vector3 pos, string score, string combo , Color color) {
        spawnPosition = pos;
        scoreText.text = score;
        scoreText.color = color;
        comboText.text = combo;
    }
}
