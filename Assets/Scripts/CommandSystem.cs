using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//inspired on: https://sj-jason-liu.medium.com/game-design-pattern-command-pt-2-game-dev-series-137-f7c052304812
public class CommandSystem
{
   // PlayerCommand RefrenceType = new PlayerCommand();
    ICommand handler;
    KeyCode Current;
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
        if (CommandBuffer.ContainsKey(_keyCode)) //if the commandbuffer knows the command execute that command
        {
            CommandBuffer.GetValueOrDefault(_keyCode).Execute();
        }
  
    }
    public void UpdateParticleCollision()
    {
        if (handler.GetType() == typeof(PlayerCommand))
        {
            Debug.Log("handle player");
            handler.Execute();
        }
    }
    public void SetHandler(ICommand _Com) //detect types of input and set handler when possible
	{
        handler = _Com;
      
    }
    public void AddCommand(ICommand _command, KeyCode _keycode)
    {
        CommandBuffer.Add(_keycode,_command);
    }
}
