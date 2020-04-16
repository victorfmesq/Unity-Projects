using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    public float maxX;
    public float minX;
    public float waitingTime = 2f;

    private GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        UpdateTarget(); // create the Target
        StartCoroutine("PatrolToTarget"); // Call the patrol Coroutine 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateTarget()
    {
        // if have no target, create one
        if (_target == null){
            _target = new GameObject("Target");
            _target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1,1,1); // setted to the left
            return; 
        }

        // if we're by the left, change the target to right
        if (_target.transform.position.x == minX){
            _target.transform.position = new Vector2(maxX, transform.position.y);
            transform.localScale = new Vector3(1,1,1); // setted to the right
        }

        // if we're by the right, change the target to left
        else if (_target.transform.position.x == maxX){
            _target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    // IEnumerator é uma interface que retorna uma Coroutine(função que nos permite executar passos incluindo tempos de espera, intercalação de ações etc)
    private IEnumerator PatrolToTarget() 
    {
        // Coroutine to move the Enemy
        while (Vector2.Distance(transform.position, _target.transform.position) > 0.05f) // Vector2.Distance(Vector2 a , Vector2 b) returns the distance between a & b
        {
            // Nos movemos
            Vector2 direction = _target.transform.position - transform.position; 
            float xDirection = direction.x; 

            transform.Translate(direction.normalized * speed * Time.deltaTime); // move the enemy to direction

            yield return null; // IMPORTANT
        }

        // Here the enemy find the target. Let's set the enemy position on the target position
        Debug.Log("Alvo encontrado");
        transform.position = new Vector2(_target.transform.position.x, transform.position.y);

        // Waiting
        yield return new WaitForSeconds(waitingTime); // IMPORTANT

        // Now the enemy will return to patrol for the target. But for the opposite direction
        UpdateTarget();
        StartCoroutine("PatrolToTarget");
    }
    /**
     *                 EXPLICAÇÃO: YIELD RETURN
     * Usando como exemplo o codigo acima, o tipo do retorno "yeild return" funciona da segunte forma:
     *  -> Quando chega em "Yield Return Null" o código para e se chama novamente (desde o começo).
     *  -> Porém, yield return só eh chamado uma vez por corroutine.
     *  -> Depois de sair do laço, o código irá parar no segundo Yield Return, mas nesse caso ele esperará um tempo para que se chame novamente devido ao método WaitForSeconds().
     *  -> Depois que ele sair desse return a função eh chamada novamente mas pulará o ultimo yield que foi chamado.
     *  -> Depois altera o alvo de posição.
     *  -> No final chamamos a coroutina para que ela faça tudo de novo.
     *  -> O método representa um comportamento então será executado infinitamente até que o objeto que o possuir seja destruido.
     */
}
