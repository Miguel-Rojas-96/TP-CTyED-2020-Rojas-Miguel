using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
	public class ComputerPlayer: Jugador
	{
		public ComputerPlayer(){}
		private int UltimaCartaUsuario;		
		private List<int> naipesComputer;
		
		public override void incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
			bool Turno=true; //comienza tirando el usuario!
			List<int> usuario=new List<int>(){1,2,3}; 
			List<int> maquina=new List<int>(){4,5,6};
			naipesComputer=maquina;
			ArbolGeneral<int> ArbolCompleto=CompletarRama(new ArbolGeneral<int>(0),usuario,maquina,Turno,limite); 
			ArbolGeneral<int> Arbol=InstalacionHeuristica(ArbolCompleto,Turno,7);
			ArbolEuristico=Arbol;  //Guarda el Arbol Heuristico en el Campo de la clase base llamado "ArbolEuristico" para que todas las
			                       //clases que hereden de esta clase base tambien tengan disponible este dato.
		}
		public ArbolGeneral<int> CompletarRama(ArbolGeneral<int> Hijo,List<int> CartasUsuario,List<int> CartasMaquina,bool Turno,int limite)
		{
			//Empieza Jugando Usuario->Turno=true(Juega Usuario)->Turno=false(Juega Maquina)
			List<int> cartas=new List<int>();
			if(Turno==true)
			{
				cartas.AddRange(CartasUsuario); //Si Juega Usuario guardo todas las cartas de este en la variable "cartas"
			}
			else
			{
				cartas.AddRange(CartasMaquina); //Si Juega Maquina guardo todas las cartas de este en la variable "cartas"
			}
			foreach(int carta in cartas)        //Recorre la lista de cartas Correspondiente al Jugador
			{
				ArbolGeneral<int> hijo=new ArbolGeneral<int>(carta); //Realiza una instancia de ArbolGeneral con DatoRaiz carta
				Hijo.agregarHijo(hijo);                              //Agrega al Arbol la instancia hijo creada anteriormente
				int limiteaAux=limite-carta;                         //Decrementa el limite en base a el dato de la carta
				List<int> cartasrestantes=new List<int>();
				cartasrestantes.AddRange(cartas);                    //Guardo todas las cartas en lista cartas restantes
				cartasrestantes.Remove(carta);                       // y elimino la carta que se esta recorriendo de la lista "cartas".
				if(limite>=0)                                        //Si limite es menor a cero empieza a armar la siguiente rama
				{
					if(Turno==true)
					{
						CompletarRama(hijo,cartasrestantes,CartasMaquina,!Turno,limiteaAux); //Hace llamada recursiva cambia el turno, envia la lista con
					}                                                                        //la carta eliminada y el limite reducido
					else
					{
						CompletarRama(hijo,CartasUsuario,cartasrestantes,!Turno,limiteaAux);
					}
				}
			}
			return Hijo;
		}
		public ArbolGeneral<int> InstalacionHeuristica(ArbolGeneral<int> ArbolMiniMax,bool Turno,int limite)
		{
			//Si mi funcion Heuristica, es decir si la base de mi ABGeneral tiene "getDatoRaiz=-1" gana Usuario
			//si tiene "getDatoRaiz=-2" gana Maquina...
			if(limite<0)
			{
				//En el nodo de mi arbol que se sobrepase el limite se corta y se debe poner como hijo de este el dato heuristico (-1 gano maquina,-2 gano usuario)
				//dependiendo de quien sea el turno.
				if(Turno==true)
				{
					ArbolMiniMax.ConvertirEnHoja(); //El nodo en el que se alcanza el limite se convierte en Hoja y en este se agrega el dato heuristico
					ArbolGeneral<int> Heuristic=new ArbolGeneral<int>(-1);
					ArbolMiniMax.agregarHijo(Heuristic);
				}
				else
				{
					ArbolMiniMax.ConvertirEnHoja();
					ArbolGeneral<int> Heuristic=new ArbolGeneral<int>(-2);
					ArbolMiniMax.agregarHijo(Heuristic);
				}
			}
			//Mientras no se supere el limite recorro los hijos de mi nodo
			else
			{
				foreach(var hijo in ArbolMiniMax.getHijos())
				{
					int LimiteAux=limite-hijo.getDatoRaiz();
					InstalacionHeuristica(hijo,!Turno,LimiteAux);
				}
			}
			return ArbolMiniMax;
		}
		public override int descartarUnaCarta()
		{
			Console.Write("Naipes disponibles (Maquina):");
			for (int i = 0; i < naipesComputer.Count; i++) {
				Console.Write("["+naipesComputer[i].ToString()+"]");  //Muestra las cartas que tiene disponible la Maquina
			}
			Console.WriteLine();
			ArbolGeneral<int> NuevoArbol=ArbolEuristico.CortarArbol(UltimaCartaUsuario); //Corta el arbol en la ultima jugada que realizo el Usuario
			ArbolEuristico=NuevoArbol;
			List<int> SiguienteJugada=NuevoArbol.BuscarJugada(new List<int>());       //En el arbol cortado busca el camino que tenga como dato Heuristico -2
			                                                                             //y lo guarda en la lista SiguienteJugada
			Console.WriteLine("Carta de La Maquina:         "+"["+SiguienteJugada[1]+"]");
			                                                                            //Devuelve la carta a jugar. Esta seria la carta que se ecuentra en la 
			return  SiguienteJugada[1];;                                          //posicion 1 de mi lista que guardo el camino que tiene el dato Heuristico -2.
		}
		public override void cartaDelOponente(int carta)
		{
			UltimaCartaUsuario=carta;                                  
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("La carta del oponente es    :"+"["+carta+"]");
			Console.WriteLine("=======================================================================================================================");
			//implementar
		}
	}
}
