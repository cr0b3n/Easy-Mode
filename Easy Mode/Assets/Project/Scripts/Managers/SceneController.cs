using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class SceneController : MonoBehaviour {

    [SerializeField]
    [Range(0.0f, 5.0f)]
    private float enableDelay = 0.05f;

    [SerializeField]
    [Range(0.01f, 5.0f)]
    private float transitionSpeed = .5f;

    [SerializeField]
    private TransitionController[] transitions;

    //public static Action OnTransitionComplete;
    private bool canTransition;
    private bool hasSceneAddetive;

    public static Action OnSceneAddetiveClose;

    #region Singleton

    private static SceneController instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion /Singleton

    public static float GetMoveTime() {

        if (instance == null) return .3f;

        return instance.transitionSpeed;
    }

    public static void BeginTransitionAndLoadScene(int sceneIndex) {

        if (instance == null) return;
        
        instance.TransitionInAndLoadScene(sceneIndex);
    }

    private void TransitionInAndLoadScene(int sceneIndex) {

        StopAllCoroutines();

        TransitionController[] objs = instance.ReOrderTransitionObjects(instance.transitions);

        instance.ChangeIndex(objs);

        instance.StartCoroutine(instance.ShowTransitionAndLoadScene(objs, sceneIndex));
    }

    private IEnumerator ShowTransitionAndLoadScene(TransitionController[] objs, int sceneIndex) {

        
        for (int i = 0; i < objs.Length; i++) {

            //objs[i].SetActive(!objs[i].gameObject.activeSelf);
            objs[i].gameObject.SetActive(false);
            objs[i].gameObject.SetActive(true);

            yield return new WaitForSecondsRealtime(instance.enableDelay);
        }
    
        yield return new WaitForSecondsRealtime(transitionSpeed);

        SceneManager.LoadScene(sceneIndex);
        //Debug.Log("Load new Scene");
    }

    public static void RemoveTransition() {

        if (instance == null) return;

        if (!instance.canTransition) {
            instance.canTransition = true;
            return;
        }

        TransitionController[] objs = instance.ReOrderTransitionObjects(instance.transitions);

        instance.StartCoroutine(instance.TransitionOut(objs));
    }

    private IEnumerator TransitionOut(TransitionController[] objs) {

        for (int i = 0; i < objs.Length; i++) {

            //objs[i].SetActive(!objs[i].gameObject.activeSelf);
            objs[i].Hide();

            yield return new WaitForSecondsRealtime(instance.enableDelay);
        }
    }

    private TransitionController[] ReOrderTransitionObjects(TransitionController[] objs) => objs.OrderBy(a => Guid.NewGuid()).ToArray();

    //To make sure that the order of rendering is base on the index
    private void ChangeIndex(TransitionController[] objs) {
        for (int i = 0; i < objs.Length; i++)
            objs[i].transform.SetSiblingIndex(i);
    }

    public static void LoadSceneAddetive(int sceneIndex) {

        if (instance == null) return;

        if (!instance.hasSceneAddetive)
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);

        instance.hasSceneAddetive = true;
    }

    public static void UnloadSceneAddetive(int sceneIndex) {

        if (instance.hasSceneAddetive)
            SceneManager.UnloadSceneAsync(sceneIndex);

        instance.hasSceneAddetive = false;
        OnSceneAddetiveClose?.Invoke();
    }
}
