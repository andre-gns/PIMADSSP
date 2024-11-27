namespace PIMFazendaUrbanaAPI.DTOs
{
    public class ProducaoDTO
    {
        public int Id { get; set; }
        public CultivoDTO Cultivo { get; set; } // Atualizado com composição

        //public int IdCultivo { get; set; }
        //public string Nome { get; set; }
        //public string Variedade { get; set; }
        //public string Categoria { get; set; }
        //public int TempoProdTradicional { get; set; }
        //public int TempoProdControlado { get; set; }
        public int Qtd { get; set; }
        public string Unidqtd { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataColheita { get; set; }
        public bool AmbienteControlado { get; set; }
        public bool StatusFinalizado { get; set; }

        // Métodos
        public string CalcularDataColheita()
        {
            string dataColheita = "";

            DateTime data = DateTime.Now;
            int tempoProd;
            if (AmbienteControlado)
            {
                tempoProd = (int)Cultivo.TempoProdControlado;
            }
            else
            {
                tempoProd = (int)Cultivo.TempoProdTradicional;
            }

            dataColheita = data.AddDays(tempoProd).ToShortDateString();

            return dataColheita;
        }

        public DateTime CalcularDataHoraColheita()
        {
            DateTime dataHoraColheita;

            DateTime data = DateTime.Now;
            int tempoProd;
            if (AmbienteControlado)
            {
                tempoProd = (int)Cultivo.TempoProdControlado;
            }
            else
            {
                tempoProd = (int)Cultivo.TempoProdTradicional;
            }

            dataHoraColheita = data.AddDays(tempoProd);

            return dataHoraColheita;
        }

    }
}
