using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Property
{
    float[] _valueArray;
    float[] _costArray;
    int _currentIndex = 0;
    public float _currentValue;
    public float _currentCost;

    public Property(float[] valueArray, float[] costArray)
    {
        _valueArray = valueArray;
        _costArray = costArray;
        _currentIndex = 0;
        _currentValue = valueArray[0];
    }

    public bool TryUpgradeProperty()
    {
        if (_currentIndex < _valueArray.Length - 1)
        {
            _currentIndex++;
            _currentValue = _valueArray[_currentIndex];
            _currentCost = _costArray[_currentIndex];
            return true;
        }
        return false;
    }
}
