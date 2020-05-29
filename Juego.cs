
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
			Console.WriteLine("1]COMENZAR A JUGAR...");
			Console.WriteLine("2]CONSTRUIR ÁRBOL MiniMax...");
			Console.WriteLine("3]INSTRUCCIONES...");
			Console.WriteLine("4]SALIR");
			Console.WriteLine("=======================================================================================================================");
			Console.Write("ELIJA OPCION: ");
			int modulo=int.Parse(Console.ReadLine());
			do{
				switch(modulo){
					case 1:
						IniciandoJuego();
						break;
					case 2:
						ConstruirMiniMax();
						break;
					case 3:
						Instrucciones();
						break;
					case 4:
						Environment.Exit(0);
						break;
				}
			}
			while(modulo<4);
			MenuPrincipal();
		}
		public void ConstruirMiniMax()
		{
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("                                                CONSTRUIR-MiniMax                                                      ");
			Console.WriteLine("=======================================================================================================================");
		}
		public void IniciandoJuego()
		{
//			Console.WriteLine("=======================================================================================================================");
//			Console.WriteLine("                                                 INICIANDO-JUEGO                                                       ");
//			Console.WriteLine("=======================================================================================================================");
//			Console.Write("\n  Loading   ");
//			string BarraDeCarga="";
//			while(LoadingGame<=100)
//			{
//				BarraDeCarga=BarraDeCarga+"I";
//				Iniciador(BarraDeCarga,LoadingGame);
//				LoadingGame++;
//			}
//			Console.WriteLine("\n\n\t\t\t\t\t[/(*U*)/][JUEGO-CARGADO-EXITOSAMENTE][/(*U*)/]");
			Console.Clear();
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("                                                   MENU-GAME                                                           ");
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("001]Imprimir resultados disponibles actualmente.");
			Console.WriteLine("002]Imprimir todos los resultados posibles.");
			Console.WriteLine("003]Imprimir jugadas de una determinada profundidad.");
			Console.WriteLine("004]Terminar Juego.");
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("ACLARACION!=>Los jugadores pueden seleccionar cualquiera de estas opciones (001,...,004) durante la partida.\n" +
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
			Thread.Sleep(35);
			Console.Write("\n  Loading ["+BarraDeCarga+"]"+"["+LoadingGame+"%]");
			Console.Write("\n\n");
		}
		public void Instrucciones()
		{
			Console.Clear();
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("                                                  INSTRUCCIONES                                                        ");
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("BLABLALBALBLASDLASLGAS\nASDQWDOKQPWFQPWFOKQKFWWFQKOPFW\naspofkaosgkopakopsgkoakopsgaskopkopagskop\nASDASDASFQPOOP1231234\nwqeqweqwrqwr\npQWPEQWRQRWPRWQPRWQPWEQPWEQWQEORWQORWQ\nskpasdkpkoqweqwrqwerqwerrqwe\nosadsfaokfsasafkofsakooqwerqw");
			Console.WriteLine("=======================================================================================================================");
			Console.WriteLine("ENTENDIO?");
			Console.WriteLine("-PRESS ENTER PARA VOLVER ATRAS[<-]");
			Console.ReadKey(true);
			MenuPrincipal();
			
		}
	}
}