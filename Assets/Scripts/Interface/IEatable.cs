using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType
{
    None = -1,
    Herb,
    Game,
}

public interface IEatable
{
    public Vector3 position { get;}
    public int type { get; }
    public void OnEaten(Animal by);
    public System.Action<Animal> onBeingSetTarget{ get; set;}
}
