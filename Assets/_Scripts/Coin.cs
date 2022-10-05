using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{/*
    private void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "Player"){
            ScoreManager.instance.ChangeScore(1);
            Destroy(gameObject);
        }  
    }*/

    public static Coin instance;

    public void Spawn(GameObject other, Vector3 other1)
    {
        Rigidbody coinRb = Instantiate(other, other1, Quaternion.identity).GetComponent<Rigidbody>();
        ScoreManager.instance.ChangeScore(1);

    }

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

}
