
using System;
using System.Collections.Generic;
using System.Linq;


namespace juegoIA
{

	public class HumanPlayer : Jugador
	{
		public HumanPlayer(){}
		private List<int> naipes = new List<int>();
		private List<int> naipesComputer = new List<int>();
		private int limite;
		private bool random_card = false;
		private int cartadeloponente;

		
		public HumanPlayer(bool random_card)
		{
			this.random_card = random_card;
		}
		
		public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
			this.naipes = cartasPropias;
			this.naipesComputer = cartasOponente;
			this.limite = limite;
		}
		public override int descartarUnaCarta()
		{
			int carta = 0;
			Console.Write("Naipes disponibles (Usuario):");
			for (int i = 0; i < naipes.Count; i++) {
				Console.Write("["+naipes[i].ToString()+"]");
			}
			Console.WriteLine();
			if (!random_card)
			{
				Console.Write("Ingrese naipe               :");
				string entrada = Console.ReadLine();
				switch(entrada)
				{
					case "101":
						HumanPlayer.ArbolEuristico.CortarArbol(cartadeloponente).PorNivelesMarcadoFin();
						break;
					case "102":
						HumanPlayer.ArbolEuristico.CortarArbol(cartadeloponente).Jugadas(new List<int>());
						break;
					case "103":
						Console.Write("Imprimir nodos del arbol en la Profundidad: ");
						int profundidad=int.Parse(Console.ReadLine());
						Console.Write("[Profundidad-"+profundidad+"]: ");
						HumanPlayer.ArbolEuristico.CortarArbol(cartadeloponente).profundidad(profundidad,profundidad);
						Console.WriteLine();
						break;
					case "104":
						//Nuevo Juego
						Menus nuevo=new Menus();
						nuevo.IniciandoJuego();
						break;
					case "105":
						//Cerrar Juego
						Environment.Exit(0);
						break;
						
				}
				Int32.TryParse(entrada, out carta);
				while (!naipes.Contains(carta)) {
					Console.Write("Ingrese naipe               :");
					entrada = Console.ReadLine();
					switch(entrada)
					{
						case "101":
							HumanPlayer.ArbolEuristico.CortarArbol(cartadeloponente).PorNivelesMarcadoFin();
							break;
						case "102":
							HumanPlayer.ArbolEuristico.CortarArbol(cartadeloponente).Jugadas(new List<int>());
							break;
						case "103":
							Console.Write("Imprimir nodos del arbol en la Profundidad: ");
							int profundidad=int.Parse(Console.ReadLine())-1;
							Console.Write("[Profundidad-"+profundidad+"]: ");
							HumanPlayer.ArbolEuristico.CortarArbol(cartadeloponente).profundidad(profundidad,profundidad);
							Console.WriteLine();
							break;
						case "104":
							//Nuevo Juego
							Menus nuevo=new Menus();
							nuevo.IniciandoJuego();
							break;
						case "105":
							//Cerrar Juego
							Environment.Exit(0);
							break;
							
					}
					Int32.TryParse(entrada, out carta);
				}
			}
			else
			{
				var random = new Random();
				int index = random.Next(naipes.Count);
				carta = naipes[index];
				Console.Write("Ingrese naipe:" + carta.ToString());
			}
			
			return carta;
		}
		public override void cartaDelOponente(int carta)
		{
			cartadeloponente=carta;
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("La carta del oponente es    :"+"["+carta+"]");
			Console.WriteLine("=======================================================================================================================");
		}
	}
}
