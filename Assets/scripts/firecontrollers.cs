using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firecontrollers : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
   void OnTriggerEnter2D(Collider2D collision)
   {
       if (collision.gameObject.CompareTag("FireBall"))
       {
           Destroy(collision.gameObject);
           animator.SetTrigger("fireball");
       }
   }
}
