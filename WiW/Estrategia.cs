
using System;
using System.Collections.Generic;
//using tp1;
using tp2;
using System.Linq;
using System.Text;
namespace tpfinal
{

	class Estrategia
	{
		/* ------------------------------------------------------------------------------------------------
		1. CrearArbol (Clasificador clasificador): Este método construye un árbol de decisión a
		partir de un clasificador que es enviado como parámetro y retorna una instancia de
		ArbolBinario<DecisionData>.
		*/
		public ArbolBinario<DecisionData> CrearArbol(Clasificador clasificador)
		{
			// si es clasificador es hoja
			if (clasificador.crearHoja()){
				// predicciones en nodo hoja
				Dictionary<string, int> datosHoja = clasificador.obtenerDatoHoja();
				return new ArbolBinario<DecisionData>(new DecisionData(datosHoja));
			}else{
				// se guarda la pregunta en una variable, con ella se crea una DecisionData y un nuevo arbol
				Pregunta preguntaActual = clasificador.obtenerPregunta();
				ArbolBinario<DecisionData> arbol = new ArbolBinario<DecisionData>(new DecisionData(preguntaActual));
				// se obtiene los clasificadores y se hace llamadas recursivas para crear un arbol izq y un arbol der con ellos
				Clasificador clasificadorIzq = clasificador.obtenerClasificadorIzquierdo();
				Clasificador clasificadorDer = clasificador.obtenerClasificadorDerecho();
				arbol.agregarHijoIzquierdo(CrearArbol(clasificadorIzq));
				arbol.agregarHijoDerecho(CrearArbol(clasificadorDer));
				return arbol;
			}
		}
		/* ------------------------------------------------------------------------------------------------
		2. Consulta1 (ArbolBinario< DecisionData > arbol): Retorna un texto con todas las posibles 
		predicciones que puede calcular el árbol de decisión del sistema
		*/
		public String Consulta1(ArbolBinario<DecisionData> arbol)
		{
			// se usa un metodo que recorre el arbol en preorden modificado,
			// al metodo se le pasa por parametro el arbol a recorrer y un string pasado por ref donde 
			// quiero que se concatene todas las predicciones que estan en los nodos hojas.
			string resultado="";
			ObtenerPredicciones(arbol, ref resultado);
			return resultado;
		}
		private void ObtenerPredicciones(ArbolBinario<DecisionData> arbol,ref string predicciones){
			if(arbol == null){
				return;
			}
			if (!arbol.esHoja()){
				if (arbol.getHijoIzquierdo() != null){
					ObtenerPredicciones(arbol.getHijoIzquierdo(),ref predicciones);
				}
				if(arbol.getHijoDerecho() != null){
					ObtenerPredicciones(arbol.getHijoDerecho(),ref predicciones);
				}
			}else{
				// se concatena las predicciones de un nodo hojas, en un string "prediciones" pasado por ref
				// se obtiene el dato raiz de una hoja que debe de ser un DecisionData 
				// y se obtienen las prediccion con el metodo Tostring();
				predicciones += arbol.getDatoRaiz().ToString() +"\n";
			}
		}
		/* ------------------------------------------------------------------------------------------------
		3. Consulta2 (ArbolBinario< DecisionData > arbol): Retorna un texto que contiene todos los caminos 
		hasta cada predicción.
		 */
		public String Consulta2(ArbolBinario<DecisionData> arbol)
		{
			// se obtiene los caminos en una lista y luego se concatena los caminos a un string para ser mostrado
			// ademas como plus se "dibuja el arbol de dicision para que se vea el resultado de forma mas visual.
			string camino ="";
			List<string>caminos= new List<string>();
			string todosLosCaminosHastaPrediccion ="";
			string r= DibujarArbol(arbol, "", "");
			
			ObtenerCaminos(arbol,ref camino,ref caminos);
			foreach ( string c in caminos){
				todosLosCaminosHastaPrediccion += c ;
			}
			return todosLosCaminosHastaPrediccion +"\n"+ r;

		}
			
