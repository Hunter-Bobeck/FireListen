using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
	public InterpolationCurved.Curve curve;
	public ParticleSystem fire, embers;
	public int intensityRadius = 3;
	public int fires = 3, emberses = 3;
	public float fireAmountMax, fireAmountMin, embersAmountMax, embersAmountMin;
    private float fireAmount, embersAmount;

	private void Start()
	{
		fireAmount = fire.emission.rateOverTime.constant;
		embersAmount = embers.emission.rateOverTime.constant;
	}

	public void addFire()
	{
		if (fires < 6)
		{
			fires++;
		}
	}
	public void removeFire()
	{
		if (fires > 0)
		{
			fires--;
		}
	}
	public void addEmbers()
	{
		if (fires < 6)
		{
			fires++;
		}
	}
	public void removeEmbers()
	{
		if (emberses > 0)
		{
			emberses--;
		}
	}
	public void add()
	{
		print("added");
		addFire();
		addEmbers();
	}
	public void remove()
	{
		print("removed");
		removeFire();
		removeEmbers();
	}

	private void Update()
	{
		float fireRatio = (((float) fires) / ((float) ((intensityRadius * 2) + 1)));
		float embersRatio = (((float) emberses) / ((float) ((intensityRadius * 2) + 1)));
		fireAmount = InterpolationCurved.floatClamped(curve, fireAmountMin, fireAmountMax, fireRatio);
		embersAmount = InterpolationCurved.floatClamped(curve, embersAmountMin, embersAmountMax, embersRatio);

		ParticleSystem.EmissionModule emFire = fire.emission;
		ParticleSystem.MinMaxCurve rateFire = emFire.rateOverTime;
		rateFire.constantMin = fireAmount;
		rateFire.constantMax = fireAmount;
		emFire.rateOverTime = rateFire;

		ParticleSystem.EmissionModule emEmbers = fire.emission;
		ParticleSystem.MinMaxCurve rateEmbers = emEmbers.rateOverTime;
		rateEmbers.constantMin = fireAmount;
		rateEmbers.constantMax = fireAmount;
		emEmbers.rateOverTime = rateEmbers;
	}
}