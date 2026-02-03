using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandoChildrenSelector : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject[] allChildren;
    int[] activateIndex;
    public int numberActivatedItems;



    void Start()
    {
        activateIndex = new int[numberActivatedItems];
        allChildren = new GameObject[gameObject.transform.childCount];

        for (int i = 0; i< allChildren.Length ; i++)
        {
            allChildren[i] = gameObject.transform.GetChild(i).gameObject;
            allChildren[i].SetActive(false);
        }

        int totalChildren = transform.childCount;

        // Seguridad por si piden más de los que existen
        numberActivatedItems = Mathf.Min(numberActivatedItems, totalChildren);

        activateIndex = new int[numberActivatedItems];

        // Lista con todos los índices posibles
        List<int> availableIndexes = new List<int>();
        for (int i = 0; i < totalChildren; i++)
            availableIndexes.Add(i);

        // Elegimos sin repetir
        for (int i = 0; i < numberActivatedItems; i++)
        {
            int randomPos = Random.Range(0, availableIndexes.Count);
            activateIndex[i] = availableIndexes[randomPos];
            availableIndexes.RemoveAt(randomPos); // lo quitamos para no repetir
        }

        foreach (int i in activateIndex) { 
            allChildren[i].gameObject.SetActive(true);
        }


    }
}
