using UnityEngine;

public class Explosive : Machine
{
    public int timer;
    public float flashRateStart;
	public float flashRateEnd;
	public float radius;
	public float powerMax;
	public float powerMin;

	Material mat;
	int countdown;

	public void Start() {
		mat = GetComponent<MeshRenderer>().material;
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        if (Active) {
			countdown++;
			if (countdown == timer) {
				Explode();
			} else {
				float timerFactor = (countdown - 1f)/(timer);

				float flashFactor = (1 - (Mathf.Cos(((flashRateEnd - flashRateStart)*timerFactor/2 + flashRateStart)*timerFactor*Mathf.PI*timer*Time.fixedDeltaTime) + 1)/2);
				mat.color = new Color((1 + flashFactor*7), flashFactor*(1 + flashFactor*7), 0);
		    }
		}
    }

    public void Explode() {
		Destroy(gameObject);
    }

	public override void OnActivate() {
        if (timer == 0) {
            Explode();
        } else {
			mat.color = new Color(1, 0, 0);
		}
    }

	public override void OnDeactivate() {
		mat.color = new Color(1, 1, 1);
		countdown = 0;
    }
}
