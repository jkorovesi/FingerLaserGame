using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int enemyHealth;

	// Use this for initialization
	void Start () {
        enemyHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(enemyHealth == 0)
        {
            Destroy(gameObject);
        }
	}
}
