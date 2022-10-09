using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//inspired on: https://sj-jason-liu.medium.com/game-design-pattern-command-pt-2-game-dev-series-137-f7c052304812
public class CommandSystem
{
   // PlayerCommand RefrenceType = new PlayerCommand();
    ICommand handler;
    //private GameObject Actor;
    protected Dictionary<KeyCode, ICommand> CommandBuffer = new Dictionary<KeyCode, ICommand>();
    private static CommandSystem _instance = new CommandSystem();
    public static CommandSystem Instance
    {
        get { if (_instance == null) Debug.LogError("CommandSystem is NULL!"); return _instance; }
    }
   
    public Dictionary<KeyCode,ICommand> getCommandbuffer()
    {
        return CommandBuffer;
    }
   

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
        if(CommandBuffer.ContainsKey(_keyCode)) //if the commandbuffer knows the command execute that command
        {
            CommandBuffer.GetValueOrDefault(_keyCode).Execute();
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

	public void SetHandler(ICommand _Com) //detect types of input and set handler when possible
	{
        handler = _Com;
        // Debug.Log(_Com);
        //  Debug.Log(playercommand.GetType());
  //      var a = new PlayerCommand();
  //      if (_Com.GetType() ==  a.GetType())
		//{
		//	Debug.Log("Handler = player");
  //          //needs check if value already exsists
  //          AddCommand(_Com, KeyCode.KeypadEnter);
		//	handler = (PlayerCommand) _Com;
		//}
        
        //extra inputs
        /*
		if(_Com.GetType() == ArrowCommands.GetType())
		{
		Debug.Log("Handler = Arrow");
		//	handler = (ArrowCommands) AltCommand;
		}
        */
    }
    public void AddCommand(ICommand _command, KeyCode _keycode)
    {
        CommandBuffer.Add(_keycode,_command);
    }
}
