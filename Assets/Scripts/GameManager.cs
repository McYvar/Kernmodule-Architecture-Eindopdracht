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
        ICommand ComMsg = new PlayerCommand(actor.transform);
        CommandSystem.Instance.SetHandler(ComMsg);
        ICommand space = new keyTransformCommand(KeyCode.Space,Vector3.up, actor.transform);
       CommandSystem.Instance.AddCommand(space, KeyCode.Space);
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
