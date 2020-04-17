using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ScorePopupPooler : MonoBehaviour {

    public ScorePopup pooledObject;

    public int pooledAmount;

    List<ScorePopup> pooledObjects;

    private void OnEnable() {
        pooledObjects = new List<ScorePopup>();

        for (int i = 0; i < pooledAmount; i++) {

            ScorePopup obj = Instantiate(pooledObject, transform.position, Quaternion.identity, transform);
            obj.gameObject.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public ScorePopup GetPooledObject(Vector3 pos, Quaternion rot, bool isEnable = true) {

        for (int i = 0; i < pooledObjects.Count; i++) {

            if (!pooledObjects[i].gameObject.activeInHierarchy) {

                pooledObjects[i].transform.SetPositionAndRotation(pos, rot);
                pooledObjects[i].gameObject.SetActive(isEnable);

                return pooledObjects[i];
            }
        }

        ScorePopup obj = Instantiate(pooledObject, transform);

        pooledObjects.Add(obj);
        obj.transform.SetPositionAndRotation(pos, rot);
        obj.gameObject.SetActive(isEnable);

        return obj;
    }

}
