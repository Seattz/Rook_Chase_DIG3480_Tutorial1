using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, RollABallControls.IPlayerActions
{
    private float speed = 20; 
    public Text countText;
    public Text winText;
    public Text livesText;
    public RollABallControls controls;
    public Vector2 motion;
    public GameObject player;

    private Rigidbody rb;
    private int count;
    private int lives;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText ();
        lives = 3;
        SetLivesText ();
        winText.text = "";
    }

    public void OnEnable() {
        if (controls == null) 
        {
            controls = new RollABallControls();

            controls.Player.SetCallbacks(this);
        }
        controls.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
       motion = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(motion.x, 0.0f, motion.y);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText ();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive (false);
            lives = lives - 1;  
            SetLivesText();
        }
        if (count == 10) 
        {
        transform.position = new Vector3(0.0f, 0.0f, -300.5f); 
        }
    }

    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString ();
        if (count >= 18)
        {
            winText.text = "You Win! Game created by Chase Rook.";
        }
    }

    void SetLivesText ()
    {
        livesText.text = "Lives: " + lives.ToString ();
        if (lives <= 0)
        {
            Destroy(player);
            winText.text = "You lose! Game created by Chase Rook.";
        }
    }
}
    

    