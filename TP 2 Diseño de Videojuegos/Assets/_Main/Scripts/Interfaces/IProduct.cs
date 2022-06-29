using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProduct<T> where T : ScriptableObject
{
    T Data { get; }
    public void SetData(T newData);
}
