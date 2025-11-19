using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MovJugador : MonoBehaviour
{
   
        public float velocidad = 2f; 
        public Rigidbody2D rb;
        public Vector2 mov;
        public float movementX;
        public float movementY;
        public float fuerzaSalto = 4f; 
        public LayerMask sueloLayer; 
        public Transform comprobadorSuelo; 
        public float radioComprobador = 0.1f;
        public bool estaensuelo;

        public Transform target; 

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnMove(InputValue movimientos)
        {
          
            mov = movimientos.Get<Vector2>();
            movementX = mov.x;
            movementY = mov.y;
            Debug.Log(mov.x + " , " + mov.y);

            if (movementX != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(movementX), 1, 1);
            }
            estaensuelo = Physics2D.OverlapCircle(comprobadorSuelo.position, radioComprobador, sueloLayer);

        }
        private void FixedUpdate()
        {
     
            Vector2 movimiento = new Vector2(mov.x, 0f);
            rb.linearVelocity = new Vector2(movimiento.x * velocidad, rb.linearVelocity.y);
            estaensuelo = Physics2D.OverlapCircle(comprobadorSuelo.position, radioComprobador, sueloLayer);

        }
        private void OnJump(InputValue movimientos)
        {
            if (estaensuelo)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);

            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
       
            if (collision.gameObject.tag == "Recoleccion")
            {
                GameManager.instance.SumarPuntos(1);
                Destroy(collision.gameObject);
            }
           
            if (collision.gameObject.tag == "Enemigo")
            {
              
                ReiniciarNivel();
            }
           
            if (collision.gameObject.tag == "Fin")
            {
                Victoria();
            }
            
            if (collision.CompareTag("SueloMuerte"))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
                );
            }

        }
        void ReiniciarNivel()
        {
            // Reinicia la escena actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        void Victoria()
        {
            // Cargar la escena de victoria
            SceneManager.LoadScene("Victoria"); 
        }
        public void Salir()
        {
            Debug.Log("Saliendo del juego...");
            Application.Quit();
        }

}
