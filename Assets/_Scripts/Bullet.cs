using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bullet : MonoBehaviour
{
      //  public float playerHeath = 100;
       //public TextMeshProUGUI playerHealthUI;

    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);

        if(other.collider.tag == "Player")
        {

        }
        else if(other.collider.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }

}

