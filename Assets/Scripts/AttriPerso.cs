using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This script done by Adrian Wisniewski displays the names of the classes in the character selection.
 * It was intented to be the place where all class attributes (such as attack range, health, ...)
 * are loacted. Due to lack of time, it couldn't be done.
 */

public class AttriPerso : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class Pers
    {
        public string classe;
    }


    public Pers Archer = new Pers
    {
        classe = "Archer",
    };


    public Pers Brute = new Pers
    {
        classe = "Brute",
    };
}
