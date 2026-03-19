using UnityEngine;

public class MachineTest : Machine
{
    // Update is called once per frame
    void Update()
    {
		if (Active) {
			Debug.Log("Machine running...");
		}
	}

	public override void OnActivate() {
		Debug.Log("ACTIVATED!");
	}

	public override void OnDeactivate() {
		Debug.Log("DEACTIVATED!");
	}
}
