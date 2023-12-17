using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour{

    public TextMeshProUGUI gameOverText;
    public GameObject swordHBPrefab;
    private GameObject swordHB;
    private Rigidbody playerRB;
    private bool isAlive = true;
    Animator animator;

    public float speed = 6.0f;
    public float rotationSpeed;


    void Start(){
        gameOverText.gameObject.SetActive(false);
        //swordCollider.enabled = false;
        playerRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update(){
        
        if(isAlive){
            if(!animator.GetBool("isAttacking")){
                walk();
            }
        }
        else{
            if(Input.GetKey("space")){
                SceneManager.LoadScene("Level 1");
            }
        }
        
    }

// if entering a collision with the enemy enable game over booleans
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            gameOverText.gameObject.SetActive(true);
            isAlive = false;
            animator.SetBool("gameOver", true);
        }
    }

    private void DestroyHitbox(){
        Destroy(swordHB);
    }


    void walk(){
        //if space is pressed and the sword hitbox doesnt exist
        if(Input.GetKey("space") && swordHB == null){
            //set animator variables
            animator.SetBool("isAttacking", true);
            animator.SetBool("isWalking", false);
            
            //instantiate sword hitbox prefab at player's position
            swordHB = Instantiate(swordHBPrefab, transform.position + transform.forward * 2f, transform.rotation);
            
            //wait 5 seconds to destroy the hitbox
            Invoke("DestroyHitbox", 0.5f);
            return;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        //set direction to horizontal and vertical input
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        direction.Normalize();

        //move player
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        
        //!!!!!NB. ERIC COMMENT YOUR CODE!!!!!!
        if (direction != Vector3.zero)
        {
            animator.SetBool("isWalking",true);
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);            
        }

        else{
            animator.SetBool("isWalking",false);
        }

    }
}

