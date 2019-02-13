﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreakerInstance : MonoBehaviour {

    public float rotateSpeed;
    GameSceneManager gmScript;

	void Start () {
        gmScript=GameObject.Find("GameManager").GetComponent<GameSceneManager>();
	}

    private void FixedUpdate() {
        transform.Rotate(new Vector3(0, 0, 1)*rotateSpeed);
    }

    /*private void OnTriggerEnter(Collider other) {
        if(other.tag =="Ghost") {
            Destroy(other.gameObject);
        } else if(other.tag =="Wall" &&!other.GetComponent<Cube>().IsBoundary()) {
            Cube wall = other.GetComponent<Cube>();
            gmScript.GmBreakWall(wall.x, wall.y);
        }
    }*/

    private void OnCollisionEnter(Collision other) {
        Debug.Log("WallBreaker Collide!");
        if (other.gameObject.tag=="Ghost") {
            Destroy(other.gameObject);
        } else if (other.gameObject.tag=="Wall"&&!other.gameObject.GetComponent<Cube>().isBoundary) {
            Cube wall = other.gameObject.GetComponent<Cube>();
            gmScript.GmBreakWall(wall.x, wall.y);
        }

        Destroy(this.gameObject);
        gmScript.GetPlayer().GetComponent<Player>().isUsingItem=false;
    }
}