using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ITradable
{
    public int price { get; set; }
    public float priceMult { get;set;}
    public void OnSell();
    public void AddMult();
}
