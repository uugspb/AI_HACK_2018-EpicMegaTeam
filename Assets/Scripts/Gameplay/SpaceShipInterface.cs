using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SpaceShipInterface {

    void RotateLeft();
    void RotateRight();
    void Accelerate();
    void Shot();
    int GetHP();
}
