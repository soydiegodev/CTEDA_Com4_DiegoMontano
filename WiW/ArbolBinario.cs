using System;

namespace tp2
{
	public class ArbolBinario<T>
	{
		
		private T dato;
		private ArbolBinario<T> hijoIzquierdo;
		private ArbolBinario<T> hijoDerecho;
	
		
		public ArbolBinario(T dato) {
			this.dato = dato;
		}
	
		public T getDatoRaiz() {
			return this.dato;
		}
	
		public ArbolBinario<T> getHijoIzquierdo() {
			return this.hijoIzquierdo;
		}
	
		public ArbolBinario<T> getHijoDerecho() {
			return this.hijoDerecho;
		}
	
		public void agregarHijoIzquierdo(ArbolBinario<T> hijo) {
			this.hijoIzquierdo=hijo;
		}
	
		public void agregarHijoDerecho(ArbolBinario<T> hijo) {
			this.hijoDerecho=hijo;
		}
	
		public void eliminarHijoIzquierdo() {
			this.hijoIzquierdo=null;
		}
	
		public void eliminarHijoDerecho() {
			this.hijoDerecho=null;
		}
	
		public bool esHoja() {
			return this.hijoIzquierdo==null && this.hijoDerecho==null;
		}
		//

		public void preorden() {
			Console.WriteLine(getDatoRaiz());
			if (this.getHijoIzquierdo() != null){
				getHijoIzquierdo().inorden();
			}
			if(this.getHijoDerecho() != null){
				getHijoDerecho().inorden();
			}
		}
		
		public void postorden() {
			if (this.getHijoIzquierdo() != null){
				getHijoIzquierdo().inorden();
			}
			if(this.getHijoDerecho() != null){
				getHijoDerecho().inorden();
			}
			Console.WriteLine(getDatoRaiz());
		}
		
		public void inorden(){
			if (this.getHijoIzquierdo() != null){
				getHijoIzquierdo().inorden();
			}
			Console.WriteLine(getDatoRaiz());
			if(this.getHijoDerecho() != null){
				getHijoDerecho().inorden();
			}
		}
		
		public void recorridoPorNiveles() {
			Cola<ArbolBinario<T>> cola = new Cola<ArbolBinario<T>>();
			cola.encolar(this);
			while(cola.esVacia()){
				ArbolBinario<T> arbolActual = cola.desencolar();
				Console.WriteLine(arbolActual.getDatoRaiz());
				if (arbolActual.getHijoIzquierdo() != null){
					cola.encolar(arbolActual.getHijoIzquierdo());
				}
				if (arbolActual.getHijoDerecho() != null){
					cola.encolar(arbolActual.getHijoDerecho());
				}
			}
			
		}
	
		public int contarHojas() {
			return 0;
		}
		//n = nivel minimo, m = nivel maximo
		public void recorridoEntreNiveles(int n,int m) {
			if (n > m || n<1){
				return;
			}
			Cola< Tuple<ArbolBinario<T>,int> > cola = new Cola< Tuple<ArbolBinario<T>,int> >();
			cola.encolar(new Tuple<ArbolBinario<T>,int> (this,1));
			while(cola.esVacia()){
				Tuple<ArbolBinario<T>,int> arbolNivel = cola.desencolar();
				if (arbolNivel.Item2 > m){
					break;
				} 
				if (arbolNivel.Item2 >= n && arbolNivel.Item2 <= m){
					Console.WriteLine(arbolNivel.Item1.getDatoRaiz());
				}
				if (arbolNivel.Item2 < m){
					if (arbolNivel.Item1.getHijoIzquierdo() != null){
						cola.encolar(new Tuple<ArbolBinario<T>,int> (arbolNivel.Item1.getHijoIzquierdo(),arbolNivel.Item2+1));
					}
					if (arbolNivel.Item1.getHijoDerecho() != null){
						cola.encolar(new Tuple<ArbolBinario<T>,int> (arbolNivel.Item1.getHijoDerecho(),arbolNivel.Item2+1));
					}
				}
			}
		}
	}
}






