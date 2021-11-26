using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    private AudioSource[] doorSoundSources;
    [SerializeField]
    private AudioClip doorOpeningSound, doorClosingSound;

    // Start is called before the first frame update
    void Start()
    {
        //Assignation de son propre animator en tant que variable pour pouvoir y accéder plus simplement
        animator = GetComponent<Animator>();
    }

    
    
    
    //déclence l'animation d'ouverture des portes
    //Y intégrer le jeu d'un son ? Le lancement d'une corroutine ?
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Sound")
        {
            animator.SetBool("In", true);
            foreach (AudioSource source in doorSoundSources)
                source.PlayOneShot(doorOpeningSound);
        }
    }

    //déclence l'animation de fermeture des portes
    //Y intégrer le jeu d'un son ? Le lancement d'une corroutine ?
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Sound")
        {
            animator.SetBool("In", false);
            foreach (AudioSource source in doorSoundSources)
                source.PlayOneShot(doorClosingSound);
        }
    }

    //Créer une fonction publique à appeler lors d'un animation event ?

}
