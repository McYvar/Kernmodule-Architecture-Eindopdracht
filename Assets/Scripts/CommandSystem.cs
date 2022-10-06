using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSystem
{
    private List<KeyCommand> Keycommands = new List<KeyCommand>();

    PlayerCommand playercommand;
	Icommand handler;
	private GameObject Actor;
	
	public CommandSystem()
	{
		SetHandler(playercommand);
	}
	
	public CommandSystem(GameObject _Actor)
	{
		Actor = _Actor;
        playercommand = new PlayerCommand(_Actor.transform);

    }
	public void HandleInput(KeyCode _keyCode)
	{

        foreach (var keycommand in Keycommands)
        {
            if (keycommand.key == _keyCode)
            {
                keycommand.command.Execute();
            }
        }

        if (handler.Equals(playercommand) )
		{
            playercommand.Execute();
		}
		//adding more types of input down here

		//if(handler.Equals(AltCommand))
		//{ 
		//	AltCommand.Execute(Actor,_keyCode);
		//}
	}
	public void SetHandler(Icommand _Com)
	{
        Debug.Log(_Com);
        Debug.Log(playercommand.GetType());

        if (_Com.GetType() == playercommand.GetType())
		{
			Debug.Log("Handler = player");
			handler = (PlayerCommand) playercommand;
		}
        //extra inputs
        /*
		if(_Com.GetType() == .GetType())
		{
		Debug.Log("Handler = Arrow");
		//	handler = (ArrowCommands) AltCommand;
		}
        */
	}	
}
