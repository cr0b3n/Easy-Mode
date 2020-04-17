using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class GameplayMenuManager : MonoBehaviour {

    [Header("Menu Section")]
    public TextMeshProUGUI menuHeaderText;
    public GameObject menuScreen;
    public GameObject pauseButton;
    public GameObject playButton;
    public GameObject nextLevelButton;
    public GameObject resetButton;

    [Header("Healthbar Section")]
    public TextMeshProUGUI healthBarText;
    public Image healthBarFill;

    private bool isPause;
    private bool isGameOver;
    private bool isTransitioning;
    private bool isActive;

    private IEnumerator Start() {

        SceneController.RemoveTransition();
        AudioManager.PlayBGM(BGMType.Gameplay);
        AudioListener.pause = false;
        Time.timeScale = 1f;

        yield return new WaitForSecondsRealtime(1f);

        isActive = true;
    }

    private void Update() {

        if (isGameOver) return;

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R))
            ResetGame();

        if (Input.GetKeyDown(KeyCode.H))
            SwitchToHomeScene();
#endif

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            PausePlayGame();
    }


    private void OnDisable() {
        StopAllCoroutines();
    }

    public void LevelComplete() {
        isGameOver = true;
        pauseButton.SetActive(false);
        playButton.SetActive(false);
        resetButton.SetActive(false);
        nextLevelButton.SetActive(true);
        menuScreen.SetActive(true);
        menuHeaderText.text = "Level Complete";
    }

    public void ShowGameOverScreen() {

        menuHeaderText.text = "GAME OVER";
        menuScreen.SetActive(true);
        isGameOver = true;
        pauseButton.SetActive(false);
        playButton.SetActive(false);
    }

    //FOR TIMER OR HEALBAR PURPOSE
    public void AdjustHealthBar(float curHealth, float maxHealth) {

        float progress = curHealth / maxHealth;
        float progressPercent = Mathf.Clamp(progress * 100f, 0f, 100f);

        healthBarText.text = progressPercent.ToString("n2") + "%";
        healthBarFill.fillAmount = progress;

        //Alpha set to 180 to reduce brightness
        if (curHealth > maxHealth / 2) 
            healthBarFill.color = new Color32((byte)MapValues(curHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 180); 
        else
            healthBarFill.color = new Color32(255, (byte)MapValues(curHealth, 0, maxHealth / 2, 0, 255), 0, 180);

        float MapValues(float x, float inMin, float inMax, float outMin, float outMax) {
            return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }
    }

    public void PausePlayGame() {

        if (isGameOver || isTransitioning) return;

        PlayUIAudio();
        pauseButton.SetActive(isPause);
        isPause = !isPause;
        AudioListener.pause = isPause;
        menuScreen.SetActive(isPause);
        menuHeaderText.text = "PAUSED";
        Time.timeScale = (isPause) ? 0.0f : 1.0f;
        //Debug.Log("Game is paused");
    }

    public void ResetGame() {

        if (isTransitioning || !isActive) return;

        isTransitioning = true;
        PlayUIAudio(true);
        SceneController.BeginTransitionAndLoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SwitchToHomeScene() {

        if (isTransitioning || !isActive) return;

        isTransitioning = true;
        PlayUIAudio(true);
        SceneController.BeginTransitionAndLoadScene(0);
    }

    private void PlayUIAudio() {
        AudioManager.PlayUIAudio(GUIAudio.RegButton);
    }

    private void PlayUIAudio(bool stopOthers) {
        AudioManager.PlayUIAudio(GUIAudio.Transition);

        if (stopOthers)
            AudioManager.StopAllAudio();
    }
}
