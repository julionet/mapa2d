using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaGabriel
{
    public class Posicao
    {
        public int Linha;
        public int Coluna;
    }

    public class Coordenada
    {
        public Posicao PosicaoAtual;
        public Posicao PosicaoAnterior;
    }

    public class Mapa
    {
        private int contadormovimento = 0;
        public int ContadorMovimento
        {
            get { return contadormovimento; }
        }

        public List<Posicao> Execute(string[,] mapa)
        {
            var linhamaxima = mapa.GetLength(0);
            var colunamaxima = mapa.GetLength(1);

            var numeroitens = GetCountItem(mapa, "!");
            var contadoritens = 0;

            var posicaosaida = GetPosicao(mapa, "S");
            var posicaoinicio = GetPosicao(mapa, "S");
            var posicaotermino = new Posicao();

            var caminho = new List<Posicao>();

            var fim = false;
            while (!fim)
            {
                var coordenadas = new List<Coordenada>();
                coordenadas.Add(new Coordenada { PosicaoAtual = posicaoinicio });

                var coordenadasatual = new List<Posicao>();
                coordenadasatual.Add(posicaoinicio);

                var coordenadastemp = new List<Posicao>();

                var achou = false;
                while (!achou)
                {
                    for (var i = 0; i < coordenadasatual.Count; i++)
                    {
                        var linha = coordenadasatual[i].Linha;
                        var coluna = coordenadasatual[i].Coluna;

                        var x = linha;
                        var y = coluna - 1;
                        if (y >= 0 && mapa[x, y] != "#")
                        {
                            var posicaotemp = new Posicao { Linha = x, Coluna = y };

                            if (!ContainsPosicaoAtual(coordenadas, posicaotemp))
                            {
                                coordenadastemp.Add(posicaotemp);
                                coordenadas.Add(new Coordenada { PosicaoAtual = posicaotemp, PosicaoAnterior = coordenadasatual[i] });
                                if (contadoritens != numeroitens)
                                {
                                    if (mapa[x, y] == "!")
                                    {
                                        contadoritens += 1;
                                        achou = true;
                                        mapa[x, y] = ".";
                                        posicaotermino = posicaotemp;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (mapa[x, y] == "S")
                                    {
                                        achou = true;
                                        fim = true;
                                        break;
                                    }
                                }
                            }
                        }

                        x = linha;
                        y = coluna + 1;
                        if (y < colunamaxima && mapa[x, y] != "#")
                        {
                            var posicaotemp = new Posicao { Linha = x, Coluna = y };

                            if (!ContainsPosicaoAtual(coordenadas, posicaotemp))
                            {
                                coordenadastemp.Add(posicaotemp);
                                coordenadas.Add(new Coordenada { PosicaoAtual = posicaotemp, PosicaoAnterior = coordenadasatual[i] });
                                if (contadoritens != numeroitens)
                                {
                                    if (mapa[x, y] == "!")
                                    {
                                        contadoritens += 1;
                                        achou = true;
                                        mapa[x, y] = ".";
                                        posicaotermino = posicaotemp;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (mapa[x, y] == "S")
                                    {
                                        achou = true;
                                        fim = true;
                                        break;
                                    }
                                }
                            }
                        }

                        x = linha - 1;
                        y = coluna;
                        if (x >= 0 && mapa[x, y] != "#")
                        {
                            var posicaotemp = new Posicao { Linha = x, Coluna = y };

                            if (!ContainsPosicaoAtual(coordenadas, posicaotemp))
                            {
                                coordenadastemp.Add(posicaotemp);
                                coordenadas.Add(new Coordenada { PosicaoAtual = posicaotemp, PosicaoAnterior = coordenadasatual[i] });
                                if (contadoritens != numeroitens)
                                {
                                    if (mapa[x, y] == "!")
                                    {
                                        contadoritens += 1;
                                        achou = true;
                                        mapa[x, y] = ".";
                                        posicaotermino = posicaotemp;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (mapa[x, y] == "S")
                                    {
                                        achou = true;
                                        fim = true;
                                        break;
                                    }
                                }
                            }
                        }

                        x = linha + 1;
                        y = coluna;
                        if (x < linhamaxima && mapa[x, y] != "#")
                        {
                            var posicaotemp = new Posicao { Linha = x, Coluna = y };

                            if (!ContainsPosicaoAtual(coordenadas, posicaotemp))
                            {
                                coordenadastemp.Add(posicaotemp);
                                coordenadas.Add(new Coordenada { PosicaoAtual = posicaotemp, PosicaoAnterior = coordenadasatual[i] });
                                if (contadoritens != numeroitens)
                                {
                                    if (mapa[x, y] == "!")
                                    {
                                        contadoritens += 1;
                                        achou = true;
                                        mapa[x, y] = ".";
                                        posicaotermino = posicaotemp;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (mapa[x, y] == "S")
                                    {
                                        achou = true;
                                        fim = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    coordenadasatual.Clear();
                    coordenadasatual.AddRange(coordenadastemp);

                    contadormovimento++;
                    coordenadastemp.Clear();
                }

                var caminhotemp = new List<Posicao>();
                var c = coordenadas.Count() - 1;
                
                while (c >= 0)
                {
                    var anterior = coordenadas[c].PosicaoAnterior;
                    c = SearchIndex(coordenadas, anterior);
                    if (c >= 0)
                    {
                        caminhotemp.Add(coordenadas[c].PosicaoAtual);
                        if (coordenadas[c].PosicaoAtual.Linha == posicaoinicio.Linha &&
                            coordenadas[c].PosicaoAtual.Coluna == posicaoinicio.Coluna)
                            break;
                    }
                }

                caminhotemp.Reverse();
                caminho.AddRange(caminhotemp);

                posicaoinicio = posicaotermino;
                coordenadas.Clear();
            }

            caminho.Add(posicaosaida);
            return caminho;
        }

        private bool ContainsPosicaoAtual(List<Coordenada> coordenadas, Posicao posicao)
        {
            for (var i = 0; i < coordenadas.Count; i++)
            {
                if (coordenadas[i].PosicaoAtual.Linha == posicao.Linha && coordenadas[i].PosicaoAtual.Coluna == posicao.Coluna)
                    return true;
            }
            return false;
        }

        private int GetCountItem(string[,] mapa, string item)
        {
            var contador = 0;
            for (var i = 0; i < mapa.GetLength(0); i++)
            {
                for (var j = 0; j < mapa.GetLength(1); j++)
                {
                    if (mapa[i, j] == item)
                        contador += 1;
                }
            }
            return contador;
        }

        private Posicao GetPosicao(string[,] mapa, string item)
        {
            for (var i = 0; i < mapa.GetLength(0); i++)
            {
                for (var j = 0; j < mapa.GetLength(1); j++)
                {
                    if (mapa[i, j] == item)
                        return new Posicao { Linha = i, Coluna = j };
                }
            }
            return null;
        }

        private int SearchIndex(List<Coordenada> coordenadas, Posicao posicao)
        {
            for (var i = 0; i < coordenadas.Count; i++)
            {
                if (coordenadas[i].PosicaoAtual.Linha == posicao.Linha && coordenadas[i].PosicaoAtual.Coluna == posicao.Coluna)
                    return i;
            }
            return -1;
        }
    }
}
