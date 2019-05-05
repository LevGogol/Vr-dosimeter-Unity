using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bird : MonoBehaviour {
    [SerializeField] private GameObject leftWing;
    [SerializeField] private GameObject rightWing;

    private float height = 3f;
    
    private float leftDelay = 0.1f;
    private float rightDelay = 0.11f;

    [SerializeField] private float speed = 0.02f;
    private Vector3 direction = new Vector3(0, -1, -1);
    
     void Start()
    {
        
    }


    // Update is called once per frame
    void Update() {
        direction = Scope(direction);
        direction = direction.normalized;
        direction = Alignment(direction);
        direction = direction.normalized;
        
        transform.forward = direction; //повернуть тушку в сторону полёта
        transform.position += direction * speed;
        
        Winged();
        
        if (speed < 0.02f) {
            speed += speed * 0.01f;
        }
    }

    private Vector3 Alignment(Vector3 direction) {
        if (direction.y > 0.8f) {
            direction.y = 0.8f;
        } else if (direction.y < -0.8f) {
            direction.y = -0.8f;
        }

        return direction;
    }

    private Vector3 Scope(Vector3 direction) {
        float y1 = transform.position.y;
        float y = direction.y;
        if (y1 < 6f) {
            float o = -11/30f * y1 + 2.2f;  //6 -> 0, 3 -> 1.1
            y += y1 * o;
            speed -= speed * 0.03f;
            
        } else if (y1 < 3f) {
            y = 1000;
        }
        direction.y = y;

        float xs = transform.position.x;
        float ys = transform.position.y;
        float zs = transform.position.z;
        if (xs*xs + ys*ys + zs*zs > 15 * 15) {
            float k = 0.015625f * (xs*xs + ys*ys + zs*zs) - 3.515625f;

            direction.x += -xs * k;
            direction.y += -ys * k;
            direction.z += -zs * k;
            Debug.DrawRay(transform.position, direction);
            speed -= speed * 0.03f;
        }
        
        return direction;
    }
    
    private void Winged() {
        if (leftDelay < 0) {
            leftDelay = 0.1f;
            leftWing.SetActive(!leftWing.activeSelf);
        }

        if (rightDelay < 0) {
            rightDelay= 0.11f;
            rightWing.SetActive(!rightWing.activeSelf);
        }

        leftDelay -= Time.deltaTime;
        rightDelay -= Time.deltaTime;
    }
}
