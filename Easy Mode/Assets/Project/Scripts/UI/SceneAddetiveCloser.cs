using UnityEngine;

[DisallowMultipleComponent]
public class SceneAddetiveCloser : MonoBehaviour {

    [Range(2, 10)]
    public int sceneIndexToUnload = 2;

    public void CloseScene() {
        AudioManager.PlayUIAudio(GUIAudio.RegButton);

        SceneController.UnloadSceneAddetive(sceneIndexToUnload);
    }
}
