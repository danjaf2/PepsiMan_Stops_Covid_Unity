using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (this.transform.position.x>43|| this.transform.position.x < -43)
        {
            Destroy(this.gameObject);
        }
    }
}
