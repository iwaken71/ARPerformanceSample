using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public enum EffectState
{


}

[System.Serializable]
public class EffectStateReactiveProperty : ReactiveProperty<EffectState>
{
    public EffectStateReactiveProperty() { }
    public EffectStateReactiveProperty(EffectState initialValue) : base(initialValue) { }
}

public enum PostEffectState
{
    Quick



}


