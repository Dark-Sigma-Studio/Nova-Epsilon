using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRace : MonoBehaviour
{
    /*
     Player should have the script on it
     */

    public Race race;

    public class Race
    {
        public bool hasJumped;
        public float moveMag;
        public Canvas ui;
        public GameObject self;

        public virtual void Update() { }
        public virtual void Init() { }
    }



    // Start is called before the first frame update
    void Start()
    {
        race = new Synth();
        race.ui = GetComponentInChildren<Canvas>();
        race.self = gameObject;
        race.Init();
    }

    // Update is called once per frame
    void Update()
    {
        race.Update();
    }
}
