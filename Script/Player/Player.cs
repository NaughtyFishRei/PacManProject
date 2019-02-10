﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int maxItemsNumber;
    public string[] itemsNameList;
    public bool isUsingItem;
    public GameObject wallBreaker;
    public GameObject portalAInstance;
    public GameObject portalBInstance;
    public GameObject laserInstance;
    public GameObject laserChildInstance;
    public float laserMaxTime;
    public float laserLastTime;
    public bool isUsingLaser;

    private Item[] itemsList;
    [SerializeField]private int energy = 0;
    [SerializeField]private int ownedItems = 0;
    private GameSceneManager gameManagerScript;

	void Start () {
        gameManagerScript=GameObject.Find("GameManager").GetComponent<GameSceneManager>();
        maxItemsNumber=gameManagerScript.maxItemsNumber;
        itemsList=new Item[maxItemsNumber];
        itemsNameList=new string[maxItemsNumber];
        GetItem("Laser");
    }
	
	void Update () {
        CheckItemButton();
        if (isUsingLaser) {
            laserLastTime=laserLastTime+Time.deltaTime;
            Debug.Log(laserLastTime);
        }
        if(laserLastTime >=laserMaxTime) {
            isUsingLaser=false;
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*1000, Color.red);
    }

    private void FixedUpdate() {
        if (isUsingLaser) {
            CreateLaserInstance();
        }
    }

    public void UseItem(int index) {
        if (itemsList[index]==null) {
            Debug.Log("No available item");
            return;
        }
        string itemName = itemsList[index].GetItemName();
        CreateItemInstance(itemName);

        Debug.Log(itemsNameList[index]+" is used");
        itemsList[index]=null;
        itemsNameList[index]=null;
        ownedItems--;
        gameManagerScript.PlayerUseItem(index);

        if(itemName =="Portal") {
            GetItem("PortalB");
        }
    }

    public void GetItem(string itemName) {
        Item item = ScriptableObject.CreateInstance<Item>();
        item.SetName(itemName);
        for (int i =0; i<maxItemsNumber; i++) {
            if(itemsList[i] ==null) {
                itemsList[i]=item;
                itemsNameList[i]=item.GetItemName();
                ownedItems++;
                gameManagerScript.PlayerGetItem(i, itemName);
                break;
            }
        }
    }

    public void AddEnergy(int number) {
        energy=energy+number;
    }

    public bool ItemsListHasSpace() {
        return ownedItems<maxItemsNumber;
    }

    private void CheckItemButton() {
        if (isUsingItem) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Debug.Log("1 is pressed");
            UseItem(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log("2 is pressed");
            UseItem(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            Debug.Log("3 is pressed");
            UseItem(2);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            Debug.Log("4 is pressed");
            UseItem(3);
        } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            Debug.Log("5 is pressed");
            UseItem(4);
        } else if (Input.GetKeyDown(KeyCode.Alpha6)) {
            Debug.Log("6 is pressed");
            UseItem(5);
        } else if (Input.GetKeyDown(KeyCode.Alpha7)) {
            Debug.Log("7 is pressed");
            UseItem(6);
        } else if (Input.GetKeyDown(KeyCode.Alpha8)) {
            Debug.Log("8 is pressed");
            UseItem(7);
        }
    }

    private void CreateItemInstance(string name) {
        if(name =="WallBreaker") {
            CreateWallBreakerInstance();
        } else if(name =="Portal") {
            CreatePortalAInstance();
        } else if(name =="PortalB") {
            CreatePortalBInstance();
        } else if (name=="Laser") {
            laserLastTime=0;
            isUsingLaser=true;
        }
    }

    private void CreateWallBreakerInstance() {
        Instantiate(wallBreaker, this.transform);
    }

    private void CreatePortalAInstance() {
        Debug.Log("Portal Instance created!");
        Instantiate(portalAInstance, this.transform.position, Quaternion.Euler(0,0,0));
    }

    private void CreatePortalBInstance() {
        Debug.Log("PortalB Instance created!");
        Instantiate(portalBInstance, this.transform.position, Quaternion.Euler(0, 0, 0));
    }

    private void CreateLaserInstance() {
        RaycastHit hit;
        Debug.Log("Laser Instance created!");
        //GameObject laserParent = Instantiate(laserInstance,this.transform);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, 9)) {
            Debug.Log(hit.point);
            Debug.Log("Did Hit");
            Vector3 dir = transform.TransformDirection(Vector3.forward);
            Instantiate(laserChildInstance, this.transform.position, this.transform.rotation);
        }

    }
}
