using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://refactoring.guru/design-patterns/command/csharp/example

	
	//declare a method for executing a command
	public interface Icommand
	{
	 void Execute();
    void Undo();
}
	