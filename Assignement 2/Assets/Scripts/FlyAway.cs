using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAway : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5f);
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Random.Range(4,7));
    }
}
