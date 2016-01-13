using System.IO;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Coletor : MonoBehaviour {
	
	//Utilizado para poder imprimir o resultado mais recente.
	private Stack<String> pilha = new Stack<String>();
	
	private string caminhoArquivo;
	
	void Awake() {
		caminhoArquivo = Application.persistentDataPath + " Coletor.csv"; // Pegando o caminho certo.
		Debug.Log(caminhoArquivo);
	}
	
	public void SaveToFile(string dados){
		//Se o arquivo ja existir.
		if(File.Exists (caminhoArquivo)) {
			using (StreamWriter sw = new StreamWriter(caminhoArquivo,true)){
				sw.WriteLine (dados);
				sw.Close ();	
			} 
		}
		//Se nao existir o arquivo, ele e criado.
		else{
			using(FileStream fs = File.Create(caminhoArquivo)){
				using (StreamWriter sw = new StreamWriter(fs)){
					sw.WriteLine ("sep=,");
					sw.WriteLine ("NumeroOnda, TaxaExterminioNaves, TaxaExterminioAsteroides, TaxaColisaoNaves, TaxaColisaoAsteroides, " +
						"DelayTiro, TirosAlvejados, UltimaCampanha100Kill, " +
						"QuantidadeMovimentos, NomeCapitao");
					sw.WriteLine (dados);
					sw.Close ();
				}
			}
		}
	}
	
	//Para ler os dados dos jogos no box da coleta(Menu)
	public string ReadFromFile(){
		
		//Os dados deverao ser concatenados junto a essa string.
		string coleta = "";
		
		//Pega o caminho do arquivo e le ate que nao reste mais nada a ser lido.
		if(File.Exists(caminhoArquivo)){
			try{
				using (StreamReader sr = new StreamReader(caminhoArquivo)){
					string linha;
					while((linha = sr.ReadLine())!= null){
						if(linha.Equals("sep=,") || linha.Equals("NumeroOnda, TaxaExterminioNaves, TaxaExterminioAsteroides, TaxaColisaoNaves, TaxaColisaoAsteroides, " +
						                                         "DelayTiro, TirosAlvejados, UltimaCampanha100Kill, " +
						                                         "QuantidadeMovimentos,NomeCapitao")){
							continue;
						}else{
							//Gatilho para finalizr a insercao de itens na pilha.
							//Problema: StreamReader nao parava de adicionar elementos ja lidos, ficando assim em looping.
							if(!pilha.Contains(linha)){
								pilha.Push(linha);
							}
						}
					}
				}
				
			}catch(Exception e){
				Debug.Log(e.Message);
			}
		}

		
		//Desempilhando e formatando para assim apresentar.
		for(int i = pilha.Count; i != 0; i--){
			coleta += formata(pilha.Pop());
		}

		//Retornando string que contem todos os resultados que serao impressos na tela.
		return coleta;
	}
	
	//Formatar como os dados irao aprecer no menu estatisticas.
	public String formata(string linha){
		string linha_atual = "";
		for(int i = 0 ; i < linha.Length; i++){
			if(linha[i].Equals(',')){
				linha_atual += "\t";
			}else{
				linha_atual += linha[i];
			}
		}
		return (linha_atual + "\n");
	}
}