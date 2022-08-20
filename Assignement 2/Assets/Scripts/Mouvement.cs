using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    [SerializeField] public float speed = 4.5f;

    private Animator animator;
    private Rigidbody2D body2d;
    private bool grounded = false;
    private bool moving = false;

    public GameObject dropPoint;
    float x;


    // Start is called before the first frame update
    void Start()
    {
        dropPoint= GameObject.Find("DropPoint");
        animator = GetComponent<Animator>();
       body2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        float inputRaw = Input.GetAxisRaw("Horizontal");
        if (inputRaw!=0)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (inputRaw > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            x = (this.transform.position.x+2);
            if (x < 0)
            {
                dropPoint.transform.position = new Vector3(x, 0, 0);
            }else if (x > 0)
            {
                dropPoint.transform.position = new Vector3(x, 0, 0);
            }
           
            
        }

        else if (inputRaw < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
                 x = (this.transform.position.x - 2);
            if (x < 0)
            {
                dropPoint.transform.position = new Vector3(x, 0, 0);
            }
            else if (x > 0)
            {
                dropPoint.transform.position = new Vector3(x, 0, 0);
            }
          
          
        }
        animator.SetBool("isMoving", moving);
        body2d.velocity = new Vector2(speed*input, 0);
    }
}
