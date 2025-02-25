namespace Pedido10.Domain.Dto
{
    public class ParcelaDto
    {
        public int Numero_Parcela { get; set; }
        public DateOnly DT_Vencimento { get; set; }
        public int ID_Forma_PGTO { get; set; }
        public decimal Valor_Parcela { get; set; }
        public char Status_Parcela { get; set; }
        public decimal? Valor_Pago_Parcela { get; set; }
        public DateOnly? Data_Pagamento { get; set; }
    }
}