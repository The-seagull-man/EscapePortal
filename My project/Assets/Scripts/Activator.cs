using UnityEngine;

public abstract class Activator : Machine
{
    public Machine machine;

	public override void OnActivate() {
		machine.Active = true;
	}

	public override void OnDeactivate() {
		machine.Active = false;
	}
}
