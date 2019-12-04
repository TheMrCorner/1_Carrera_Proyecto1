using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //Para llamar a las funciones del GameManager desde otras entidades
    public static GameManager instance;

    //CHECKPOINTS
    public CheckPoint[] CheckPoints; //Array de checkPoints
    public int order; //Indicará el check actual
    bool seHaDesactivadoUnCheck; //Para saber cuando hay que disminuir un check

    public GameObject Jugador; //Para editar la posición del mismo
    PlayerMovement jugaPlayer;

    // COLOR PLAYER SPRITE
    private SpriteRenderer currentColor;
    private Color original;

    //STATS
    public int vida; // *De momento está público para hacer testeos*
    public int embriaguez; // Para testeo
    public int llaves = 0;
    public int flechas = 10;
    [HideInInspector]
    public bool borracho = false;
    [HideInInspector]
    public bool muerto;

    //HUD
    public GameObject PanelDePausa;
     bool paused;
    public bool activaPausa;

    public GameObject botella1;
    public GameObject botella2;
    public GameObject botella3;
    public GameObject barraVida;
    public Text contadorLlaves;
    public Text contadorFlechas;
    private float barraVidaInicialX;
    public GameObject fondoBosque;
    public GameObject fondoCastillo;

    //OBJETOS:    
    GameObject[] botellas;
    GameObject[] enemigos;
    [HideInInspector]
    Vector3[] posicionesEnemigos;
    //BOSS
    public GameObject bossDelNivel;

    //OTROS
    [HideInInspector]
    public bool Persecuccion;
    [HideInInspector]
    public bool hayCofre;

    public MovimientoBarca movBarca;

    public Camera camara;
    public DesactivaSpawn desactivaSpawn;
    void Start()
    {
        instance = this;	//GameManager
        Persecuccion = true;
        jugaPlayer = Jugador.GetComponent<PlayerMovement>();
        botellas = GameObject.FindGameObjectsWithTag("Botella"); //Miramos todas las botellas de la escena al empezar
        order = -1;
        PanelDePausa.SetActive(false);
        barraVidaInicialX = (barraVida.transform.localScale.x);
        vida = 100;
        embriaguez = 0;
        // Color
        currentColor = jugaPlayer.GetComponent<SpriteRenderer>();
        original = currentColor.color;

        // Posiciones iniciales de los enemigos para el Respawn
        enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        posicionesEnemigos = new Vector3[enemigos.Length];
        int i = 0;
        foreach (GameObject Enemigo in enemigos)
        {
            posicionesEnemigos[i] = Enemigo.transform.position;
            i++;
        }

        UpdateGUI();	    //Actualiza el GUI de vidas y embriaguez

        // Diálogos
        CuadroDialogos.SetActive(false);
        
    }

    //DISTRIBUCION CHECKPOINTS
    public void AumentarCheck()
    {
        order++;
    }

    public void DisminuirCheck()
    {
        if (order >= 0)
        { //Si tiene algun checkPoint		
            order--;
            seHaDesactivadoUnCheck = true;
        }
    }
    //EMBRIAGUEZ Y VIDA
    public void AumentarEmbriaguez(int alc)
    {
        embriaguez += alc;
    }

    public void AumentarVida(int vid)
    {
        vida += vid;
        if (vida > 100)
            vida = 100; //Para que no se pase

        if (barraVida.transform.localScale.x + (barraVidaInicialX / 100) * vid < barraVidaInicialX)
            barraVida.transform.localScale = new Vector3(barraVida.transform.localScale.x + ((barraVidaInicialX / 100) * vid),
            barraVida.transform.localScale.y, barraVida.transform.localScale.z);
        else
            barraVida.transform.localScale = new Vector3(barraVidaInicialX, barraVida.transform.localScale.y, barraVida.transform.localScale.z);
    }

    public void AumentarMunicion(int muni) {
        flechas += muni;

        if (flechas > 20)
            flechas = 20;
    }
    public void Disparo(){
        flechas--;
        UpdateGUI();
    }

    public void Respawn()
    {
      

        barraVida.transform.localScale = new Vector3(barraVidaInicialX, barraVida.transform.localScale.y, barraVida.transform.localScale.z);
        //Aparición
        if (borracho)
        {
            DisminuirCheck(); //Esto activa seHaDesactivadoUnCheck
            jugaPlayer.setDesnudo(true);
        }

        if (seHaDesactivadoUnCheck)
        {
            CheckPoints[order + 1].ReiniciarCheck(); //Para poder coger el ultimo alcanzado cuando revivamos (a no ser que nos emborrachemos)
            seHaDesactivadoUnCheck = false;
        }

        //Reactivar Objetos
        foreach (GameObject Botella in botellas)
        {
            Botella.SetActive(true);
        }

        ReiniciarStats();

        //Posicion del jugador (checkpoint)
        if (order < 0){
			order = 0;
            CheckPoints[0].cogido = true;
        }
        
        Jugador.transform.position = CheckPoints[order].transform.position;


        //Barca
        if (movBarca != null)
        {
            movBarca.muerto = true;
            movBarca.fin = true;
            movBarca.GetComponent<Collider2D>().isTrigger = true;
            movBarca.transform.position = new Vector3(-543.9f, 0.79f, 0.5f);
            movBarca.movX = 9;
        }

        //Enemigos
        int i = 0;
        foreach (GameObject Enemigo in enemigos)
        {
            if (Enemigo.GetComponent<EnemyAI>() != null)
                Enemigo.GetComponent<EnemyAI>().Respawn();

            else if (Enemigo.GetComponent<ShieldEnemyAI>() != null)
                Enemigo.GetComponent<ShieldEnemyAI>().Respawn();

            Enemigo.transform.position = posicionesEnemigos[i];
            Enemigo.SetActive(true);
            Enemigo.transform.eulerAngles = new Vector3(0, 0, 0);
            i++;
        }

        if (desactivaSpawn != null)
            desactivaSpawn.DesactivarSpawn();

        if (bossDelNivel != null) { 
            if(bossDelNivel.GetComponent<FinalBoss>() != null)
                bossDelNivel.GetComponent<FinalBoss>().Respawn();

            else if (bossDelNivel.GetComponent<FinalBossFase2>() != null)
                bossDelNivel.GetComponent<FinalBossFase2>().Respawn();

            else if (bossDelNivel.GetComponent<BossIA>() != null)
            bossDelNivel.GetComponent<BossIA>().Respawn();
        }

		if (AudioManager.Instance.audioSourceBoss.activeInHierarchy == true){
			AudioManager.Instance.audioSourceBoss.SetActive (false);
			AudioManager.Instance.Musica1 ();
		}
    }

    public void ReiniciarStats()
    {
        vida = 100;
        flechas = 0;
        embriaguez = 0;
        borracho = false;
        muerto = false;
        camara.orthographicSize = 8;
    }

    public void UpdateGUI()
    {
        //Muerto
        if (vida <= 0)
        {
            muerto = true;
            Debug.Log("Muerto = " + muerto);
          
        }

        if (flechas <= 0)
        {
            flechas = 0;
        }

        if (embriaguez >= 3)
        {
            embriaguez = 3;
            borracho = true;
        }
        if (embriaguez <= 0)
        {
            embriaguez = 0;
            borracho = false;
        }
        botella1.SetActive(embriaguez >= 1);
        botella2.SetActive(embriaguez >= 2);
        botella3.SetActive(embriaguez >= 3);
        contadorLlaves.text = "x" + llaves;
        contadorFlechas.text = "x" + flechas;
    }

    public void TogglePause()
    {
        paused = !paused;
        if (paused)
            PanelDePausa.SetActive(true);
        if (!paused)
            PanelDePausa.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();
        //PAUSA
        if(activaPausa)
        Pausa();
       
        //Debug.Log ("Flechas: " + flechas + ", vida: " +vida+ ", embriaguez: " + embriaguez + ", borracho = " + borracho + ", muerto = " + muerto);
    }

    public void Damage(int danyo)
    {
        if (jugaPlayer.apareceVestido == false)
            danyo = danyo / 2;

        vida -= danyo;

        if (vida > 0)
        {

            Debug.Log("El jugador recibe daño" + vida);
            currentColor.color = Color.red;
            Invoke("OriginalColor", 0.4f);

            //Reduce la escala de la barra de vida del jugador
            barraVida.transform.localScale = new Vector3(barraVida.transform.localScale.x - ((barraVidaInicialX / 100) * danyo),
            barraVida.transform.localScale.y, barraVida.transform.localScale.z);
        }
        else
            barraVida.transform.localScale = new Vector3(0, barraVida.transform.localScale.y, barraVida.transform.localScale.z);

    }
    //TODO LO DE HUMO
    int danoHumo = 0;
    [HideInInspector]
    public bool dentroHumo = false;

    public void DamageHumo(int dano)
    {
        danoHumo = dano;
        DanoHumo();
    }

    void DanoHumo()
    {
        if (jugaPlayer.apareceVestido == false)
            danoHumo = danoHumo / 2;
        
            vida -= danoHumo;

        if (vida > 0)
        {
            Debug.Log("El jugador recibe daño" + vida);
            currentColor.color = Color.red;
            Invoke("OriginalColor", 0.4f);
            barraVida.transform.localScale = new Vector3(barraVida.transform.localScale.x - ((barraVidaInicialX / 100) * danoHumo),
            barraVida.transform.localScale.y, barraVida.transform.localScale.z);
        }
        else
            barraVida.transform.localScale = new Vector3(0, barraVida.transform.localScale.y, barraVida.transform.localScale.z);

        if (dentroHumo)
            Invoke("DanoHumo", 1);
    }

    //TODO LO DE RAYO
    int danoRayo = 0;
    [HideInInspector]
    public bool dentroRayo = false;

    public void DamageRayo(int dano)
    {
        if (jugaPlayer.apareceVestido == false)
            danoRayo = dano / 2;
        else 
            danoRayo = dano;

        DanoRayo();
    }

    void DanoRayo()
    {
        vida -= danoRayo;

        if (vida > 0)
        {
            Debug.Log("El jugador recibe daño de DANORAYO" + vida);
            currentColor.color = Color.red;
            Invoke("OriginalColor", 0.4f);
            barraVida.transform.localScale = new Vector3(barraVida.transform.localScale.x - ((barraVidaInicialX / 100) * danoRayo),
            barraVida.transform.localScale.y, barraVida.transform.localScale.z);
        } else
            barraVida.transform.localScale = new Vector3(0, barraVida.transform.localScale.y, barraVida.transform.localScale.z);

        if (dentroRayo)
            Invoke("DanoRayo", 0.8f);
    }

    public void AddKey()
    {
        llaves++;
        UpdateGUI();
        Debug.Log("Toma una llave!");
    }

    private void OriginalColor()
    {
        currentColor.color = original;
    }

    public void LoadScene(string nivel)
    {
        SceneManager.LoadScene(nivel);
    }

    //Método que cambia los fondos de pantalla
    public void CambiaFondo()
    {
        //Si ya está activado el fondo del bosque lo desactivamos
        if (fondoBosque.activeSelf)
        {
            fondoBosque.SetActive(false);
            fondoCastillo.SetActive(true);
        }

        //SI está activado el fondo del castillo, activamos el del Bosque
        else if (fondoCastillo.activeSelf)
        {
            fondoBosque.SetActive(true);
            fondoCastillo.SetActive(false);
        }
    }

	public void Salir(){
		Application.Quit ();
	}

    void Pausa()
    {
        if (paused)
            Time.timeScale = 0;
        if (!paused)
            Time.timeScale = 1;
    }

    // DIÁLOGOS
    public Text dialogos;
    public GameObject CuadroDialogos;

}
