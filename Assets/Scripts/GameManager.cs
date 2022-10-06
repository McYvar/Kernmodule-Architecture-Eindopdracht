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
       // CommandSystem = new CommandSystem(actor);
        Icommand ComMsg = new PlayerCommand(actor.transform);
        CommandSystem.Instance.SetHandler(ComMsg);
        Icommand ComMsg2 = new keymashCommand();
       CommandSystem.Instance.AddCommand(ComMsg2,KeyCode.S);
    }

    // Update is called once per frame
    void Update()
    {
	   if(Input.GetMouseButtonDown(0)) //convert mouse input to keycodes
        {
            CommandSystem.Instance.HandleInput(KeyCode.Mouse0);
        }
    }
    
    
	void OnGUI() //currently used to update keybindings, no mouse support
	{
		Event e = Event.current;
		if (e.isKey)
		{
           
			CommandSystem.Instance.HandleInput(e.keyCode);
		}
	}
}
