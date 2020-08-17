using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatTankScript : MonoBehaviour //Probably don't need this, will look into removing the dependancy at a later time.
{
    //Private Fields
    private float _currmoles;

    public float volume;            // Total volume of electrolite in liters
    public float chargePerMole;     // Should be -96520 for electrons (anode) 192640 for alpha particles (cathode)
    public float maxConcentration;  // Maximum concentration of moles/liter

    // Special properties
    public float moles
	{
		get{ return _currmoles; }
		set{ _currmoles = Mathf.Clamp(value, 0, maxConcentration * volume); }
	}           // This is the moles of the electrolite

    // Section for calculated properties
    public float concentration
	{
		get
		{
            return (moles / volume);
		}
	}   // molar concentration of the electrolite
    public float charge
    { 
        get { return chargePerMole * moles; }
        set { moles = value / chargePerMole; }
    }           // Total charge available provided by this tank

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
