using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://refactoring.guru/design-patterns/command/csharp/example

	
	//declare a method for executing a command
	public interface Icommand
	{
	 void Execute();
	}
	public class CommandPatternA : Icommand
	{
	//the command and it's related data and functions

		private GameObject payload = new GameObject();
			private Vector3 location = Vector3.zero;	
		
		public CommandPatternA(GameObject _payload, Vector3 loc)
			{
						this.payload = _payload;
						this.location = loc;
			}
			public void Execute()
			{
				location = payload.transform.position;
				Debug.Log(payload.name);
				Debug.Log(location);
			}
	}
	public class CommandPatternB : Icommand
	{
		private GameObject Locator;// = new GameObject();
		private GameObject Parent;
		private string Name;
		
		public CommandPatternB(GameObject _Locator, string _Name)
		{
			this.Locator = _Locator;
			Parent = new GameObject();
			Parent.name = _Name + "Parent";
			this.Name = _Name;
		}

		// Commands can delegate to any methods of a receiver.
		public void Execute()
		{
			GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
			block.transform.position = Locator.transform.position;
			block.transform.parent = Parent.transform;
			Debug.Log("Placed cube.");
		}
	}