		private void ObtenerCaminos(ArbolBinario<DecisionData> arbol, ref string camino,ref List<string> caminos){
			// una variacion del metodo obtenerPredicciones() , este ademas de recorrer hasta las hojas(predicciones), 
			// concatena el nodo(Pregunta) en el camino hasta llegar a una hoja donde todo el camino se agregas a una lista de caminos.
			if(arbol == null){
				return;
			}
			if (!arbol.esHoja()){
				string caminoAnterior = camino;
				string pregunta = arbol.getDatoRaiz().Question.Texto; //.TextoParaUsuario();
				if (arbol.getHijoIzquierdo() != null){
					camino = camino + pregunta + "= NO -> ";
					ObtenerCaminos(arbol.getHijoIzquierdo(),ref camino, ref caminos);
				}
				camino = caminoAnterior;
				if(arbol.getHijoDerecho() != null){
					camino = camino + pregunta + "= SI -> ";
					ObtenerCaminos(arbol.getHijoDerecho(),ref camino,ref caminos);
				}
				camino = caminoAnterior;
			}else{
				string prediccion = arbol.getDatoRaiz().ToString();
				caminos.Add(camino + prediccion + "\n");
			}
		}
		private string DibujarArbol( ArbolBinario<DecisionData> arbol, string indent,string rama){
			if (arbol == null){
        		return "";
			}
	        string resultado = indent  ;
			if (rama == "Izq - NO" ){
	        	resultado += "- [ "+rama+" ]" ;
	        }else if ( rama == "Der - SI"){
	        	resultado += "| - [ "+rama+" ]" ;
	        }
	        if (!arbol.esHoja()){
	       		resultado += "\n" + indent + "- "+arbol.getDatoRaiz().Question.Texto +"\n";
	        }else{
	        	resultado += " - "+ arbol.getDatoRaiz().ToString()  +"\n";
	        }
	
	        if (arbol.getHijoIzquierdo() != null){
				string nuevoIndent = indent + (arbol.getHijoDerecho() != null ?  "	│":"   	" );
	        	resultado +=  DibujarArbol(arbol.getHijoIzquierdo(), nuevoIndent, "Izq - NO")  ;
	        }
	        if (arbol.getHijoDerecho() != null){
	        		string nuevoIndent =  indent + "	 ";
	                resultado +=  DibujarArbol(arbol.getHijoDerecho(), nuevoIndent, "Der - SI") ;
	        }
	        return resultado;
	    }

		/* ------------------------------------------------------------------------------------------------
		4. Consulta3 (ArbolBinario< DecisionData > arbol): Retorna un texto que contiene los datos 
		almacenados en los nodos del árbol diferenciados por el nivel en que se encuentran.
		 */
		public String Consulta3(ArbolBinario<DecisionData> arbol)
		{
			// se usa la logica de recorrido entre niveles para mostrar el resultado que se quiere.
			string resultado;
			Cola<ArbolBinario<DecisionData>> colaArboles = new Cola<ArbolBinario<DecisionData>>();
			Cola<int> colaNiveles = new Cola<int>();
			int nivelActual = 1;
			colaArboles.encolar(arbol);
			colaNiveles.encolar(nivelActual);
			resultado = "Nivel: " + nivelActual +"\n";
			while(!colaArboles.esVacia()){
				ArbolBinario<DecisionData> arbolActual = colaArboles.desencolar();
				int nivelArbol = colaNiveles.desencolar();
				if (nivelArbol > nivelActual){
					nivelActual = nivelArbol;
					resultado += "\n"+ "Nivel: " + nivelActual + "\n" ;
				}
				resultado += arbolActual.getDatoRaiz().ToString();
				if( arbolActual.getHijoIzquierdo() != null){
					colaArboles.encolar(arbolActual.getHijoIzquierdo());
					colaNiveles.encolar(nivelActual +1);
				}
				if (arbolActual.getHijoDerecho() != null){
					colaArboles.encolar(arbolActual.getHijoDerecho());
					colaNiveles.encolar(nivelActual +1);
				}
			}
			return resultado;
		}
	}
}