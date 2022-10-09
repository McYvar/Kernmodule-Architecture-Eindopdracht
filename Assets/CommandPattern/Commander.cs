using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class Commander
	{
		// The Commander does not depend on concrete command or receiver classes.
		// The Commander passes a request to a receiver indirectly, by executing a
		// command.
		//----------------------------==== variables ====-------------------------------------
		private ICommand Onstart;
		private ICommand Onexit;
		private List<ICommand> commandlist; // a list of all known commands for the commander
		
		//----------------------------==== methods ====-------------------------------------
		
		public void SetOnstart(ICommand _command)
		{
			this.Onstart = _command;
			commandlistAdd(_command);
		}
	    
		public void SetOnexit(ICommand _command)
		{
			this.Onexit = _command;
			commandlistAdd(_command);
		}

		public void RunstartExit()
		{
			if(this.Onstart is ICommand)
			{
				this.Onstart.Execute();
			}
		    
			if(this.Onexit is ICommand)
			{
				this.Onexit.Execute();
			}
		}
		
		private void commandlistAdd(ICommand _command)
		{
			if(commandlist == null)
			{
				commandlist = new List<ICommand>();
				commandlist.Add(_command); //to make sure we still add the first item in a empty list
			}
			else
			{
				commandlist.Add(_command);
			}
		}
		
		private void commandlistRemove(ICommand _command)
		{
			commandlist.Remove(_command);
		}
		 
		public void CommandFromList(ICommand _command)
		{
				foreach (ICommand item in commandlist)
				{
					Debug.Log(item+" commands");
					if(item == _command)
					{
						item.Execute();
					}
				}
		}
		//----------------------------====  ====-------------------------------------
	}
