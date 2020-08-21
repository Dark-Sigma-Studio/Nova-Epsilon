using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Synth : PlayerRace.Race
{
	public struct Battery
	{
		public Electrode Anode;
		public Electrode Cathode;
		public float volts;

		public float charge 
		{ 
			get { return Cathode.charge - Anode.charge; }
			set
			{
				float delta = (value - charge) / 2;
				Cathode.charge += delta;
				Anode.charge -= delta;
			}
		}
		public float energy 
		{ 
			get { return charge * volts; }
			set{ charge = value / volts; }
		}

		public struct Electrode
		{
			public float moles;
			public float chargepermole;
			public float volume;
			public float maxConcentration;

			public float charge
			{
				get { return moles * chargepermole; }
				set { moles = Mathf.Max(0, value / chargepermole); }
			}

			public float concentration
			{
				get { return moles / volume; }
				set { moles = Mathf.Clamp( value * volume, 0, maxConcentration * volume); }
			}
		}
	}

	Battery battery = new Battery();

	public float idleWatts = 250;
	public float baseWatts = 250;
	public float burstWatts = 500;
	public float jumpCost = 10000;
	public Text display;

	public Material anodeMat;
	public Material cathodeMat;

	public override void Init()
	{
		display = ui.transform.Find("Text").GetComponent<Text>();

		anodeMat = self.transform.Find("SynthBase").Find("Battery").Find("Anode").GetComponent<MeshRenderer>().material;
		cathodeMat = self.transform.Find("SynthBase").Find("Battery").Find("Cathode").GetComponent<MeshRenderer>().material;

		battery.volts = 12;

		battery.Anode.chargepermole = -10000;
		battery.Cathode.chargepermole = 10000;

		battery.Anode.volume = 2;
		battery.Cathode.volume = 2;

		battery.Anode.maxConcentration = 25;
		battery.Cathode.maxConcentration = 25;

		battery.Anode.concentration = 12;
		battery.Cathode.concentration = 12;
	}
	public override void Update()
	{
		float power = 0;

		if(hasJumped)
		{
			battery.energy -= jumpCost;
			hasJumped = false;
		}

		power = moveMag * baseWatts + idleWatts;

		battery.energy -= power * Time.deltaTime;

		anodeMat.color = Color.Lerp(new Color(0, 0, 1, 0), new Color(0, 0, 1, 1), (battery.Anode.concentration * 2) / (battery.Anode.concentration + 3));
		cathodeMat.color = Color.Lerp(new Color(1, 0, 0, 0), new Color(1, 0, 0, 1), (battery.Cathode.concentration * 2) / (battery.Cathode.concentration + 3));

		display.text = $"{battery.energy: 0}\nTime Remaining: {TimeSpan.FromSeconds(battery.energy / power).ToString(@"hh\:mm\:ss")}";
	}
}
