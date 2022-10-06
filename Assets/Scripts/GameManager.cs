using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	CommandSystem CommandSystem;
	public GameObject actor;
	
    // Start is called before the first frame update
    void Start()
    {
        CommandSystem = new CommandSystem(actor);
        Icommand ComMsg = new PlayerCommand(actor.transform);
        CommandSystem.SetHandler(ComMsg);
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
			CommandSystem.HandleInput(e.keyCode);
		}
	}
}
