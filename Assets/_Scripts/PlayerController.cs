using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    PlayerAction inputAction;
    Vector2 move;
    Vector2 rotate;
    Rigidbody rb;

    private float distanceToGround;
    bool isGrounded = true;
    public float jump = 5f;
    public float walkSpeed = 5f;
    public Camera playerCamera;
    Vector3 cameraRotation;

    private Animator animator;
    private bool isWalking = false;

    public GameObject projectile;
    public GameObject coinCat;
    public Transform coinCatPosition;

    public Transform projectilePos;

    public float bounceForce = 5f;
    public float playerHeath = 100;
    public TextMeshProUGUI playerHealthUI;

    private void OnEnable() {
        inputAction.Enable();
    }

    private void OnDisable() {
        inputAction.Disable();
    }

    private void OnCollisionEnter(Collision other)
    {
        //bouncer
        if (other.collider.tag == "Bouncer")
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
        }

        //player health
        if (other.collider.tag == "Bullet")
        {
            if (playerHeath == 0)
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                playerHeath -= 25;

            }
            playerHealthUI.text = "Health: " + playerHeath.ToString();
            Debug.Log(playerHeath);


        }
        //final coin check to end game
        if (other.collider.tag == "FinalCoin")
        {
            Application.Quit();
           // PauseGame();
        }
    }

    private void Awake() {

        inputAction = new PlayerAction();

        inputAction.Player.Jump.performed += cntxt => Jump();

        inputAction.Player.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        inputAction.Player.Move.canceled += cntxt => move = Vector2.zero;

        inputAction.Player.Look.performed += cntxt => rotate = cntxt.ReadValue<Vector2>();
        inputAction.Player.Look.canceled += cntxt => rotate = Vector2.zero;

        inputAction.Player.Shoot.performed += cntxt => Shoot();      

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        distanceToGround = GetComponent<Collider>().bounds.extents.y;
        cameraRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        
    }

    public void Jump()
    {
        if(isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isGrounded = false;
        }

        Vector3 coinPostion = new Vector3(coinCatPosition.transform.position.x * Random.value, coinCatPosition.transform.position.y, coinCatPosition.transform.position.z * Random.value);
        Coin.instance.Spawn(coinCat, coinPostion);

    }

    private void Shoot()
    {
        Rigidbody bulletRb = Instantiate(projectile, projectilePos.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        bulletRb.AddForce(transform.up * 1f, ForceMode.Impulse);

        
    }

    // Update is called once per frame
    void Update()
    {
        cameraRotation = new Vector3(cameraRotation.x + rotate.y, cameraRotation.y + rotate.x, cameraRotation.z);
        
        playerCamera.transform.rotation = Quaternion.Euler(cameraRotation);
        transform.eulerAngles = new Vector3(transform.rotation.x, cameraRotation.y, transform.rotation.z);

        transform.Translate(Vector3.right * Time.deltaTime * move.x * walkSpeed, Space.Self);
        transform.Translate(Vector3.forward * Time.deltaTime * move.y * walkSpeed, Space.Self);

        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround);

        Vector3 m = new Vector3(move.x, 0, move.y);
        AnimateRun(m);
    }

    void AnimateRun(Vector3 m)
    {
        isWalking = (m.x > 0.1f || m.x < -0.1f) || (m.z > 0.1f || m.z < -0.1f) ? true : false;
        animator.SetBool("isWalking", isWalking);
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
