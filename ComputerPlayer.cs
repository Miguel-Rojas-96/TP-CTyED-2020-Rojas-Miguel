using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
	public class ComputerPlayer: Jugador
	{
		public ComputerPlayer(){}

		private List<int> naipesComputer = new List<int>();
		private int UltimaCartaUsuario;
		
		public override void incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
			naipesComputer=cartasPropias;
			bool Turno=true; //comienza tirando el usuario!
			//CompletarRama(new ArbolGeneral<int>(0),cartasPropias,cartasOponente,Turno,limite).PorNivelesMarcadoFin();
			List<int> usuario=new List<int>(){1,2,3};
			List<int> maquina=new List<int>(){4,5,6};
			ArbolGeneral<int> ArbolCompleto=CompletarRama(new ArbolGeneral<int>(0),usuario,maquina,Turno,limite);
//			ArbolCompleto.PorNivelesMarcadoFin();
//			HackMaquina(ArbolCompleto,9,Turno);
			ArbolGeneral<int> nuevo=InstalacionHeuristica(ArbolCompleto,Turno,7);
			ArbolEuristico=nuevo;
//			nuevo.PorNivelesMarcadoFin();
		}
		public ArbolGeneral<int> CompletarRama(ArbolGeneral<int> Hijo,List<int> CartasUsuario,List<int> CartasMaquina,bool Turno,int limite)
		{
			List<int> cartas=new List<int>();
			if(Turno==true)
			{
				cartas.AddRange(CartasUsuario);
			}
			else
			{
				cartas.AddRange(CartasMaquina);
			}
			foreach(int carta in cartas)
			{
				//Si mi funcion Heuristica, es decir si la base de mi ABGeneral tiene "getDatoRaiz=-1" gana Usuario, si tiene "getDatoRaiz=-2" gana Maquina...
				ArbolGeneral<int> hijo=new ArbolGeneral<int>(carta);
				Hijo.agregarHijo(hijo);
				int limiteaAux=limite-carta;
				List<int> cartasrestantes=new List<int>();
				cartasrestantes.AddRange(cartas);
				cartasrestantes.Remove(carta);
				if(limite>=0)
				{
					if(Turno==true)
					{
						bool turnito=false;
						CompletarRama(hijo,cartasrestantes,CartasMaquina,turnito,limiteaAux);
					}
					else{
						bool turnito=true;
						CompletarRama(hijo,CartasUsuario,cartasrestantes,turnito,limiteaAux);
					}
				}
			}
			return Hijo;
		}
		public ArbolGeneral<int> InstalacionHeuristica(ArbolGeneral<int> ArbolMiniMax,bool Turno,int limite)
		{
			if(limite<0)
			{
				if(Turno==true)
				{
					//En el nodo de mi arbol que sobrepase el limite se cortar y se debe poner como hijo de este el dato heuristico (-1 gano maquina,-2 gano usuario)
					ArbolMiniMax.ConvertirEnHoja();
					ArbolGeneral<int> Heuristic=new ArbolGeneral<int>(-1);
					ArbolMiniMax.agregarHijo(Heuristic);
					return Heuristic;
				}
				else
				{
					ArbolMiniMax.ConvertirEnHoja();
					ArbolGeneral<int> Heuristic=new ArbolGeneral<int>(-2);
					ArbolMiniMax.agregarHijo(Heuristic);
					return Heuristic;
				}
			}
			else
			{
				foreach(var hijo in ArbolMiniMax.getHijos())
				{
//					int LimiteAux=limite-ArbolMiniMax.getDatoRaiz(); corregido /
					int LimiteAux=limite-hijo.getDatoRaiz();
//					if(LimiteAux<0)
//					{
//						hijo.crearHoja();
//					}
					InstalacionHeuristica(hijo,!Turno,LimiteAux);
				}
				return ArbolMiniMax;
			}
		}
		public ArbolGeneral<int> ArbolHeuristico(ArbolGeneral<int> Hijo,List<int> CartasUsuario,List<int> CartasMaquina,bool Turno,int limite)
		{
			List<int> cartas=new List<int>();
			if(Turno==true)
			{
				cartas.AddRange(CartasUsuario);
			}
			else
			{
				cartas.AddRange(CartasMaquina);
			}
			foreach(int carta in cartas)
			{
				//Si mi funcion Heuristica, es decir si la base de mi ABGeneral tiene "getDatoRaiz=-1" gana Usuario, si tiene "getDatoRaiz=-2" gana Maquina...
				ArbolGeneral<int> hijo=new ArbolGeneral<int>(carta);
				Hijo.agregarHijo(hijo);
				int limiteaAux=limite-carta;
				List<int> cartasrestantes=new List<int>();
				cartasrestantes.AddRange(cartas);
				cartasrestantes.Remove(carta);
				if((limite<0)&&Turno==true)
				{
					ArbolGeneral<int> Heuristic=new ArbolGeneral<int>(-2);
					hijo.agregarHijo(Heuristic);
				}
				if((limite<0)&&Turno==false)
				{
					ArbolGeneral<int> Heuristic=new ArbolGeneral<int>(-1);
					hijo.agregarHijo(Heuristic);
				}
				if(limite>=0)
				{
					if(Turno==true)
					{
						bool turnito=false;
						ArbolHeuristico(hijo,cartasrestantes,CartasMaquina,turnito,limiteaAux);
					}
					else{
						bool turnito=true;
						ArbolHeuristico(hijo,CartasUsuario,cartasrestantes,turnito,limiteaAux);
					}
				}
			}
			return Hijo;
		}	
		public int  JuagadaDeLaMaquina()
		{
			ArbolGeneral<int> NuevoArbol=ArbolEuristico.CortarArbol(UltimaCartaUsuario);
			ArbolEuristico=NuevoArbol;
			List<int> nueva=new List<int>();
			List<int> SiguienteJugada=NuevoArbol.PreordenMaquina(nueva);
			Console.ReadKey();
			Console.WriteLine("Carta de La Maquina:         "+"["+SiguienteJugada[1]+"]");
			return SiguienteJugada[1];
		}
		
		public override int descartarUnaCarta()
		{
			int carta = 0;
			Console.Write("Naipes disponibles (Maquina):");
			for (int i = 0; i < naipesComputer.Count; i++) {
				Console.Write("["+naipesComputer[i].ToString()+"]");
			}
			Console.WriteLine();
			carta = JuagadaDeLaMaquina();
			return carta;
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
