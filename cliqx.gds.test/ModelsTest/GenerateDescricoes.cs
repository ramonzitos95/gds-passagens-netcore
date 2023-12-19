using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus.Models;

namespace cliqx.gds.test.ModelsTest
{
    public class GenerateDescricoes
    {
        public static List<Descrico> GenerateListDescricoes()
        {
            return new List<Descrico>()
            {
                new Descrico()
                {
                    Chave = "RJ_TRANSACAO", Valor = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxODY5Ny4xNzY3Mi4yMDIyLTA2LTEwLjMyMTMuMTAwMDAwMDA0MTY2ODIuMzYuLTEuZmFsc2UuMTAzQDg0IiwiZXhwIjoxNjg1ODMzNjczLCJ1c2VySWQiOiJ3aGF0c2FwcCIsInJvbGUiOiItIn0.OUuEhZfgkF-2_7jRmFXj0HMhsMosguW37pBnDLW2hcg",
                    Exibir = "S", Posicao = 0, Titulo = ""
                },
                new Descrico()
                {
                    Chave = "RJ_LOCALIZADOR", Valor = "010000464050", Exibir = "S", Posicao = 0, Titulo = ""
                },
                new Descrico()
                {
                    Chave = "POLTRONA", Valor = "36", Exibir = "S", Posicao = 0, Titulo = "Poltrona"
                },
                new Descrico()
                {
                    Chave = "ORIGEM_ID", Valor = "18697", Exibir = "S", Posicao = 0, Titulo = string.Empty
                },
                new Descrico()
                {
                    Chave = "ORIGEM_CIDADE", Valor = "SAO PAULO - SP", Exibir = "S", Posicao = 0, Titulo = string.Empty
                },
                new Descrico()
                {
                    Chave = "ORIGEM_DATA_SAIDA", Valor = "2022-06-10 20:40", Exibir = "S", Posicao = 0, Titulo = string.Empty
                },
                new Descrico()
                {
                    Chave = "DESTINO_ID", Valor = "17672", Exibir = "S", Posicao = 0, Titulo = string.Empty
                },
                new Descrico()
                {
                    Chave = "DESTINO_CIDADE", Valor = "BLUMENAU - SC", Exibir = "S", Posicao = 0, Titulo = string.Empty
                },
                new Descrico()
                {
                    Chave = "DESTINO_DATA_CHEGADA", Valor = "2022-06-10 21:20", Exibir = "S", Posicao = 0, Titulo = string.Empty
                },
                new Descrico()
                {
                    Chave = "PASSAGEIRO_NOME", Valor = "Ramon Silva", Exibir = "S", Posicao = 0, Titulo = string.Empty
                },
                new Descrico()
                {
                    Chave = "PASSAGEIRO_DOCUMENTO", Valor = "08390890909", Exibir = "S", Posicao = 0, Titulo = string.Empty
                }
            };
        } 
    }
}
