using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sword : MonoBehaviour
{

    private int score;
    public TextMeshProUGUI scoreText;
    //private GameObject player;
    //public bool attacking = false;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score;
        //player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Enemy"){
            Destroy(other.gameObject);
            score += 1;
            scoreText.text = "Score: " + score;
            //GameObject.enabled = false;
        }
    }
}
