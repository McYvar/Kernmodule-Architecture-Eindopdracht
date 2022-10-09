using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	//declare a method for executing a command
	public interface ICommand
	{
	void Execute();
    void Undo();
}
	