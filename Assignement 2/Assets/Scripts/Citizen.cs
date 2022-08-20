using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    private enum citizenState
    {
        Normal,
        mNormal,
        Vac,
        Sus,
        mSus,
        Infected
        
    }
    
    public Sprite normalSprite;
    public Sprite mNormalSprite;
    public Sprite vacSprite;
    public Sprite susSprite;
    public Sprite mSusSprite;
    public Sprite infectedSprite;

    public static int normalTotal=0;
    public static int mNormalTotal = 0;
    public static int vacTotal = 0;
    public static int susTotal = 0;
    public static int mSusTotal = 0;
    public static int infectedTotal = 0;
    public static float speedMultipler=1f;

    

    public int selectedSprite;
    private SpriteRenderer currentSprite;
    private Rigidbody2D body;

    public bool isGrabbed;
    private GameObject playerGrab;
    public GameObject virus;

    private AudioSource fx;
    public AudioClip destroyMask;
    public AudioClip sick;

    // Start is called before the first frame update
    void Start()
    {
        currentSprite = this.GetComponent<SpriteRenderer>();
        currentSprite.enabled = false;
        fx = this.GetComponent<AudioSource>();
        playerGrab = GameObject.Find("GrabPoint");
        isGrabbed = false;
        ArrayList list = new ArrayList();
       
        if (normalTotal < SliderText.nNormal)
        {
            list.Add(0);
        }
        if (mNormalTotal < SliderText.nNormalM)
        {
            list.Add(1);
        }
        if (vacTotal < SliderText.nVac)
        {
            list.Add(2);
        }
        if (susTotal < SliderText.nSus)
        {
            list.Add(3);
        }
        if (mSusTotal < SliderText.nSusM)
        {
            list.Add(4);
        }
        if (infectedTotal < SliderText.nInfected)
        {
            list.Add(5);
        }
       
        if (list.Count == 0)
        {
           Destroy(this.gameObject);
           return;
        }

        body = this.GetComponent<Rigidbody2D>();
        
        
        currentSprite.enabled = true;
        int index = Random.Range(0, list.Count);
        //Debug.Log(selectionList.Count);
       
        selectedSprite = (int)list[index];
        
        //for (int i=0;i< selectionList.Count; i++)
        // {
        //  Debug.Log(selectionList[i]);
        // }
        InvokeRepeating("MoveAround", 0.5f, 2f);
        InvokeRepeating("RemoveMask", 5f, 5f);
        InvokeRepeating("SpreadDisease", 2f, 3f);
        ChangeSprite();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbed)
        {
            this.transform.position = playerGrab.transform.position;
        }
    }

   public void editSprite(int sprite)
    {
        switch (sprite)
        {
            case 0:
                selectedSprite = 0;
                currentSprite.sprite = normalSprite;

                break;
            case 1:
                selectedSprite = 1;
                currentSprite.sprite = mNormalSprite;

                break;
            case 2:
                selectedSprite = 2;
                currentSprite.sprite = vacSprite;

                break;
            case 3:
                selectedSprite = 3;
                currentSprite.sprite = susSprite;

                break;
            case 4:
                selectedSprite = 4;
                currentSprite.sprite = mSusSprite;

                break;
            case 5:
                selectedSprite = 5;
                currentSprite.sprite = infectedSprite;

                break;
        }
    }



    void ChangeSprite()
    {  
        switch (selectedSprite)
        {
            case 0:
                normalTotal++;
                currentSprite.sprite = normalSprite;
                
                break;
            case 1:
                mNormalTotal++;
                currentSprite.sprite = mNormalSprite;
                
                break;
            case 2:
                vacTotal++;
                currentSprite.sprite = vacSprite;
                
                break;
            case 3:
                susTotal++;
                currentSprite.sprite = susSprite;
                
                break;
            case 4:
                mSusTotal++;
                currentSprite.sprite = mSusSprite;
               
                break;
            case 5:
                infectedTotal++;
                currentSprite.sprite = infectedSprite;
                
                break;
        }
    }
    void MoveAround()
    {
        if (!isGrabbed)
        {
            int orientation = Random.Range(-1, 2);
            float speed = Random.Range(1f, 3f) * speedMultipler;
            body.velocity = new Vector2(orientation * (speed), 0);
        }
       
    }


    void RemoveMask()
    {
        int randomizer = Random.Range(0, 4);
        if (randomizer == 0)
        {
            if (selectedSprite == 1 || selectedSprite == 4)
            {
                editSprite(selectedSprite - 1);
            }
        }
        
    }

    public static void IncreaseCitizen()
    {
        
        if (!LoadLevel.special)
        {
            SliderText.nNormal += 1;
            SliderText.nNormalM += 1;
            SliderText.nSus += 1;
            SliderText.nSusM += 1;
            SliderText.nVac += 1;
            SliderText.nInfected += 1;
        }
        else
        {
            SliderText.nNormal += 2;
            SliderText.nNormalM += 2;
            SliderText.nSus += 2;
            SliderText.nSusM += 2;
            SliderText.nVac += 2;
            SliderText.nInfected += 2;
        }

    }

    public static void ResetCitizen()
    {
        normalTotal = 0;
        mNormalTotal = 0;
        vacTotal = 0;
        susTotal = 0;
        mSusTotal = 0;
        infectedTotal = 0;

        SliderText.nNormal -= 2;
        SliderText.nNormalM -= 2;
        SliderText.nSus -= 2;
        SliderText.nSusM -= 2;
        SliderText.nVac -= 2;
        SliderText.nInfected -= 2;
    }

    public int getSprite()
    {
        return selectedSprite;
    }

    public void setGrabbed(bool state)
    {
        isGrabbed = state;
    }

    public int getInfectedTotal()
    {
        return infectedTotal;
    }

    public void setInfectedTotal(int amount)
    {
        infectedTotal = amount;
    }
    public int getNormalTotal()
    {
        return normalTotal;
    }

    public void setNormalTotal(int amount)
    {
        normalTotal = amount;
    }


    public int getNormalMTotal()
    {
        return mNormalTotal;
    }

    public void setNormalMTotal(int amount)
    {
        mNormalTotal = amount;
    }

    public int getSusTotal()
    {
        return susTotal;
    }

    public void setSusTotal(int amount)
    {
        susTotal = amount;
    }

    public int getSusMTotal()
    {
        return mSusTotal;
    }

    public void setSusMTotal(int amount)
    {
        mSusTotal = amount;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (this.getSprite()==5 && this.isGrabbed)
        {
            if (collision.gameObject.layer == 4)
            {
                GiveMasks.score += 5;
                collision.gameObject.GetComponent<AudioSource>().Play();
                GameObject.FindGameObjectWithTag("Player").GetComponent<GiveMasks>().isGrabbing = false;
                GameObject.FindObjectOfType<EnemySpawn>().setAmountOfPeople(GameObject.FindObjectOfType<EnemySpawn>().amountOfPeople()-1);
                Destroy(this.gameObject);
                
            }
        }


    }
    
    public void SpreadDisease()
    {
        if (selectedSprite == 5)
        {
            int randomizer = Random.Range(0, 3);

            if (randomizer == 0)
            {
                GiveMasks.score -= 1;
                Instantiate(virus, new Vector3(this.transform.position.x, 1.15f, 0), Quaternion.identity);
                fx.PlayOneShot(sick);
            }
        }
    }
}
