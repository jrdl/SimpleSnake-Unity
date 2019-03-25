using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public GameObject foodPrefab;

    public Transform borderLeft;
    public Transform borderRight;
    public Transform borderTop;
    public Transform borderBottom;

    // Use this for initialization
    void Start () {
        Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn()
    {
        int x = (int) Random.Range(borderLeft.position.x, borderRight.position.x);
        int y = (int)Random.Range(borderTop.position.y, borderBottom.position.y);

        GameObject food = Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;


    }
}
