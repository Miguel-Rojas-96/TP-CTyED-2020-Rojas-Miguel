
using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
	public class ComputerPlayer: Jugador
	{
		
		public ComputerPlayer()
		{
		}
		
		public override void incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
			bool Turno=true; //comienza tirando el usuario!
			List<int> usuario=new List<int>(){1,2};
			List<int> maquina=new List<int>(){3,4};			
			ArbolGeneral<int> ArbolCompleto=CompletarRama(new ArbolGeneral<int>(0),usuario,maquina,Turno,limite);
			ArbolCompleto.PorNivelesMarcadoFin();

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
		
		
		public override int descartarUnaCarta()
		{
			//Implementar
			return 0;
		}
		
		public override void cartaDelOponente(int carta)
		{
			//implementar
			
		}
		
	}
}
