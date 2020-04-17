using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour {

    #region /Singleton

    public static GameManager instance;

    private void Awake() {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public GameplayMenuManager menu;

    [Range(1, 100)]
    public int maxHealth = 100;
    private int curHealth;

    private void Start() {
        curHealth = maxHealth;
        menu.AdjustHealthBar(curHealth, maxHealth);
    }

    private void Update() {

        //TODO:: To be deleted specially if update is not actually in used!!!
#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.G))
            GameOver();

        if (Input.GetKeyDown(KeyCode.L))
            LevelComplete();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            AdjustHealth(Random.Range(1, 10));
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            AdjustHealth(Random.Range(-10, -1));
#endif

    }

    private void AdjustHealth(int amount) {

        Debug.Log(amount);

        curHealth += amount;
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
        menu.AdjustHealthBar(curHealth, maxHealth);
    }

    private void GameOver() {
        menu.ShowGameOverScreen();
        AudioManager.PlayAudioEffect(EffectAudio.GameOver);
    }

    private void LevelComplete() {
        menu.LevelComplete();
        AudioManager.PlayAudioEffect(EffectAudio.LevelComplete);
    }
}
