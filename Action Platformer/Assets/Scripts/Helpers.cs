using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static bool Timer(float time, float waitfor, System.Func<bool> todo) {
        if (todo is null) throw new System.ArgumentNullException(nameof(todo));
        if(Time.time > time + waitfor){
            return todo();
        }
        return false;
    }
    public static float Map (float value, float fromMin, float fromMax, float toMin, float toMax) {
        return (value - fromMin) / (toMin - fromMin) * (toMax - fromMax) + fromMax;
    }
}
