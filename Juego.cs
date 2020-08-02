
using System;
using System.Threading;
using System.Collections;

namespace juegoIA
{
	class Juego
	{
		public static void Main(string[] args)
		{
			Menus nuevo=new Menus();
			nuevo.MenuPrincipal();
			Console.ReadKey();
		}
	}
	public class Menus
	{
		public ArrayList Loading=new ArrayList();
		public  int LoadingGame=1;
		public void MenuPrincipal()
		{
			Console.Clear();
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("                                                  JUEGO-DE-CARTAS                                                      ");
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("1]INICIAR JUEGO.");
			Console.WriteLine("2]SALIR.");
			Console.WriteLine("=======================================================================================================================");
			Console.Write("ELIJA OPCION: ");
			int modulo=int.Parse(Console.ReadLine());
			do{
				switch(modulo){
					case 1:
						IniciandoJuego();
						break;
					case 2:
						Environment.Exit(0);
						break;
				}
			}
			while(modulo<4);
			MenuPrincipal();
		}
		public void IniciandoJuego()
		{
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("                                                 INICIANDO-JUEGO                                                       ");
			Console.WriteLine("=======================================================================================================================");
			Console.Write("\n  Loading   ");
			string BarraDeCarga="";
			while(LoadingGame<=100)
			{
				BarraDeCarga=BarraDeCarga+"I";
				Iniciador(BarraDeCarga,LoadingGame);
				LoadingGame++;
			}
//			Console.WriteLine("\n\n\t\t\t\t\t[/(*U*)/][JUEGO-CARGADO-EXITOSAMENTE][/(*U*)/]");
//			Console.ReadKey();
			Console.Clear();
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("                                                   MENU-GAME                                                           ");
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("101]Imprimir resultados disponibles del Arbol Heuristico.");
			Console.WriteLine("102]Imprimir todas las jugadas posibles.");
			Console.WriteLine("103]Imprimir jugadas de una determinada profundidad.");
			Console.WriteLine("104]Comenzar nueva partida.");
			Console.WriteLine("105]Terminar Juego.");
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("ACLARACION!=>Los jugadores pueden seleccionar cualquiera de estas opciones (001,...,005) durante la partida.\n" +
			                  "             A continuacion de la linea <INGRESE NAPIE> escribiendo los 3 digitos correspondiente a la opcion...");
			Console.WriteLine("=======================================================================================================================");
			Game game = new Game();
			game.play();
			Console.ReadKey(true);
		}
		public void Iniciador(string BarraDeCarga,int LoadingGame)
		{
			Console.Clear();
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("                                                 INICIANDO-JUEGO                                                       ");
			Console.WriteLine("=======================================================================================================================");
			Thread.Sleep(17);
			Console.Write("\n  Loading ["+BarraDeCarga+"]"+"["+LoadingGame+"%]");
			Console.Write("\n\n");
		}
		public void IniciadorMiniMax(string BarraDeCarga,int LoadingGame)
		{
			Console.Clear();
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("                                                 CONSTRUYENDO-ARBOL                                                    ");
			Console.WriteLine("=======================================================================================================================");
			Thread.Sleep(17);
			Console.Write("\n  Loading ["+BarraDeCarga+"]"+"["+LoadingGame+"%]");
			Console.Write("\n\n");
		}
	}
}