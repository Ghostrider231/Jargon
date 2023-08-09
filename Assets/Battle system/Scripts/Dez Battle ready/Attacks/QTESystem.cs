using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTESystem : MonoBehaviour
{
    public GameObject DisplayBOX;
    public GameObject PassBox;
    public int QTEGenertator;
    public int WaitForKey;
    public int CorrectKey;
    public int CountingDown;

    private void Update()
    {
        if(WaitForKey == 0)
        {
            QTEGenertator = Random.Range(1, 4);
            CountingDown = 1;
            StartCoroutine(CountDown());

            if(QTEGenertator == 1)
            {
                WaitForKey = 1;
                DisplayBOX.GetComponent<Text>().text = "[E]";
            }

            if(QTEGenertator == 2)
            {
                WaitForKey = 1;
                DisplayBOX.GetComponent<Text>().text = "[R]";
            }

            if(QTEGenertator == 3)
            {
                WaitForKey = 1;
                DisplayBOX.GetComponent<Text>().text = "[T]";
            }
        }

        if(QTEGenertator == 1)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("EKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }

        if (QTEGenertator == 2)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("RKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }

        if (QTEGenertator == 3)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("TKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }



    }

    IEnumerator KeyPressing()
    {
        QTEGenertator = 9;
        if(CorrectKey == 1)
        {
            CountingDown = 2;
            PassBox.GetComponent<Text>().text = "Pass!";
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            DisplayBOX.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            WaitForKey = 0;
            CountingDown = 1;

        }

        if (CorrectKey == 2)
        {
            CountingDown = 2;
            PassBox.GetComponent<Text>().text = "Fail!";
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            DisplayBOX.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            WaitForKey = 0;
            CountingDown = 1;

        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(3.5f);
        if (CountingDown == 1)
        {
            QTEGenertator = 4;
            CountingDown = 2;
            PassBox.GetComponent<Text>().text = "Fail!";
            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            DisplayBOX.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            WaitForKey = 0;
            CountingDown = 1;
        }

    }

}
