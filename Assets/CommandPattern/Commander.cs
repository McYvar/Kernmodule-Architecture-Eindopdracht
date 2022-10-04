using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class Commander
	{
		// The Commander does not depend on concrete command or receiver classes.
		// The Commander passes a request to a receiver indirectly, by executing a
		// command.
		//----------------------------==== variables ====-------------------------------------
		private Icommand Onstart;
		private Icommand Onexit;
		private List<Icommand> commandlist; // a list of all known commands for the commander
		
		//----------------------------==== methods ====-------------------------------------
		
		public void SetOnstart(Icommand _command)
		{
			this.Onstart = _command;
			commandlistAdd(_command);
		}
	    
		public void SetOnexit(Icommand _command)
		{
			this.Onexit = _command;
			commandlistAdd(_command);
		}

		public void RunstartExit()
		{
			if(this.Onstart is Icommand)
			{
				this.Onstart.Execute();
			}
		    
			if(this.Onexit is Icommand)
			{
				this.Onexit.Execute();
			}
		}
		
		private void commandlistAdd(Icommand _command)
		{
			if(commandlist == null)
			{
				commandlist = new List<Icommand>();
				commandlist.Add(_command); //to make sure we still add the first item in a empty list
			}
			else
			{
				commandlist.Add(_command);
			}
		}
		
		private void commandlistRemove(Icommand _command)
		{
			commandlist.Remove(_command);
		}
		 
		public void CommandFromList(Icommand _command)
		{
				foreach (Icommand item in commandlist)
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
