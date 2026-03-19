using UnityEngine;

public abstract class Machine : MonoBehaviour
{
    private bool _active;
    public bool Active {
        get {
            return _active;
        }
        set {
            if (_active != value) {
                if (value) {
                    OnActivate();
                } else {
                    OnDeactivate();
                }
            }
            _active = value;
        }
    }

    public abstract void OnActivate();

    public abstract void OnDeactivate();
}
