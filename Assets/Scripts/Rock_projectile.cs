using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Rock_projectile : MonoBehaviour
{
    Player player;
    Animator anim;
    bool hit;
    FMOD.Studio.EventInstance rockHitSound;
    FMOD.ATTRIBUTES_3D attributes;
    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        anim = GetComponent<Animator>();
        hit = false;

        // Convertir la posición y orientación del GameObject a atributos 3D de FMOD
        attributes = RuntimeUtils.To3DAttributes(gameObject);




    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && !hit) {
            rockHitSound = FMODUnity.RuntimeManager.CreateInstance("event:/ThrowRock_Ground");
            attributes = RuntimeUtils.To3DAttributes(gameObject);
            rockHitSound.set3DAttributes(attributes);
            rockHitSound.start();
            rockHitSound.release();
            player.rock = null;
            anim.SetBool("Break", true);
            
        }
        if (collision.gameObject.tag == "Rock" && !hit)
        {
            rockHitSound = FMODUnity.RuntimeManager.CreateInstance("event:/ThrowRock_Ground");
            attributes = RuntimeUtils.To3DAttributes(gameObject);
            rockHitSound.set3DAttributes(attributes);
            rockHitSound.start();
            rockHitSound.release();
            player.rock = null;
            anim.SetBool("Break", true);
        }
        if (collision.gameObject.tag == "OneWayPlatform" && !hit)
        {
            rockHitSound = FMODUnity.RuntimeManager.CreateInstance("event:/ThrowRock_Wood");
            attributes = RuntimeUtils.To3DAttributes(gameObject);
            rockHitSound.set3DAttributes(attributes);
            rockHitSound.start();
            rockHitSound.release();
            player.rock = null;
            anim.SetBool("Break", true);
        }
    }

    public void DestroyRock()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            rockHitSound = FMODUnity.RuntimeManager.CreateInstance("event:/ThrowRock_Spikes");
            attributes = RuntimeUtils.To3DAttributes(gameObject);
            rockHitSound.set3DAttributes(attributes);
            rockHitSound.start();
            rockHitSound.release();
        }

        if (collision.gameObject.tag == "Water")
        {
            hit = true;
            rockHitSound = FMODUnity.RuntimeManager.CreateInstance("event:/ThrowRock_Water");
            attributes = RuntimeUtils.To3DAttributes(gameObject);
            rockHitSound.set3DAttributes(attributes);
            rockHitSound.start();
            rockHitSound.release();
            player.rock = null;
            anim.SetBool("Break", true);
        }
    }
}
