using UnityEngine;

public interface ExplosionReceiver
{
    public void ReceiveExplosion(Vector3 position, Vector3 force, float power);
}
