using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    public GameObject Anode;
    public GameObject Cathode;

    public GameObject player;

    public float volts = 12;        // This happens to be rather arbitrary come to find out. I really can't find much info on finding out how to calculate the voltage for some of these things, so, 12 seems to be fine. We can actually tweek this later to better formulate gameplay.

    public float charge
	{
		get
		{
            return Cathode.GetComponent<BatTankScript>().charge - Anode.GetComponent<BatTankScript>().charge;   // Cool thing here, we are finding the difference in charge here, so it's just the cathode charge minus the anode charge, so because the anode is negetive, you end up adding the two together, effectivly
		}
	}

    public float TimeLeft;

	#region Methods
    public float CalcTimeRemaining(float amps)
	{
        return (charge / amps) / 2;
	}
	#endregion

	// Start is called before the first frame update
	// might not be neccessary to use, will keep anyway for future possible functionality.
	void Start()
    {
        Anode.GetComponent<BatTankScript>().moles = 6;
        Cathode.GetComponent<BatTankScript>().moles = 3;
    }

    // Update is called once per frame
    void Update()
    {
        float watts = player.GetComponent<PlayerStats>().PowerIdle;   // initialize to the base idle power need for the player
        float dt = Time.deltaTime;                                    // Let's just hold the delta time here as it'll be easier to work with later

        float amps = watts / volts; // this is a well known formula as watts = amps * volts
        float dc = amps * dt;       // amps = charge / time, therefore, delta charge is the amperage times delta time

        Anode.GetComponent<BatTankScript>().charge += dc;       // Since the anode is negative, you add charge to it to get it to be neutral
        Cathode.GetComponent<BatTankScript>().charge -= dc;     // Same thing, but the cathode is positive, so you subtract charge from it.

        TimeLeft = CalcTimeRemaining(amps);
    }
}
