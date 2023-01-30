using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] int _CountOfBreakPeople;

    private Human _human;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Human human) && _CountOfBreakPeople > 0)
        {
            _human = human;
            _CountOfBreakPeople--;
        }
    }

    public Human ColisedHuman()
    {
        return _human;
    }
}
