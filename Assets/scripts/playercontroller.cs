using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class playercontroller : MonoBehaviour
{
    //liikkumismuuttujat
    private Rigidbody2D body;
    private Animator animator;
    private float moveSpeed = 5f;
    private Vector2 movement = new Vector2();
    // Start is called before the first frame update 
    private float horizontalMovement;
    private float verticalMovement;
    private bool grounded;
    private float jumpForce = 10f;
    private bool canClimb;
    private bool isClimbing;
    private Vector3 startPosition;
    void Start()
    {
      body = GetComponent<Rigidbody2D>(); 
      animator = GetComponent<Animator>();
      startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
           verticalMovement = Input.GetAxisRaw("Vertical");
           horizontalMovement =Input.GetAxisRaw("Horizontal");
       movement.x = horizontalMovement * moveSpeed; 
       
       if (horizontalMovement > 0)
       {
          transform.localScale = new Vector3(-1, 1, 1);
       }
       if (horizontalMovement < 0)
       {
          transform.localScale = new Vector3(1, 1, 1);
       }

       if (canClimb && verticalMovement !=0)
       {
          isClimbing = true;
       }
       else
       {
          isClimbing = false;
        
       }
       if (isClimbing)
       {
        movement.y = verticalMovement * moveSpeed;
        body.isKinematic = true;
       }
       else
       {
           movement.y = 0f;
           body.isKinematic = false;
       }
       if (Input.GetButtonDown("Jump") && grounded)
       {
          body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
       }
      animator.SetFloat("speed",Mathf.Abs(horizontalMovement));
      animator.SetBool("climbing",isClimbing);
       
    }
    void FixedUpdate()
    {
        transform.Translate(movement * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.gameObject.CompareTag("dragon"))
      {
        win();
      }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
      if(collision.gameObject.CompareTag("Platfrom"))
      {
        grounded = true;
      }
      
      if(collision.gameObject.CompareTag("ladder"))
      {
        canClimb = true;
      }
       
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        
        if(collision.gameObject.CompareTag("Platfrom"))
        {
           grounded = false;
        }
if(collision.gameObject.CompareTag("ladder"))
      {
        canClimb = false;
      }
    }
    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void win()
    {
      Debug.Log("You win!");
      //RestartScene();
      transform.position = startPosition;
    }
}