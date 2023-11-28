using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private string scenename;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Goal!");
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(scenename);
        }
    }
}
