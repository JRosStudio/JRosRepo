using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButtonOnEnable : MonoBehaviour
{
    [SerializeField] Button button;

    private void OnEnable()
    {
        button.Select();
    }
}
