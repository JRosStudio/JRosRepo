using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBreakIcon : MonoBehaviour
{

    public Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void DeleteThis()
    {
        Destroy(gameObject);
    }

    public void setAnimBool(string name, bool state)
    {
        anim.SetBool(name, state);
    }
}

