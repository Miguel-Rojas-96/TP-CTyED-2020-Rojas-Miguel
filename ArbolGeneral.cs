using System;
using System.Collections.Generic;

namespace juegoIA
{
	public class ArbolGeneral<T>
	{
		private NodoGeneral<T> raiz;

		public ArbolGeneral(T dato) {
			this.raiz = new NodoGeneral<T>(dato);
		}
		
		private ArbolGeneral(NodoGeneral<T> nodo) {
			this.raiz = nodo;
		}
		
		private NodoGeneral<T> getRaiz() {
			return raiz;
		}
		
		public T getDatoRaiz() {
			return this.getRaiz().getDato();
		}
		
		public List<ArbolGeneral<T>> getHijos() {
			List<ArbolGeneral<T>> temp= new List<ArbolGeneral<T>>();
			foreach (var element in this.raiz.getHijos()) {
				temp.Add(new ArbolGeneral<T>(element));
			}
			return temp;
		}
		
		public void agregarHijo(ArbolGeneral<T> hijo) {
			this.raiz.getHijos().Add(hijo.getRaiz());
		}
		
		public void eliminarHijo(ArbolGeneral<T> hijo) {
			this.raiz.getHijos().Remove(hijo.getRaiz());
		}
		//METODO CREADO PARA EL TP-FINAL
		public void ConvertirEnHoja()
		{
			foreach(var x in this.getHijos())
			{
				this.raiz.getHijos().Remove(x.getRaiz());
			}
		}
		public bool esVacio() {
			return this.raiz == null;
		}
		
