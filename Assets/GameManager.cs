using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	InputHandler InputSystem;
	public GameObject actor;
	WASDCommands command = new WASDCommands();
    // Start is called before the first frame update
    void Start()
    {
	    InputSystem = new InputHandler(actor);
	    InputSystem.SetHandler(command);
    }

    // Update is called once per frame
    void Update()
    {
	   
    }
    
    
	void OnGUI()
	{
		Event e = Event.current;
		if (e.isKey)
		{
			InputSystem.HandleInput(e.keyCode);
		}
	}
}
