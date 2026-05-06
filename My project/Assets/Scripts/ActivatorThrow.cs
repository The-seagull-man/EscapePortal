using UnityEngine;

public class ActivatorThrow : PickupScript
{
	public Machine machine;
	public bool activateOnPickup;

	public override void OnPickup() {
		base.OnPickup();
		if (activateOnPickup) {
			machine.Active = false;
			machine.Active = true;
		} else {
			machine.Active = false;
		}
	}

	public override void OnDrop(Vector3 direction, bool thrown = true) {
		base.OnDrop(direction, thrown);
		if (!activateOnPickup) {
			machine.Active = true;
		}
	}
}