		public bool esHoja()
		{
			return this.raiz != null && this.getHijos().Count == 0;
		}
		public void PorNivelesMarcadoFin()
		{
			Cola<ArbolGeneral<T>> cola=new Cola<ArbolGeneral<T>>();    //Intancia una cola vacia
			cola.encolar(this);                                        //Obtiene el Nodo Raiz y lo encola
			cola.encolar(null);                                        //Encola null (para determinar cuando empieza el nuevo nivel)
			int nivel=1;
			Console.WriteLine("=======================================================================================================================");
			Console.Write("[Nivel-"+nivel+"]: ");
			while(cola.esVacia()!=true)
			{
				ArbolGeneral<T> NodoDesencolado=cola.desencolar();     //Quita de la cola el dato(cola[0]) y lo almacena en la variable
				if(NodoDesencolado==null)
				{
					cola.encolar(null);                               //Si el dato es NULL, encola NULL para señalar el comienzo de un nuevo nivel
					if(cola.tope()==NodoDesencolado)
					{
						cola.desencolar();                            //Si el dato en la cola, anterior a el dato NodoDesencolado, es tambien NULL se desencola
						//pero no se almacena en ninguna variable temporal.
					}
					else
					{
						nivel++;                                      //Si el dato no es NULL, es nodo, nivel incrementa y se comienza a imprimir el nuevo nivel
						Console.Write("\n========================================================================================================================");
						Console.Write("[Nivel-"+nivel+"]: ");
					}
				}
				else                                                  //Si el dato desencolado es un NODO se encola e imprimen los hijos de este
				{
					foreach(var Nodo in NodoDesencolado.getHijos())
					{
						cola.encolar(Nodo);
					}
					Console.Write("["+NodoDesencolado.getDatoRaiz()+"]");
				}
			}
			Console.Write("\n========================================================================================================================");
		}
		public List<int> BuscarJugada(List<int> vacia)
		{
			List<int> nueva=vacia;
			if(Convert.ToInt32(this.getDatoRaiz())!=-1)
			{
				nueva.Add(Convert.ToInt32(this.getDatoRaiz()));
			}
			List<ArbolGeneral<T>> listaHijos = this.getHijos();
			foreach(var hijo in listaHijos)
			{
				if(Convert.ToInt32(hijo.getDatoRaiz())==-2) //Si encuentra el nodo hoja con datoraiz -2 esa secuencia de nodos por los que paso hasta llegar a este nodo
					                                        //estara almacenada en la lista con nombre nueva. De esta lista, que representa el cojunto de jugadas, el dato
					                                        //que se encuentra en la posicion 1 es la siguiente carta a jugar
				{
					return nueva;
				}
				nueva=hijo.BuscarJugada(nueva);
				return nueva;
			}
			return nueva;
		}
		public void profundidad(int n, int profundidad)
		{
			//Este metodo permite realizar la consulta de los datos de los nodos que se encuentran entre dos niveles
			//si los parametros n=profundidad obtenemos los datos que se encuentran en un determinado nivel. Si los 
			//decrementamos el nivel en uno obtenemos los datos en una determinada profundidad
			var cola=new Cola<NodoGeneral<T>>(); 
			cola.encolar(this.getRaiz());         
			cola.encolar(null);                    //Encola null para marcar la finalizacion un  nivel.
			int nivel=1;                           //Cuando finaliza un nivel y comienza uno nuevo la variable nivel incrementa en 1.
			string datos="";
			while(!cola.esVacia())
			{
				NodoGeneral<T> aux=cola.desencolar();
				if (aux == null)
				{
					cola.encolar(null);
					nivel++;
					if (cola.tope() == aux)        //Esta estructura condicional permite evitar un bucle infinito, ya que cuando desencolo un null encolo otro para finalizar un nivel
					{                              //Cuando consultamos con el metodo tope no estamos desencolando sino preguntando si el siguiente dato a desencolar sera nulo o no. Si
						cola.desencolar();         //este dato siguiente es nulo lo desencolo y no encolo nada. De esta forma evito que se encolen nulls infinitos.
					}
				}
				else
				{
					if (nivel>=n && nivel <= profundidad) //Si el nivel coincide con la profundidad, es decir nivel=n=profundidad agrega a mi variable "datos" el  dato del nodo que
					{                                     //se encuentra en dicha profundidad.
						Console.Write("["+aux.getDato()+"]"); //Esta impresion elabora una candena con los datos de los nodos que se encuentran en la misma profundidad
						datos=datos+aux.getDato();
					}

					if(!this.esHoja()){
						foreach(var hijo in aux.getHijos())
							cola.encolar(hijo);
					}
				}	
			}
		}
		public ArbolGeneral<T> CortarArbol(int UltimaCartaUsuario)
		{
			Cola<ArbolGeneral<T>> c  = new Cola<ArbolGeneral<T>>();
			ArbolGeneral<T> arbolAux=new ArbolGeneral<T>(null);
			c.encolar(this);
			while(!c.esVacia()){
				arbolAux = c.desencolar();
				if(Convert.ToInt32(arbolAux.getDatoRaiz())==UltimaCartaUsuario)
				{
					return arbolAux;
				}
				if(!this.esHoja()){
					foreach(var hijo in arbolAux.getHijos())
						c.encolar(hijo);
				}
			}
			return arbolAux;
		}
		public List<int> Jugadas(List<int> vacia)
		{
			List<int> nueva=vacia;
			if(this.esHoja())                                    //Encuentra un nodo hoja
			{                                                    //
				Console.Write("Posible secuencia de Jugada: ");  //
				foreach(var jugada in nueva)                     //Imprime todos los nodos que recorrio hasta llegar al nodo hoja.
				{
					Console.Write("["+jugada+"]");
				}
				Console.WriteLine("["+this.getDatoRaiz()+"]");
			}
			else{
				List<ArbolGeneral<T>> listaHijos = this.getHijos();
				nueva.Add(Convert.ToInt32(this.getDatoRaiz()));   //Todos los nodos que no son hoja los almacena en la lista "nueva"
				foreach(var hijo in listaHijos)
				{
					nueva=hijo.Jugadas(nueva);
				}
				nueva.Remove(Convert.ToInt32(this.getDatoRaiz())); //Cuando termina de recorrer los hijos de un nodo elimina ese nodo padre de la lista para
				                                                   //poder seguir con el siguiente camino
			}
			return nueva;
		}
	}
}
