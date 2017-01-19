using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {

	private GameObject indexPagePanelObject;
	private GameObject machineRacePanelObject;
	private GameObject rankPanelObject;
	private GameObject myInfoPanelObject;
	private GameObject settingPanelObject;

	// Use this for initialization
	void Start () {
		//TabBar设置
		GameObject gridLayoutObject = GameObject.Find ("GridLayout");
		GridLayoutGroup gridLayout = gridLayoutObject.GetComponent<GridLayoutGroup>();
		gridLayout.cellSize = new Vector2 (Screen.width/5,gridLayout.cellSize.y);

		//TabItem设置
		GameObject indexPageObject = GameObject.Find ("IndexPage");
		indexPageObject.GetComponent<Toggle> ().onValueChanged.AddListener (onIndexPageToggleVauleChanged);

		GameObject machineRaceObject = GameObject.Find ("MachineRace");
		machineRaceObject.GetComponent<Toggle> ().onValueChanged.AddListener (onMachineRaceToggleVauleChanged);

		GameObject rankObject = GameObject.Find ("Rank");
		rankObject.GetComponent<Toggle> ().onValueChanged.AddListener (onRankToggleVauleChanged);

		GameObject myInfoObject = GameObject.Find ("MyInfo");
		myInfoObject.GetComponent<Toggle> ().onValueChanged.AddListener (onMyInfoToggleVauleChanged);

		GameObject settingInfoObject = GameObject.Find ("Setting");
		settingInfoObject.GetComponent<Toggle> ().onValueChanged.AddListener (onSettingToggleVauleChanged);

		//Panel设置
		indexPagePanelObject = GameObject.Find ("IndexPagePanel");
		indexPagePanelObject.SetActive (true);

		machineRacePanelObject = GameObject.Find ("MachineRacePanel");
		machineRacePanelObject.SetActive (false);

		rankPanelObject = GameObject.Find ("RankPanel");
		rankPanelObject.SetActive (false);

		myInfoPanelObject = GameObject.Find ("MyInfoPanel");
		myInfoPanelObject.SetActive (false);

		settingPanelObject = GameObject.Find ("SettingPanel");
		settingPanelObject.SetActive (false);
	}
	

	void Update () {
		
	}


	public void onIndexPageToggleVauleChanged(bool selected){
		indexPagePanelObject.SetActive (selected);

	}

	public void onMachineRaceToggleVauleChanged(bool selected){
		machineRacePanelObject.SetActive (selected);
	}

	public void onRankToggleVauleChanged(bool selected){
		rankPanelObject.SetActive (selected);
	}

	public void onMyInfoToggleVauleChanged(bool selected){
		myInfoPanelObject.SetActive (selected);
	}

	public void onSettingToggleVauleChanged(bool selected){
		settingPanelObject.SetActive (selected);
	}
}