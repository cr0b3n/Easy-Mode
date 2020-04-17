using UnityEngine;

[DisallowMultipleComponent]
public class CreditsController : MonoBehaviour {
    public void OpenLink(int index) {

        AudioManager.PlayUIAudio(GUIAudio.RegButton);

        switch (index) {
            default:
            case 0:
                Application.OpenURL("https://www.croben.com/");
                break;
            case 1:
                Application.OpenURL("https://www.facebook.com/crobengames");
                break;
            case 2:
                Application.OpenURL("https://twitter.com/crobengames");
                break;
            case 3:
                Application.OpenURL("https://www.youtube.com/user/teknoaxe");
                break;
        }
    }
}
