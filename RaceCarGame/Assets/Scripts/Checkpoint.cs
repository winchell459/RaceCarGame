using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public void OnTriggerEnter(Collider other)
	{
        if(other.CompareTag("Player")){
            FindObjectOfType<GameHandler>().CheckpointTriggered(gameObject);
        }
	}
}
