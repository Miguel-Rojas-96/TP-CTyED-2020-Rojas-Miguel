
using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{

	public class Game
	{
		public static int WIDTH = 12;
		public static int UPPER = 35;
		public static int LOWER = 25;
		
		private Jugador player1 = new ComputerPlayer();
		private Jugador player2 = new HumanPlayer();
		private List<int> naipesHuman = new List<int>();
		private List<int> naipesComputer = new List<int>();
		private int limite=7;
		private bool juegaHumano = false;
		
		//DETERMINA EL LIMITE DE MANERA ALEATORIA ENTRE EL INTERVALO 25 Y 35
		public Game()
		{
//			var rnd = new Random();
//			limite = rnd.Next(LOWER, UPPER);
//			
//			naipesHuman = Enumerable.Range(1, WIDTH).OrderBy(x => rnd.Next()).Take(WIDTH / 2).ToList();
//			
//			for (int i = 1; i <= WIDTH; i++) {
//				if (!naipesHuman.Contains(i)) {
//					naipesComputer.Add(i);
//				}
//			}
			naipesComputer.Add(4);
			naipesComputer.Add(5);
			naipesComputer.Add(6);
			naipesHuman.Add(1);
			naipesHuman.Add(2);
			naipesHuman.Add(3);
				
			player1.incializar(naipesComputer, naipesHuman, limite);
			player2.incializar(naipesHuman, naipesComputer, limite);
			
		}
		
		
		private void printScreen()
		{
			Console.WriteLine();
			Console.WriteLine("Limite:" + limite.ToString());
		}
		private void turn(Jugador jugador, Jugador oponente, List<int> naipes)
		{
			int carta = jugador.descartarUnaCarta();
			naipes.Remove(carta);
			limite -= carta;
			oponente.cartaDelOponente(carta);
			juegaHumano = !juegaHumano;
		}
		private void printWinner()
		{
			if (!juegaHumano) {
				Console.WriteLine("Ganaste La Partida!!!");
			}
			else
			{
				Console.WriteLine("La Maquina Gano La Partida!!!");
			}
			
		}
		private bool fin()
		{
			return limite <0;
		}
		
		public void play()
		{
			while (!this.fin()) 
			{
				this.printScreen();
				this.turn(player2, player1, naipesHuman); // Juega el usuario
				if (!this.fin()) 
				{
					this.printScreen();
					this.turn(player1, player2, naipesComputer); // Juega la IA
				}
			}
			this.printWinner();
		}
	}
}
