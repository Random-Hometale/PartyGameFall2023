using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController1 : MonoBehaviour
{

    public static PlayerController1 instance;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float xLimit = 8.47f;

    [SerializeField]
    private Animator anim;

    private bool movingRight = true;
    private int direction = 1;
    private bool startMoving = false;

    public GameObject WinTextObject;
    public GameObject LoseTextObject;
    public bool gameOver = false;


    public bool StartMoving { get {return startMoving;}}

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && startMoving == false)
        {
            startMoving = true;
        }

        if (Input.GetKeyDown("space"))
        {
            movingRight = !movingRight;
            direction = -direction;
            transform.localScale = new Vector3(direction, 1, 1);
        }

        if (startMoving == false) return;

        ChangeDirection();

        transform.position += Vector3.right * moveSpeed * Time.deltaTime * direction;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xLimit, xLimit), transform.position.y, transform.position.z);
    
        anim.SetBool("Start", startMoving);
        
    }

    void ChangeDirection()
    {
        if (movingRight && transform.position.x >= xLimit)
        {
            movingRight = false;
            direction = -1;
            transform.localScale = new Vector3(direction, 1, 1);
        }

        if (!movingRight && transform.position.x <= -xLimit)
        {
            movingRight = true;
            direction = 1;
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            LoseTextObject.SetActive(true);
            gameOver = true;
        }

        if (gameOver == true)
        {
            moveSpeed = 0;
        }
    }
}
