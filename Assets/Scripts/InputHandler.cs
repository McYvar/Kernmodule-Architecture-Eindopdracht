using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
	//private List<KeyCommand> Keycommands = new	List<KeyCommand>();
	
	WASDCommands Command = new WASDCommands();
	ArrowCommands AltCommand = new ArrowCommands();
	Icommand handler;
	private GameObject Actor;
	
	public InputHandler()
	{
		SetHandler(Command);
	}
	
	public InputHandler(GameObject _Actor)
	{
		Actor = _Actor;
	}
	public void HandleInput(KeyCode _keyCode)
	{
		
		/*foreach (var keycommand in Keycommands)
			{
			if(keycommand.key == _keyCode )
			{
				keycommand.command.Execute();
			}
		}
		*/
		if(handler.Equals(Command) )
		{ 
			Command.Execute(Actor,_keyCode);
		}
			
		if(handler.Equals(AltCommand))
		{ 
			AltCommand.Execute(Actor,_keyCode);
		}
	}
	public void SetHandler(Icommand _Com)
	{
		//handler = _Com;
		if(_Com.GetType() == Command.GetType())
		{
			Debug.Log("Handler = WASD");
			handler = (WASDCommands) Command;
		}
		if(_Com.GetType() == AltCommand.GetType())
		{
		Debug.Log("Handler = Arrow");
			handler = (ArrowCommands) AltCommand;
		}
	}
	
}
/*
	if(Input.GetKeyDown(KeyCode.W)){command.Front();}
		if(Input.GetKeyDown(KeyCode.A)){command.Left() ;}
		if(Input.GetKeyDown(KeyCode.S)){command.Back() ;}
		if(Input.GetKeyDown(KeyCode.D)){command.Right();}
	
		if(Input.GetKeyDown(KeyCode.UpArrow)) {command.Front() ;}
		if(Input.GetKeyDown(KeyCode.LeftArrow)){command.Left() ;}
		if(Input.GetKeyDown(KeyCode.DownArrow)){command.Back() ;}
		if(Input.GetKeyDown(KeyCode.RightArrow)){command.Right();}
*/