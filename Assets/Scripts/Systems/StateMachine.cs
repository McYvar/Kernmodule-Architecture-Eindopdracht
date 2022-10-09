using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://faramira.com/generic-finite-state-machine-using-csharp/
public class StateMachine<T>
{
	protected Dictionary<T, State<T> > states; //list of availible states
	protected State<T> Currentstate;
	
	public void Update()
	{
	  if (Currentstate != null)
	  {
		  Currentstate.OnUpdate();
	  }
	}
	
	public StateMachine()
	{
		states = new Dictionary<T, State<T>>();
	}

	public void AddState(State<T> _state)
	{
		if(states.ContainsKey(_state.ID))
        {
			if (_state.Name == states[_state.ID].Name)
			{
				//same object
			}
			else
			{
				Debug.Log("error can not add 2 references of the same object to the machine");
				//states.Add(_state.ID, _state);
				return;
			}
		}
		else
		states.Add(_state.ID, _state);
	}
	
	public State<T> GetState(T stateID)
	{
	if (states.ContainsKey(stateID))
		return states[stateID];
		return null;
	}

	public void SetCurrentState(State<T> _state)
	{
		if(Currentstate == _state)
		{
			return;
		}
		if(Currentstate != null)
		{
			Currentstate.OnExit();
			Currentstate = GetState(_state.ID);
			Currentstate.OnEnter();
		}
		if(Currentstate == null)
		{
			Currentstate = _state;
			Currentstate.OnEnter();
		}
		
		//Currentstate.OnUpdate();
		//AddState(_state);
		
	}
}
