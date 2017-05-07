using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class talkController : MonoBehaviour {


	public Text TXT_pluto;
	public Text TXT_comet;
	private int step;

	public float textRate = 1.0f;
	private float nextText;

	// Use this for initialization
	void Start () {
		nextStep ();
		TXT_pluto.text = "Pluto: Hey little comet! \n What are you doing here?";
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKey (KeyCode.Space)) {

			if(Time.time > nextText) {
				nextStep();
				nextText = Time.time + textRate;
			}
		}
	}

	void nextStep() {
		step++;
		switch (step) {
		case 1: 
			TXT_pluto.text = "Pluto: Hey little comet! \n What are you doing here? \n Why are you so sad?";
			TXT_comet.text = " ";
			break;
		case 2:
			TXT_pluto.text = "";
			TXT_comet.text = "Comet: I just flew past Earth. \n They're celebrating the new year...";
			break;
		case 3:
			TXT_pluto.text = "Pluto: And? \n Have you any goals for new year?";
			TXT_comet.text = "";
			break;
		case 4:
			TXT_pluto.text = "";
			TXT_comet.text = "Comet: Just one. \n But it's almost impossible";
			break;
		case 5:
			TXT_pluto.text = "Pluto: Why? What's that?";
			TXT_comet.text = "";
			break;
		case 6:
			TXT_pluto.text = "";
			TXT_comet.text = "Comet: I would like to mean something more \n Like a planet";
			break;
		case 7:
			TXT_pluto.text = "Pluto: So be it! \n Many years ago i've tried it too";
			TXT_comet.text = "";
			break;
		case 8:
			TXT_pluto.text = "";
			TXT_comet.text = "Comet: What happened?";
			break;
		case 9:
			TXT_pluto.text = "Pluto: I reach stable orbit.";
			TXT_comet.text = "";
			break;
		case 10:
			TXT_pluto.text = "Pluto: And i was unable \n to collect more mass...";
			TXT_comet.text = "";
			break;
		case 11:
			TXT_pluto.text = "Pluto: I was so happy for a short time.";
			TXT_comet.text = "";
			break;
		case 12:
			TXT_pluto.text = "Pluto: People said that \n I don't deserve to be called a planet";
			TXT_comet.text = "";
			break;
		case 13:
			TXT_pluto.text = "Pluto: That's the story...";
			TXT_comet.text = "";
			break;
		case 14:
			TXT_pluto.text = "";
			TXT_comet.text = "Comet: Sad... \n What should i do?";
			break;
		case 15:
			TXT_pluto.text = "Pluto: Go back to Earth.";
			TXT_comet.text = "";
			break;
		case 16:
			TXT_pluto.text = "Pluto: Try to collect a lot of scrap \n from asteroid belts";
			TXT_comet.text = "";
			break;
		case 17:
			TXT_pluto.text = "Pluto: It let you grow...";
			TXT_comet.text = "";
			break;
		case 18:
			TXT_pluto.text = "Pluto: But beware of the sun. \n It may melt you faster you think...";
			TXT_comet.text = "";
			break;
		case 19:
			TXT_pluto.text = "Pluto: But beware of the sun. \n It may melt you faster you think...";
			TXT_comet.text = "";
			break;
		case 20:
			TXT_pluto.text = "Pluto: Use your mouse to catapult from orbit";
			TXT_comet.text = "";
			break;
		case 21:
			TXT_pluto.text = "Pluto: And remember that \n you can use Left Shift \n to look around";
			TXT_comet.text = "";
			break;
		case 22:
			TXT_pluto.text = "";
			TXT_comet.text = "Comet: Thanks Pluto! You're great!";
			break;
		case 23:
			TXT_pluto.text = "Pluto: Good luck little comet! \n Press space to begin!";
			TXT_comet.text = "";
			break;
		case 24:
			Application.LoadLevel(2);
			break;
		}
	}
}
