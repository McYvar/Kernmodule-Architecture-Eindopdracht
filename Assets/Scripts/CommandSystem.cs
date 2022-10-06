using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//inspired on: https://sj-jason-liu.medium.com/game-design-pattern-command-pt-2-game-dev-series-137-f7c052304812
public class CommandSystem
{
    PlayerCommand RefrenceType = new PlayerCommand();
    Icommand handler;
    //private GameObject Actor;

    private static CommandSystem _instance = new CommandSystem();
    public static CommandSystem Instance
    {
        get { if (_instance == null) Debug.LogError("CommandSystem is NULL!"); return _instance; }
    }
    private Dictionary<KeyCode,Icommand> _commandBuffer = new Dictionary<KeyCode,Icommand>();

   

	public CommandSystem()
	{
        _instance = this;
      
    }

	public void HandleInput(KeyCode _keyCode)
	{
        //Debug.Log(_keyCode);

        if (_keyCode == KeyCode.Mouse0)
        {
            Debug.Log("start raycast" + Input.mousePosition);
            Ray rayorigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayorigin,out hitInfo))
            {
                if(hitInfo.collider.tag == "Cube")
                {
                    //we clicked a cube
                    Debug.DrawRay(Camera.main.transform.position, hitInfo.transform.position, Color.green, 10f);

                    Debug.Log("raycast detected object in scene");
                }
            }
        }
        if(_commandBuffer.ContainsKey(_keyCode)) //if the commandbuffer knows the command execute that command
        {
            _commandBuffer.GetValueOrDefault(_keyCode).Execute();
        }
        //if (handler.Equals(RefrenceType))
        //{
        //    handler.Execute();
        //   //if( _commandBuffer.Contains(RefrenceType))
        //   // {
        //   //     _commandBuffer.Find(RefrenceType.GetType()).Execute();
        //   // }
               
        //}
        //adding more types of input down here

        //if(handler.Equals(AltCommand))
        //{ 
        //	AltCommand.Execute(Actor,_keyCode);
        //}
    }

	public void SetHandler(Icommand _Com) //detect types of input and set handler when possible
	{
       // Debug.Log(_Com);
      //  Debug.Log(playercommand.GetType());

        if (_Com.GetType() == RefrenceType.GetType())
		{
			Debug.Log("Handler = player");
            //needs check if value already exsists
            AddCommand(_Com, KeyCode.A);
			handler = (PlayerCommand) _Com;
		}
        
        //extra inputs
        /*
		if(_Com.GetType() == ArrowCommands.GetType())
		{
		Debug.Log("Handler = Arrow");
		//	handler = (ArrowCommands) AltCommand;
		}
        */
    }
    public void AddCommand(Icommand _command, KeyCode _keycode)
    {
        _commandBuffer.Add(_keycode,_command);
    }
}
