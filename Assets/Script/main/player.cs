using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour
{
	/*スワイプで使用する位置と時間*/
	float StartPos, EndPos;
	float StartTime, EndTime;
	/*スワイプによるチャージされた量*/
	public static float charge = 0;
	/*各種パラメータ*/
	public static float bgSpeed = 5.0f;
	public static float obstacleSpeed = 1.5f;
	public float playerSpeed = 3.0f;
	public float decreaseSpeed = 3.0f;
	public static int turboCount = 0;
	/*各種フラグ*/
	public static bool escapeFlag = false;
	public static bool moveRightFlag, moveLeftFlag, pauseFlag, riseFlag, countFlag, turboFlug,speedupFlag = false;
	public ParticleSystem fire;
	public GameObject background, ButtonController, PowerUpAudio, DamageAudio,EmergencyAudio;
	public Text countText, chargeText, ScoreText;
	public Material[] TadashiMaterial;
	Camera camera;
	Vector3 min, max;
	public Button turboBtn;
	public Text rotateTimeText, TurboText;
	string offset = "";
	float highScore = 0;
	float countDown = 5.0f;
	float rotateTime = 0;
	public static float height = 0;
	//記録となる高さ
	const string HIGH_SCORE_KEY = "highScore";
	Animator animator;
	// Use this for initialization
	void Start ()
	{

		/*キャンバス表示*/
		GameObject.Find ("MainCanvas").GetComponent<Canvas> ().enabled = true;
		/*ハイスコア乗りセット*/
//		PlayerPrefs.SetFloat (HIGH_SCORE_KEY, 0.0f);
		camera = Camera.main;
		min = camera.ViewportToWorldPoint (new Vector3 (0, 0, camera.nearClipPlane));
		max = camera.ViewportToWorldPoint (new Vector3 (1, 1, camera.nearClipPlane));

		ButtonController = GameObject.Find ("ButtonController");
		offset = "";
		animator = GetComponent<Animator> ();
		ChangeTadashi (0);
		ScoreText.text = "";
		charge = 0.0f;
		height = 0.0f;
		turboCount = 0;
		GameObject.Find ("TurboPanel").SendMessage ("ShowIcons", turboCount);
		escapeFlag = false;
		moveLeftFlag = false;
		moveRightFlag = false;
		pauseFlag = false;
		riseFlag = false;
		countFlag = true;
		turboFlug = true;
		speedupFlag = false;
		

		ControllFire (false);
		ButtonController.SendMessage ("StartStateButtons");
		ButtonController.SendMessage ("PauseButtons", false);


	}
	// Update is called once per frame
	void Update ()
	{	

		debug (playerSpeed.ToString ());

		if (countFlag) {
			GameObject.Find ("EmergencyPanel").GetComponent<Image> ().enabled = true; 
			if(!EmergencyAudio.GetComponent<AudioSource>().isPlaying)
			EmergencyAudio.GetComponent<AudioSource> ().Play();
			CountDown ();
		}else{
			GameObject.Find ("EmergencyPanel").GetComponent<Image> ().enabled = false;
			EmergencyAudio.GetComponent<AudioSource> ().Stop(); 
		}

		if (!escapeFlag && !pauseFlag) {
			Swipe ();
		}
		if (escapeFlag && !pauseFlag) {
			Escape ();

		}
		//タダシ回転
		Rotate ();
		rotateTimeText.text = "回転時間 : " + rotateTime.ToString ();

		showCharge (charge.ToString ());
	
		//riseFlagが立ってるときに発射モーション
		if (riseFlag) {
			RiseToSail ();
		}
		changeBG ();

		if (charge >= 0) {
			Move ();
		} else {
			/*ゲームオーバー*/
			GameOver ();
		}

	}
	/*スワイプでチャージ*/
	void Swipe ()
	{
		float interval = 0;
		float distance = 0;



		if (Input.GetMouseButtonDown (0)) {
			StartPos = Input.mousePosition.x;
			StartTime = Time.time;
		}
		if (Input.GetMouseButtonUp (0)) {
			EndPos = Input.mousePosition.x;
			EndTime = Time.time;
			distance = EndPos - StartPos;
			interval = EndTime - StartTime;
			//Debug.Log ("sp: " + StartPos + " , end : "+ EndPos);
//			Debug.Log ("distance : " + distance + " interval : " + interval);
			if (distance > 0) {
				/*スワイプされたときの動作*/
				animator.SetBool ("ROTATE", true);
				Charge (distance, interval);
			} 
		}
	}

	void Charge (float distance, float interval)
	{
		charge += (distance * 0.1f) / (interval * 20f);
		rotateTime += 0.75f;
	}

	private void Rotate ()
	{
		if (rotateTime > 0) {
			rotateTime -= Time.deltaTime;
		}
		if (rotateTime <= 0) {
			ChangeTadashi (1);
		}
	}

	void changeBG(){
		if(height > 200){
			//宇宙に変える
			BackGroundGenelator.parent = GameObject.Find ("universe1");
			BackGroundGenelator.prefab = (GameObject)Resources.Load ("Prefab/background/universe");
			BackGroundGenelator.count = 1;
			GameObject.Find ("TransitionPanel").SendMessage ("TransitionBG", 2);
		}
	}
	/*脱出後*/

	void Escape ()
	{
		if (charge > 0) {

			/*背景スクロール*/
			background.transform.position += Vector3.down * bgSpeed * Time.deltaTime;
			/*チャージ量を減少させていく*/
			charge -= Time.deltaTime * decreaseSpeed;
			height += Time.deltaTime * decreaseSpeed;

		} else {

			transform.position += Vector3.down * Time.deltaTime * 5.0f;
		}
	
	}

	void EscapeTadashi ()
	{
		animator.SetBool ("ROTATE", false);
		//		animator.SetBool ("ESCAPE", trustartgamee);
		riseFlag = true;
	}

	void RiseToSail ()
	{
		transform.position += Vector3.up * 2.0f * Time.deltaTime;
		if (transform.position.y > max.y) {
			riseFlag = false;
			StartGame ();
		}
	}
	//空に出てからゲームスタート
	void StartGame ()
	{
		GameObject.Find ("TransitionPanel").SendMessage ("TransitionBG",1);
		ButtonController.SendMessage ("MoveButtons", true);

	}

	void ChangeTadashi (int id)
	{
		animator.SetBool ("ROTATE", false);
		GetComponent<Renderer> ().material = TadashiMaterial [id];
	}
	/*噴射パーティクル操作*/
	void ControllFire (bool onOff)
	{
		if (onOff) {
			fire.Play ();
			GetComponent<AudioSource> ().Play ();
		} else {
			fire.Stop ();
			GetComponent<AudioSource> ().Pause ();
		}
	}

	void CountDown ()
	{	
		if (!pauseFlag) {
			if (escapeFlag) {
				countText.text = "";
			} else {
				if (countDown > -0.5) {
					countDown -= Time.deltaTime;
					countText.text = ((int)Mathf.Ceil (countDown)).ToString ();
				} else {
					countText.text = "";
					GameObject.Find ("Canvas").GetComponent<Canvas> ().enabled = false;
					EmergencyAudio.GetComponent<AudioSource> ().Stop ();
					transform.position += Vector3.forward * 100;
					GameObject.Find ("Mother").GetComponent<Image> ().enabled = true;
					GameObject.Find ("OyaFlaText").GetComponent<Text> ().enabled = true;
					/*カウントが0になったときに脱出してなかったらゲームオーバー*/
					if (!escapeFlag) {
						GameOver ();
					
					}
				}
			}
		}
	}
	/*ボタンでの移動*/
	void Move ()
	{

		Vector3 distance = new Vector3 (0, 0, 0);
//		camera = Camera.main;
//		min = camera.ViewportToWorldPoint (new Vector3 (0, 0, camera.nearClipPlane));
//		max = camera.ViewportToWorldPoint (new Vector3 (1, 1, camera.nearClipPlane));

	
		Vector3 pos = transform.position;
		if (moveRightFlag) {
			distance = Vector3.right * playerSpeed * Time.deltaTime;
		}
		if (moveLeftFlag) {
			distance = Vector3.left * playerSpeed * Time.deltaTime;
		}
			
		pos += distance;

		pos.x = Mathf.Clamp (pos.x, min.x, max.x);


		transform.position = pos;

	}
	/*ハイスコア*/
	public void InitScore ()
	{
		highScore = 0;
		PlayerPrefs.SetFloat (HIGH_SCORE_KEY, highScore);
	}
	/*ハイスコアのセーブ*/
	public void SaveHighScore (float score)
	{	
		if (!PlayerPrefs.HasKey (HIGH_SCORE_KEY)) {
			InitScore ();
		}
		highScore = LoadHighScore ();
		if (score > highScore) {
			highScore = score;
		}
		PlayerPrefs.SetFloat (HIGH_SCORE_KEY, highScore);
		PlayerPrefs.Save ();
	}
	/*ハイスコアのロード*/
	public static float LoadHighScore ()
	{
	
		return PlayerPrefs.GetFloat (HIGH_SCORE_KEY, 0);
		
	}
	/*ゲームオーバー処理*/
	public void GameOver ()
	{
		//スコアの表示
		if (height > LoadHighScore ()) {
			offset = "ハイスコア!!\n";
		}
		if (background.name == "sky1"){
			ScoreText.color = Color.black;
		}else if(background.name == "universe1"){
			ScoreText.color = Color.white;
		}

		if (!countFlag) {
			ScoreText.text = offset + "脱出した高さ " + ((int)height).ToString ("f1") + "M";
		}
		//ハイスコアの記録
		SaveHighScore (height);
		//ボタンの表示・非表示
		ButtonController.SendMessage ("GameOverStateButtons");

		GameObject.Find ("PauseButton").SetActive (false);
		//パーティクルの停止
		ControllFire (false);

	}
	/*障害物やパワーップアイテムに接触したとき*/
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "obstacle") {
			animator.SetBool ("DAMAGE", true);
			charge -= 5.0f;
			Invoke ("StopDamage", 1.0f);
			debug (other.name + "に衝突");
			DamageAudio.GetComponent<AudioSource> ().Play ();
		}
		if (other.tag == "ChargeUp") {
			GameObject.Find ("PowerUpEffect").GetComponent<ParticleSystem> ().startColor = Color.green;
			GameObject.Find ("PowerUpEffect").SendMessage ("Play");
			PowerUpAudio.GetComponent<AudioSource> ().Play ();
			ChargeUp ();
		}
		if (other.tag == "SpeedUp") {
			GameObject.Find ("PowerUpEffect").GetComponent<ParticleSystem> ().startColor = Color.blue;
			GameObject.Find ("PowerUpEffect").SendMessage ("Play");
			PowerUpAudio.GetComponent<AudioSource> ().Play ();
			StartCoroutine (SpeedUp ());

		}
		if (other.tag == "TurboItem") {
			GameObject.Find ("PowerUpEffect").GetComponent<ParticleSystem> ().startColor = Color.yellow;
			GameObject.Find ("PowerUpEffect").SendMessage ("Play");
			if (turboCount < 3) {
				turboCount++;
			}
			if (turboCount > 0) {
				turboFlug = true;
				turboBtn.enabled = true;
				turboBtn.gameObject.SetActive (true);
			}else{
				turboBtn.gameObject.SetActive (false);
			}
			GameObject.Find ("TurboPanel").SendMessage ("ShowIcons", turboCount);
			PowerUpAudio.GetComponent<AudioSource> ().Play ();
//			TurboText.text = "ターボ使用可能数 : " + turboCount.ToString ();
		}

	}

	void ChargeUp ()
	{
		charge += 30;
	}

	private IEnumerator SpeedUp ()
	{	

		if (!speedupFlag) {
			float s = playerSpeed;

			playerSpeed *= 3.0f;
			speedupFlag = true;
			yield return new WaitForSeconds (5.0f);
			speedupFlag = false;
			playerSpeed = s;
		}
	}

	void StartTurbo ()
	{
		StartCoroutine (Turbo ());
	}

	private IEnumerator Turbo ()
	{	
		float b = bgSpeed;
		float o = obstacleSpeed;
		float turbotime = 0.75f;
		float fire_startSpeed;
		float fire_startSize;
		//ターボアイコンの更新
		turboCount--;
		GameObject.Find ("TurboPanel").SendMessage ("ShowIcons", turboCount);
		//ターボ中はターボボタンを押せなくする
		turboFlug = false;
		turboBtn.enabled = false;
		fire_startSize  =fire.startSize  ;
		fire_startSpeed = fire.startSpeed ;
		fire.startSpeed = 20f;
		fire.startSize = 8f;
		if (turboCount == 0) {
			ButtonController.SendMessage ("TurboButton", false);
		}
		TurboText.text = "ターボ使用可能数 : " + turboCount.ToString ();
		bgSpeed *= 3.0f;
		obstacleSpeed *= 3.0f;
		yield return new WaitForSeconds (turbotime);
		fire.startSpeed = fire_startSpeed;
		fire.startSize = fire_startSize;
		height += decreaseSpeed * 3.0f * turbotime;
		bgSpeed = b;
		obstacleSpeed = o;
		if (turboCount > 0) {
			turboFlug = true;
			turboBtn.enabled = true;
			ButtonController.SendMessage ("TurboButton",true);
		}

	}

	void StopDamage ()
	{
		animator.SetBool ("DAMAGE", false);
	}
	/*ボタンの検知*/
	public void PushRightDown ()
	{
		moveRightFlag = true;
	}

	public void PushRightUp ()
	{
		moveRightFlag = false;
	}

	public void PushLeftDown ()
	{
		moveLeftFlag = true;
	}

	public void PushLeftUp ()
	{
		moveLeftFlag = false;
	}
	/*テスト用*/
	void debug (string s)
	{
		Debug.Log (s);
	}

	void showCharge (string s)
	{
		chargeText.text = s;
	}
}
