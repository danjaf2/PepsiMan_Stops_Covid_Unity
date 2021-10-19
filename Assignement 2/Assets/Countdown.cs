using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public GameObject death;
    public static int startingTime;
    private static int currentTime;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI level;
    private int levelCounter=0;
    public static bool levelCleared;


    public int mobSpawn1;
    public int mobSpawn2;


    public EnemySpawn[] spawners;
    


    // Start is called before the first frame update
    void Start()
    {
        spawners = FindObjectsOfType<EnemySpawn>();
        startingTime = 30;
        levelCleared = false;
        mobSpawn1 = Random.Range(5, startingTime-5);
        mobSpawn2 = Random.Range(5, startingTime - 5);
        level.SetText("Level: " + levelCounter);
        currentTime = startingTime;
        timer = this.GetComponent<TextMeshProUGUI>();
        timer.SetText(currentTime.ToString());
        InvokeRepeating("UpdateTime", 0f, 1f);

    }
    private void Update()
    {
        var allCitizens = FindObjectsOfType<Citizen>();
        if (allCitizens.Length == 0)
        {
            levelCleared=true;
        }

        if (currentTime== mobSpawn1)
        {
            Debug.Log("yo 1");
            int index=Random.Range(0, spawners.Length);
            spawners[index].SpawnMob();
            mobSpawn1 = -10;
        }
        if (currentTime == mobSpawn2)
        {
            Debug.Log("yo 2");
            int index = Random.Range(0, spawners.Length);
            spawners[index].SpawnMob();
            mobSpawn2 = -10;
        }
    }
    void UpdateTime()
    {
        currentTime--;
        timer.SetText(currentTime.ToString());
        resetGame();
    }



    void resetGame()
    {
        if (currentTime <= 0)
        {
            GiveMasks.canDrink = true;
            EnemySpawn.currentAmount = 0;
            var allCitizens = FindObjectsOfType<Citizen>();
            for (int i = 0; i < allCitizens.Length; i++)
            {
                levelCleared = false;
                int orientation=0;
                if (allCitizens[i].gameObject.transform.position.x >= 0)
                {
                    orientation = 1;
                }
                else
                {
                    orientation = -1;
                }
                if (allCitizens[i].isGrabbed == true)
                {
                    GameObject.FindObjectOfType<GiveMasks>().isGrabbing = false;
                }
                if (allCitizens[i].getSprite() == 5)
                {
                    Instantiate(death, new Vector3(allCitizens[i].transform.position.x, 0.787f, 0), Quaternion.identity);
                    Destroy(allCitizens[i].gameObject);
                    GiveMasks.score -= 10;
                }
                allCitizens[i].gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(orientation * 30, 0);
                allCitizens[i].gameObject.GetComponent<BoxCollider2D>().enabled = false;
                allCitizens[i].CancelInvoke();
            }
            Citizen.speedMultipler += 0.5f;
            Citizen.IncreaseCitizen();
            EnemySpawn.maximumAmount = SliderText.nSus + SliderText.nSusM + SliderText.nVac + SliderText.nInfected + SliderText.nNormal + SliderText.nNormalM;
            Citizen.ResetCitizen();
            
            levelCounter += 1;
            level.SetText("Level: " + levelCounter);
            Debug.Log(Citizen.infectedTotal);
            currentTime = startingTime + 6;
            mobSpawn1 = Random.Range(5, currentTime - 5);
            mobSpawn2 = Random.Range(5, currentTime - 5);

        }
    }
}
