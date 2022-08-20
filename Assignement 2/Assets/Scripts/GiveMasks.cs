using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiveMasks : MonoBehaviour
{
    public float givingRange=3f;


    public TextMeshProUGUI scoreBoard;
    public static int score = 0;


    public LayerMask citizenLayer;
    public LayerMask refillLayer;
    public LayerMask virusLayer;


    public Transform centerPoint;
    public int currentAmount = 2;



    public GameObject mask1;
    public GameObject mask2;

    public GameObject pepsi;

    public Transform lockPoint;
    public Transform returnPoint;
    public bool isGrabbing;
    public GameObject victim;

    private AudioSource fx;
    public AudioClip pickup;
    public AudioClip drop;
    public AudioClip success;
    public AudioClip clean;
    public AudioClip soda;


    private float initSpeed;
    public bool isCrack;
    public static bool canDrink;
    public bool power;



    // Start is called before the first frame update
    void Start()
    {
        power = false;
        canDrink = true;
        isCrack = false;
        fx = this.GetComponent<AudioSource>();
        score = 0;
        this.gameObject.GetComponent<Mouvement>().enabled = true;
        Time.timeScale = 1f;
        Cursor.visible = true;
        isGrabbing = false;
        currentAmount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentAmount)
        {
            case 2:
                mask1.SetActive(true);
                mask2.SetActive(true);
                break;
            case 1:
                mask1.SetActive(true);
                mask2.SetActive(false);
                break;

            case 0:
                mask1.SetActive(false);
                mask2.SetActive(false);
                break;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GiveMask();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RefillMask();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickupInfected();
        }

        if (Input.GetKeyDown(KeyCode.Q)||power)
        {
            CleanInfected();
        }
        scoreBoard.SetText("Score: " + score);


        if (LoadLevel.special)
        {
            if (canDrink)
            {
                pepsi.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    this.GetComponent<Mouvement>().speed *= 3;
                    power = true;
                    isCrack = true;
                    canDrink = false;
                    fx.PlayOneShot(soda);
                }
            }
            else
            {
                pepsi.SetActive(false);
            }
            if (isCrack)
            {
                StartCoroutine(TimerRoutine());
            }
        }
       
        



    }

    private IEnumerator TimerRoutine()
    {
        isCrack = false;
        yield return new WaitForSeconds(10);
        this.GetComponent<Mouvement>().speed /= 3;
        power = false;

    }

    private void CleanInfected()
    {
        if (!isGrabbing)
        {
            Collider2D virusInRange = Physics2D.OverlapCircle(centerPoint.position, givingRange, virusLayer);
            if (virusInRange != null)
            {
                fx.PlayOneShot(clean);
                if (LoadLevel.special)
                {
                    score += 1;
                }
                else
                {
                    score += 2;
                }
                
                Destroy(virusInRange.gameObject);
            }
        }
        }

    void GiveMask()
    {
        if (!isGrabbing)
        {
            int amountGiven = 0;
            Collider2D[] citizensInRange = Physics2D.OverlapCircleAll(centerPoint.position, givingRange, citizenLayer);
            int counter = 0;
            if (currentAmount == 0)
            {
                return;
            }
            foreach (Collider2D citizen in citizensInRange)
            {
                if (citizen.GetComponent<Citizen>().getSprite() == 0 || citizen.GetComponent<Citizen>().getSprite() == 3)
                {
                    if (counter < 2)
                    {
                        amountGiven++;
                        score++;
                        citizen.gameObject.GetComponent<Citizen>().editSprite(citizen.gameObject.GetComponent<Citizen>().getSprite() + 1);
                        currentAmount--;
                        fx.PlayOneShot(success);
                        if (amountGiven >= 2)
                        {
                            score+=2;
                        }
                        if (currentAmount == 0)
                        {
                            return;
                        }
                        counter++;
                    }
                }
            }

        }

           
    }

    void RefillMask()
    {
     
        Collider2D[] inRange = Physics2D.OverlapCircleAll(centerPoint.position, givingRange, refillLayer);
        if (inRange.Length > 0)
        {
            currentAmount = 2;
            fx.PlayOneShot(success);
        }


    }

    void PickupInfected()
    {
        if (!isGrabbing)
        {
            Collider2D[] inRange = Physics2D.OverlapCircleAll(centerPoint.position, givingRange, citizenLayer);
            bool found=false;
            foreach (Collider2D citizen in inRange)
            {
               if(citizen.GetComponent<Citizen>().getSprite() == 5)
                {
                    found = true;
                    victim = citizen.gameObject;
                }
            }
            if (!found) return;
            victim.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            victim.transform.position = lockPoint.position;
            victim.GetComponent<Citizen>().setGrabbed(true);
            victim.transform.SetParent(this.transform);
            fx.PlayOneShot(pickup);
            isGrabbing = true;
            }
        else
        {
            fx.PlayOneShot(drop);
            victim.transform.position = returnPoint.position;
            victim.GetComponent<Citizen>().setGrabbed(false);
            victim.transform.SetParent(null);
            isGrabbing = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(centerPoint.position, givingRange);
        
    }
}
