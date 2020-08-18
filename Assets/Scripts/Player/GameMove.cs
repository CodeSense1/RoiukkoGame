using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameMove {
    public Transform Player { get; set; }
    public Vector3 Direction { get; set; }
    public Transform Pushable { get; set; }


    public GameMove(Transform player, Vector3 direction) {
        Player = player;
        Direction = direction;
    }

    public GameMove(Transform player, Transform pushable, Vector3 direction) {
        Player = player;
        Direction = direction;
        Pushable = pushable;
    }
    

    public void Undo()
    {
        Player.Translate(-Direction);
        if (Pushable != null)
            Pushable.Translate(-Direction);
 
    }

}
