using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class raycast : MonoBehaviour
{
    void CheckforRaycasthit()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Debug.Log(hit.collider.gameObject.name + " was destroyed!");

            if (hit.collider.gameObject.name == "Cube")
            {
                GameObject.Find(hit.collider.gameObject.name).GetComponent<Renderer>().material.color = new Color(Random.Range(1,2), 0, Random.Range(1,2));
             
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            CheckforRaycasthit();

        }
        


    }
}
