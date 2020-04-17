using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EffectManager : MonoBehaviour {

    #region /Singleton

    public static EffectManager instance;

    private void Awake() {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion


}
