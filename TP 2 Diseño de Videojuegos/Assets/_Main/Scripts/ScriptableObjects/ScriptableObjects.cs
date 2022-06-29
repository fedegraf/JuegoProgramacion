using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate float FloatDelegate(float _float);
public delegate void VoidDelegatewithFloat(float _float);
public delegate void VoidDelegate();

public class ScriptableObjects : MonoBehaviour
{
    public static float BulletSpeed = 25f;
    public static float BulletDamage = 25f;
    public static float BlastDamage = 25f;
}
