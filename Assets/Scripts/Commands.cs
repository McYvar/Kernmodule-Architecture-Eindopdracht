using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommand:ICommand
{
    static Transform Owner;
    readonly Transform[] Udders;
    public PlayerCommand(Transform _owner)
    {
        Owner = _owner;
        Udders = new Transform[Owner.childCount];
        for (int i = 0; i < Owner.childCount; i++)
        {
            Transform udder = Owner.GetChild(i);
            Udders[i] = udder;
        }
        CommandDefine();
    }
    public PlayerCommand()
    {

    }
    private void CommandDefine()
    {
        ICommand Wkey = new keyTransformCommand(KeyCode.W,Vector3.forward, Owner);
        CommandSystem.Instance.AddCommand(Wkey, KeyCode.W);
        ICommand Akey = new keyTransformCommand(KeyCode.A, Vector3.left, Owner);
        CommandSystem.Instance.AddCommand(Akey, KeyCode.A);
        ICommand Skey = new keyTransformCommand(KeyCode.S, Vector3.back,Owner);
        CommandSystem.Instance.AddCommand(Skey, KeyCode.S);
        ICommand Dkey = new keyTransformCommand(KeyCode.D, Vector3.right,Owner);
        CommandSystem.Instance.AddCommand(Dkey, KeyCode.D);

        ICommand Upkey = new keyQuaternionCommand(KeyCode.UpArrow, Owner, Quaternion.AngleAxis(0f, Vector3.left));
        CommandSystem.Instance.AddCommand(Upkey, KeyCode.UpArrow);

        ICommand Downkey = new keyQuaternionCommand(KeyCode.DownArrow, Owner, Quaternion.AngleAxis(180f, Vector3.left));
        CommandSystem.Instance.AddCommand(Downkey, KeyCode.DownArrow);

        ICommand Leftkey = new keyQuaternionCommand(KeyCode.LeftArrow, Owner, Quaternion.AngleAxis(0f, Vector3.up));
        CommandSystem.Instance.AddCommand(Leftkey, KeyCode.LeftArrow);
    }

    public void Execute()
    {
        //foreach (ICommand key in CommandSystem.Instance.getCommandbuffer().Values)
        //{
        //    key.Execute();
        //}       
    }
    public void Undo()
    {
        Debug.Log("");
    }
}
public class keyTransformCommand : ICommand
{
    KeyCode Mycode;
    Vector3 Direction;
    Transform Mytransform;
    public keyTransformCommand(KeyCode _mycode,Vector3 _direction, Transform _transform)
    {
        Mycode = _mycode;
        Direction = _direction;
        Mytransform = _transform;
    }

    public void Execute()
    {
        if (Input.GetKeyDown(Mycode))
        {
            Mytransform.position += Direction;
        }
    }
    public void Undo()
    {
        Debug.Log("No undo, yet...");
    }
}
public class keyQuaternionCommand : ICommand
{
    KeyCode Mycode;
    Quaternion Myquaternion;
    Transform Mytransform;
    public keyQuaternionCommand(KeyCode _mycode, Transform _Transform, Quaternion _Quaternion)
    {
        Mycode = _mycode;
        Myquaternion = _Quaternion;
        Mytransform = _Transform;
    }

    public void Execute()
    {
        if (Input.GetKeyDown(Mycode))
        {
            Debug.Log("rotate" + Mytransform.name);
            foreach (Transform item in getChild())
            {
                if(item.name == "Cube") { }
                else
                {
                    item.localRotation = Quaternion.RotateTowards(item.localRotation, Myquaternion, 5f);
                }
            }
           // Quaternion.RotateTowards( Mytransform.rotation,Myquaternion, 1f);
        }
    }
    public Transform[] getChild()
    {
        Transform[] Childcollection = new Transform [Mytransform.childCount];
        for (int i = 0; i < Mytransform.childCount; i++)
        {
            Childcollection[i] = Mytransform.GetChild(i);
        }
        return Childcollection;
    }
    public void Undo()
    {
        Debug.Log("No undo, yet...");
    }
}
public class ArrowCommands : ICommand
{
    public void Execute()
    {
        Debug.Log("Use paramter execute for this command!");
    }

    public void Execute(GameObject _actor, KeyCode key)
    {
        if (key == KeyCode.UpArrow)
        {
            Front(_actor);
        }
        if (key == KeyCode.LeftArrow)
        {
            Left(_actor);
        }
        if (key == KeyCode.DownArrow)
        {
            Back(_actor);
        }
        if (key == KeyCode.RightArrow)
        {
            Right(_actor);
        }

    }
    public void Back(GameObject _actor)
    {
        _actor.transform.position += Vector3.back;
    }
    public void Left(GameObject _actor)
    {
        _actor.transform.position += Vector3.left;
    }
    public void Right(GameObject _actor)
    {
        _actor.transform.position += Vector3.right;
    }
    public void Front(GameObject _actor)
    {
        _actor.transform.position += Vector3.forward;
    }
    public void Undo()
    {

    }
}

public class WASDCommands : ICommand
{
    public void Execute()
    {
        Debug.Log("Use paramter execute for this command!");
    }
    public void Execute(GameObject _actor, KeyCode key)
    {
        if (key == KeyCode.W)
        {
            Front(_actor);
        }
        if (key == KeyCode.A)
        {
            Left(_actor);
        }
        if (key == KeyCode.S)
        {
            Back(_actor);
        }
        if (key == KeyCode.D)
        {
            Right(_actor);
        }

    }
    public void Undo()
    {

    }
    public void Back(GameObject _actor)
    {
        _actor.transform.position = Vector3.MoveTowards(_actor.transform.position, _actor.transform.position + Vector3.back, 0.5f); //Vector3.back;	
    }
    public void Left(GameObject _actor)
    {
        _actor.transform.position = Vector3.MoveTowards(_actor.transform.position, _actor.transform.position + Vector3.left, 0.5f);
        //_actor.transform.position += Vector3.left;	
    }
    public void Right(GameObject _actor)
    {
        _actor.transform.position = Vector3.MoveTowards(_actor.transform.position, _actor.transform.position + Vector3.right, 0.5f);
        // _actor.transform.position += Vector3.right;	
    }
    public void Front(GameObject _actor)
    {
        _actor.transform.position = Vector3.MoveTowards(_actor.transform.position, _actor.transform.position + Vector3.forward, 0.5f);
        //_actor.transform.position += Vector3.forward ;	
    }
}

