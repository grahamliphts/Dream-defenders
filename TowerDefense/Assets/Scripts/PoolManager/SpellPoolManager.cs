using UnityEngine;
using System.Collections;

public class SpellPoolManager : MonoBehaviour 
{
    [SerializeField]
    private SpellScript[] _spells;
    int _index = 0;

    public SpellScript GetSpell()
    {
        var spell = _spells[_index];
        _index++;
        if (_index >= _spells.Length)
            _index = 0;
        return spell;
    }
}
