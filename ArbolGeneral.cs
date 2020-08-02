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
		public void porNiveles(){
			Cola<ArbolGeneral<T>> c  = new Cola<ArbolGeneral<T>>();
			ArbolGeneral<T> arbolAux;
			
			c.encolar(this);
			while(!c.esVacia()){
				arbolAux = c.desencolar();
				Console.Write(arbolAux.getDatoRaiz() + " ");
				
				if(!this.esHoja()){
					foreach(var hijo in arbolAux.getHijos())
						c.encolar(hijo);
				}
			}
		}
		public void preOrden(){
			// Procesamos raiz
			Console.Write(this.getDatoRaiz() + " ");
			
			// Hago recursion en todos los hijos
			if(!this.esHoja()){
				List<ArbolGeneral<T>> listaHijos = this.getHijos();
				foreach(var hijo in listaHijos)
					hijo.preOrden();
			}
		}
		
		public void postOrden(){
			// Hago recursion en todos los hijos
			if(!this.esHoja()){
				List<ArbolGeneral<T>> listaHijos = this.getHijos();
				foreach(var hijo in listaHijos)
					hijo.postOrden();
			}
			// Procesamos raiz
			Console.Write(this.getDatoRaiz() + " ");
		}
		public void PorNivelesMarcadoFin()
		{
//			Console.WriteLine("[====[RECORRIDO-POR-NIVELES]====]\n");
			Cola<ArbolGeneral<T>> cola=new Cola<ArbolGeneral<T>>();     //Intancia una cola vacia
			cola.encolar(this);                             //Obtiene el Nodo Raiz y lo encola
			cola.encolar(null);                                       //Encola null (para determinar cuando empieza el nuevo nivel)
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
		public List<int> PreordenMaquina(List<int> vacia)
		{
			List<int> nueva=vacia;
			if(Convert.ToInt32(this.getDatoRaiz())!=-1)
			{
				nueva.Add(Convert.ToInt32(this.getDatoRaiz()));
			}
			List<ArbolGeneral<T>> listaHijos = this.getHijos();
			foreach(var hijo in listaHijos)
			{
				if(Convert.ToInt32(hijo.getDatoRaiz())==-2)
				{
					listaHijos.Clear();
					Console.WriteLine("qweqwe: "+nueva[1]);
					return nueva;
				}
				if(Convert.ToInt32(hijo.getDatoRaiz())==-1)
				{
					return nueva;
				}
				nueva=hijo.PreordenMaquina(nueva);
				return nueva;
			}
			return nueva;
			
		}
		public void profundidad(int n, int profundidad)
		{
			var cola=new Cola<NodoGeneral<T>>();
			cola.encolar(this.getRaiz());
			cola.encolar(null);
			int nivel=1;
			string datos="";
			while(!cola.esVacia())
			{
				NodoGeneral<T> aux=cola.desencolar();
				if (aux == null)
				{
					cola.encolar(null);
					nivel++;
					if (cola.tope() == aux)
					{
						cola.desencolar();
					}

				}
				else
				{
					if (nivel>=n && nivel <= profundidad)
					{
						Console.Write("["+aux.getDato()+"]");
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
//					arbolAux.PorNivelesMarcadoFin();
					return arbolAux;
				}
//				Console.Write(arbolAux.getDatoRaiz() + " ");
				
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
			
			if(this.esHoja())
			{
				Console.Write("Posible secuencia de Jugada: ");
				foreach(var jugada in nueva)
				{
					Console.Write("["+jugada+"]");
				}
				Console.WriteLine("["+this.getDatoRaiz()+"]");
				return nueva;
			}
			else{
				List<ArbolGeneral<T>> listaHijos = this.getHijos();
				nueva.Add(Convert.ToInt32(this.getDatoRaiz()));
				foreach(var hijo in listaHijos)
				{
					nueva=hijo.Jugadas(nueva);
				}
				nueva.Remove(Convert.ToInt32(this.getDatoRaiz()));
			}
			return nueva;
		}
	}
}
