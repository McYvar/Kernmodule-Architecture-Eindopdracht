using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommand:ICommand
{
    static Transform Owner;
    readonly Transform[] Udders;
    public ParticleSystem[] MilkParticles;
    public List<ParticleCollisionEvent> collisionEvents;
    ParticleCommand[] CollisionCommand;
    EnemyPool CollisionPool;

    public PlayerCommand(Transform _owner, EnemyPool _CollisionPool)
    {
        Owner = _owner;
        Udders = new Transform[Owner.childCount];
        MilkParticles = new ParticleSystem[Owner.childCount];
        collisionEvents = new List<ParticleCollisionEvent>();
        for (int i = 0; i < Owner.childCount; i++)
        {
            Transform udder = Owner.GetChild(i);
            Udders[i] = udder;
            MilkParticles[i] = Udders[i].GetComponent<ParticleSystem>();
        }
        CollisionPool = _CollisionPool;
        CommandDefine();
        ParticleDefine();
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
    private void ParticleDefine()
    {
        CollisionCommand = new ParticleCommand[4];
        ParticleCommand udderZero = new ParticleCommand(CollisionPool, MilkParticles[0]);
        ParticleCommand udderOne = new ParticleCommand(CollisionPool, MilkParticles[1]);
        ParticleCommand udderTwo = new ParticleCommand(CollisionPool, MilkParticles[2]);
        ParticleCommand udderThree = new ParticleCommand(CollisionPool, MilkParticles[3]);
        CollisionCommand[0] = udderZero;
        CollisionCommand[1] = udderOne;
        CollisionCommand[2] = udderTwo;
        CollisionCommand[3] = udderThree;
        Debug.Log("Created Particle Collider commands");
    }
    public void Execute()
    {
        for (int  i = 0;  i < CollisionCommand.Length;  i++)
        {
            CollisionCommand[i].Execute();
        }
    }
    //https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnParticleCollision.html
   
        public void Undo()
    {
        Debug.Log("");
    }
}
public class keyTransformCommand : ICommand
{
    KeyCode Mycode;
    Vector3 Direction;
    Vector3 Towards;
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
            Mytransform.position = Vector3.MoveTowards(Mytransform.position, Towards, Time.deltaTime * 15);
            //Mytransform.position += Direction;
            Towards = Mytransform.position + Direction;
        }
    }
    public void Undo()
    {
        Debug.Log("No undo, yet...");
    }
}
//concrete implementation for cow udders
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
public class ParticleCommand:ICommand
{
    ParticleSystem Particle;
    Enemy[] Colliders;
    public List<ParticleCollisionEvent> collisionEvents;
    public ParticleCommand(EnemyPool _colliders,ParticleSystem _mySystem)
    {
        Particle = _mySystem;
        Colliders = _colliders.enemies;
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    public void Execute()
    {
        foreach (Enemy item in Colliders)
        {
            OnParticleCollision(item.enemyObject);
        }
    }
    public void Undo()
    {
        Debug.Log("No undo, yet...");
    }
    private void OnParticleCollision(GameObject other) //compare particle system with enemies
    {
        int numCollisionEvents = Particle.GetCollisionEvents(other, collisionEvents);
        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (rb)
            {
                for (int j = 0; j < Colliders.Length; j++)
                {
                    if (Colliders[j].enemyObject == rb.gameObject)
                    {
                        Colliders[j].TakeDamage(100);
                     //   if (Colliders[j].CheckBulletInRange(collisionEvents[i].normal)) { Colliders[j].TakeDamage(100); }
                    }
                }
            }
            i++;
        }
    }
}

