using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class BackgroundScroller : MonoBehaviour {

    public float speed = 2.0f;
    public int totalBG = 2;
    public float backgroundWidth;

    //Vector3 moveTowards; do not use will cause uneven speed!!!

    //private void Start() {
    //    moveTowards = new Vector3(-speed * Time.deltaTime, 0, 0);
    //}

    private void Update() {
        //transform.position += moveTowards; DO NOT USE WILL CAUSE UNEVEN SPEED

        transform.Translate(new Vector3(-speed * Time.deltaTime, 0));

        if (transform.position.x < -backgroundWidth)
            RepositionBackground();
    }

    private void RepositionBackground() {

        Vector2 scrollOffset = new Vector2(backgroundWidth * totalBG, 0);
        transform.position = (Vector2)transform.position + scrollOffset;
    }
}
