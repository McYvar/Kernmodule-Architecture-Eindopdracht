using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCommand
{
	public KeyCode key;
	public Icommand command;
}

public class ArrowCommands : Icommand
{
	public void Execute()
	{
		Debug.Log("Use paramter execute for this command!");
	}
	public void Execute(GameObject _actor, KeyCode key)
	{
		if(key == KeyCode.UpArrow)
		{
			Front(_actor);
		}
		if(key == KeyCode.LeftArrow)
		{
			Left(_actor);
		}
		if(key == KeyCode.DownArrow)
		{
			Back(_actor);
		}
		if(key == KeyCode.RightArrow)
		{
			Right(_actor);
		}
	
	}
	public void Back (GameObject _actor)
	{
		_actor.transform.position += Vector3.back;	
	}
	public void Left (GameObject _actor)
	{
		_actor.transform.position += Vector3.left;	
	}
	public void Right (GameObject _actor)
	{
		_actor.transform.position += Vector3.right;	
	}
	public void Front (GameObject _actor)
	{
		_actor.transform.position += Vector3.forward ;	
	}
}

public class WASDCommands : Icommand
{
	public void Execute()
	{
		Debug.Log("Use paramter execute for this command!");
	}
	public void Execute(GameObject _actor, KeyCode key)
	{
		if(key == KeyCode.W)
		{
			Front(_actor);
		}
		if(key == KeyCode.A)
		{
			Left(_actor);
		}
		if(key == KeyCode.S)
		{
			Back(_actor);
		}
		if(key == KeyCode.D)
		{
			Right(_actor);
		}
		
	}
	public void Back (GameObject _actor)
	{
		_actor.transform.position = Vector3.MoveTowards(_actor.transform.position,_actor.transform.position + Vector3.back,0.5f); //Vector3.back;	
	}
	public void Left (GameObject _actor)
	{
		_actor.transform.position = Vector3.MoveTowards(_actor.transform.position,_actor.transform.position + Vector3.left,0.5f);
		//_actor.transform.position += Vector3.left;	
	}
	public void Right (GameObject _actor)
	{
		_actor.transform.position = Vector3.MoveTowards(_actor.transform.position,_actor.transform.position + Vector3.right,0.5f);
		// _actor.transform.position += Vector3.right;	
	}
	public void Front (GameObject _actor)
	{
		_actor.transform.position = Vector3.MoveTowards(_actor.transform.position,_actor.transform.position + Vector3.forward,0.5f);
		//_actor.transform.position += Vector3.forward ;	
	}
}

