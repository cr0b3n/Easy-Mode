using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public GameObject pooledObject;

    public int pooledAmount;

    List<GameObject> pooledObjects;

    private void OnEnable() {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++) {

            GameObject obj = Instantiate(pooledObject, transform.position, Quaternion.identity, transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject(Vector3 pos, Quaternion rot, bool isEnable = true) {

        for (int i = 0; i < pooledObjects.Count; i++) {

            if (!pooledObjects[i].activeInHierarchy) {

                pooledObjects[i].transform.SetPositionAndRotation(pos, rot);
                pooledObjects[i].SetActive(isEnable);

                return pooledObjects[i];
            }
        }

        GameObject obj = Instantiate(pooledObject, transform);

        pooledObjects.Add(obj);
        obj.transform.SetPositionAndRotation(pos, rot);
        obj.SetActive(isEnable);

        return obj;
    }
}
