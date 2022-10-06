using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//inspired on: https://sj-jason-liu.medium.com/game-design-pattern-command-pt-2-game-dev-series-137-f7c052304812
public class CommandSystem
{
    private static CommandSystem _instance;
    public static CommandSystem Instance
    {
        get { if (_instance == null) Debug.LogError("CommandSystem is NULL!"); return _instance; }
    }
    private List<Icommand> _commandBuffer = new List<Icommand>();

    PlayerCommand playercommand;
	Icommand handler;
	private GameObject Actor;

	public CommandSystem(GameObject _Actor)
	{
        _instance = this;
		Actor = _Actor;
        playercommand = new PlayerCommand(_Actor.transform);
      //  SetHandler(playercommand);
    }
	public void HandleInput(KeyCode _keyCode)
	{
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
