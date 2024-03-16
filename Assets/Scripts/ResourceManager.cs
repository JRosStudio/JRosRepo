using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int ropes = 0;

    public void addRopes(int ropesAdded){
        ropes = ropesAdded;
    }
    public int getCurrentRopes() {
        return ropes;
    }

}
