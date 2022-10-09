using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T>
{
	// The name for the state.
	public string Name { get; set; }
	// The ID of the state.
	public T ID { get; private set; }    
	
	//public delegate void OnEnter();
	//public delegate void OnExit();
	//public delegate void OnUpdate();
	public State(T id)
	{
		ID = id;
	}

	public State(T id, string name) : this(id)
	{
		Name = name;
	}

	virtual public void OnEnter()
	{
	}
	virtual public void OnExit()
	{
	}
	virtual public void OnUpdate()
	{
	}
}
//https://faramira.com/generic-finite-state-machine-using-csharp/

