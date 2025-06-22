using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class camara : MonoBehaviour
{
	
	//public CinemachineFreeLook flc;
	public float velo = 60f,bel=1f;
	
	void Start()
	{
		//sonido = GetComponent<AudioSource>();
		
	}
	void OnCollisionStay()
    {
		
	}

	void FixedUpdate()//esto es para agregarle suavidad al movimiento de la camara
	{
		/*float hori = Input.GetAxis(""); 
		float verti = Input.GetAxis("");
		flc.m_XAxis.Value+=hori*velo*Time.deltaTime;
		flc.m_YAxis.Value+=verti*bel*Time.deltaTime;*/
	}
}