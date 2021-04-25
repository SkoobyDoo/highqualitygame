using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneWeak : MonoBehaviour
{

    public bool stillExists;
    // Start is called before the first frame update
    void Start()
    {
        stillExists = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stillExists)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!stillExists)
        {
            Destroy(gameObject);
        }
    }

}
