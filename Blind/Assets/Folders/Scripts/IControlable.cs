using UnityEngine;

public interface IControlable
{
    public void SetInputs(Vector2 move, Vector2 lookDelt, bool fire, bool altFire, bool use, bool jump);
}
