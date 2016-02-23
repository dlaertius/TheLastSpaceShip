using System.IO;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Coletor : MonoBehaviour {
	
	//Utilizado para poder imprimir o resultado mais recente.
	private Stack<String> pilha = new Stack<String>();
	
	private string caminhoArquivoModeler;
	private string caminhoArquivoTrainning;
	
	void Awake() {
		caminhoArquivoModeler = Application.persistentDataPath + " DataUserModeler.csv"; // Take the right way to Colect csv file.
		caminhoArquivoTrainning = Application.persistentDataPath + " Colector.csv"; // Take the right way to Colect csv file.
		Debug.Log(caminhoArquivoModeler);
		Debug.Log(caminhoArquivoTrainning);
	}

	/*
	 * Used in the first step of this project to colect players data.
	 */
	public void SaveToFile(string dados){
		//Se o arquivo ja existir.
		if(File.Exists (caminhoArquivoTrainning)) {
			using (StreamWriter sw = new StreamWriter(caminhoArquivoTrainning,true)){
				sw.WriteLine (dados);
				sw.Close ();	
			} 
		}
		//Se nao existir o arquivo, ele e criado.
		else{
			using(FileStream fs = File.Create(caminhoArquivoTrainning)){
				using (StreamWriter sw = new StreamWriter(fs)){
					sw.WriteLine ("sep=,");
					sw.WriteLine ("Score, Energia, TaxaExterminioNaves, TaxaExterminioAsteroides, TaxaColisaoNaves, TaxaColisaoAsteroides, " +
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
		if(File.Exists(caminhoArquivoTrainning)){
			try{
				using (StreamReader sr = new StreamReader(caminhoArquivoTrainning)){
					string linha;
					while((linha = sr.ReadLine())!= null){
						if(linha.Equals("sep=,") || linha.Equals("Score, Energia, TaxaExterminioNaves, TaxaExterminioAsteroides, TaxaColisaoNaves, TaxaColisaoAsteroides, " +
						                                         "DelayTiro, TirosAlvejados, UltimaCampanha100Kill, " +
						                                         "QuantidadeMovimentos, NomeCapitao")){
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

	public void SaveToFileRecomenationsLine(string recommendationsDuringGame, string MajorOccurrence) {
		//Se o arquivo ja existir.
		if(File.Exists (caminhoArquivoModeler)) {
			using (StreamWriter sw = new StreamWriter(caminhoArquivoModeler,true)){
				sw.WriteLine ("During Game: " + recommendationsDuringGame + " Major Occurrence: " + MajorOccurrence);
				sw.Close ();	
			} 
		}
		//Se nao existir o arquivo, ele e criado.
		else{
			using(FileStream fs = File.Create(caminhoArquivoModeler)){
				using (StreamWriter sw = new StreamWriter(fs)){
					sw.WriteLine ("During Game: " + recommendationsDuringGame + " Major Occurrence: " + MajorOccurrence);
					sw.Close ();
				}
			}
		}
	}
}