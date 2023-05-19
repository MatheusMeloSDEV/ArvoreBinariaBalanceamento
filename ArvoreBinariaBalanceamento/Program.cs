using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        class Teste
        {

            class Arvore
            {
                public int info;
                public Arvore sae;
                public Arvore sad;
                public Arvore(int info)
                {
                    this.info = info;
                }
            }
            public Teste()
            {
            }

            Arvore RAIZ;
            /*
             * Verifica se a raiz existe, se não cria uma.
             * Caso ja exista, chama o método Insere()
             */
            public void Add(int data)
            {
                Arvore arvore = new Arvore(data);
                if (RAIZ == null)
                {
                    RAIZ = arvore;
                }
                else
                {
                    RAIZ = Insere(RAIZ, arvore);
                }
            }

            /*
             * Verifica se o nó ja está ocupado, caso não esteja, insere o valor digitado, e retorna o nó
             * Caso ja esteja ocupado verifica se é menor ou maior, e utiliza recursividade para avançar pela árvore.
             * > Direita
             * < Esquerda
             */
            private Arvore Insere(Arvore temp, Arvore n)
            {
                if (temp == null)
                {
                    temp = n;
                    return temp;
                }
                else if (n.info < temp.info)
                {
                    temp.sae = Insere(temp.sae, n);
                    temp = balanceamento(temp);
                }
                else if (n.info > temp.info)
                {
                    temp.sad = Insere(temp.sad, n);
                    temp = balanceamento(temp);
                }
                return temp;
            }

            /*
             * Chama método fatorBalanceamento()
             * Se o fb for > 1  e o fb do próximo nó for > 0, chama o método RodaEsquerda()
             * Se o fb for < -1  e o fb do próximo nó for < 0, chama o método RodaDireita()
             */
            private Arvore balanceamento(Arvore temp)
            {
                int fb = fatorBalanceamento(temp);
                if (fb > 1)
                {
                    if (fatorBalanceamento(temp.sae) > 0)
                    {
                        temp = RodaEsquerda(temp);
                    }
                }
                else if (fb < -1)
                {
                    if (fatorBalanceamento(temp.sad) < 0)
                    {
                        temp = RodaDireita(temp);
                    }
                }
                return temp;
            }
            /*
             * Imprime a Arvore em Ordem
             */
            public void MostraArvore()
            {
                if (RAIZ == null)
                {
                    Console.WriteLine("Árvore está vazia");
                    return;
                }
                MostraEmOrdem(RAIZ);
                Console.WriteLine();
            }

            /*
             * Imprime a Arvore em Ordem
             */
            private void MostraEmOrdem(Arvore temp)
            {
                if (temp != null)
                {
                    MostraEmOrdem(temp.sae);
                    Console.Write("({0}) ", temp.info);
                    MostraEmOrdem(temp.sad);
                }
            }

            /*
             * Verifica se existe diferença na altura da subarvore da esquerda e direita
             */
            private int max(int esq, int dir)
            {
                return esq > dir ? esq : dir;
            }

            /*
             * Verifica a altura das duas subarvores e chama o método max()
             */
            private int Altura(Arvore temp)
            {
                int altura = 0;
                if (temp != null)
                {
                    int esq = Altura(temp.sae);
                    int dir = Altura(temp.sad);
                    int m = max(esq, dir);
                    altura = m + 1;
                }
                return altura;
            }

            /*
             * Calcula o fb pela diferença de altura entre os nós
             */
            private int fatorBalanceamento(Arvore temp)
            {
                int esq = Altura(temp.sae);
                int dir = Altura(temp.sad);
                int fb = esq - dir;
                return fb;
            }

            /*
             * Executa a rotação para a direita, balanceando a árvore
             */
            private Arvore RodaDireita(Arvore noPai)
            {
                Arvore aux = noPai.sad;
                noPai.sad = aux.sae;
                aux.sae = noPai;
                return aux;
            }

            /*
             * Executa a rotação para a esquerda, balanceando a árvore
             */
            private Arvore RodaEsquerda(Arvore noPai)
            {
                Arvore aux = noPai.sae;
                noPai.sae = aux.sad;
                aux.sad = noPai;
                return aux;
            }

            static void Main(string[] args)
            {
                Teste teste = new Teste();
                teste.Add(10);
                teste.Add(20);
                teste.Add(30);
                teste.Add(40);
                teste.Add(9);
                teste.Add(8);
                teste.Add(7);
                teste.Add(6);
                teste.MostraArvore();
                Console.ReadKey();
            }
        }
    }
}

/*
 * Teste
 * 
 * Fase 1
 * 
 *              10
 *              
 * Fase 2
 * 
 *              10
 *                  20
 * 
 * Fase 3
 * 
 *              10
 *                  20
 *                      30 - Desbalanceou
 * 
 * Fase 3 - Pós balanceamento
 * 
 *              20
 *          10      30
 *          
 * Fase 4
 * 
 *              20
 *          10      30
 *                      40
 *                      
 * Fase 5
 * 
 *              20
 *          10      30
 *       9              40
 *       
 * Fase 6
 * 
 *              20
 *          10      30
 *       9              40
 *    8 - Desbalanceou
 *    
 * Fase 6 - Pós balanceamento
 * 
 *              20
 *           9      30
 *        8     10     40
 * 
 * 
 * Site utilizado como referência: https://simpledevcode.wordpress.com/2014/09/16/avl-tree-in-c/
 */
