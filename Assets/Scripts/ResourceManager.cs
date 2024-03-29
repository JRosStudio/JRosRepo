using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int ropes = 0;
    [SerializeField]
    public TMP_Text ropeNumber;

    private void Update()
    {
        ropeNumber.text = ropes.ToString();
    }

    public void addRopes(int ropesAdded){
        ropes += ropesAdded;
    }
    public int getCurrentRopes() {
        return ropes;
    }

}
