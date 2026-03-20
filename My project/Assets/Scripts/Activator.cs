using UnityEngine;

public abstract class Activator : MonoBehaviour
{
    public Machine machine;

    /**
     * Activates or deactivates the machine.
     * Returns true if the state of the machine changed.
     */
    public bool SetActivate(bool active) {
        if (machine.Active == active) {
            return false;
        } else {
            machine.Active = active;
            return true;
        }
    }
}
