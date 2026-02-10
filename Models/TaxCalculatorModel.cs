namespace MyCv.Models
{
    public class TaxCalculatorModel
    {
        public decimal AnnualIncome { get; set; }
        public decimal TaxPayable;
        public decimal NetIncome => AnnualIncome - TaxPayable; 
        public decimal MonthlyTax => TaxPayable / 12;
    }
}
