namespace Interest_Rate_Web_API
{
    public class MonthlyInfo
    {
        public int Month { get; set; }

        public float MonthlyPayment { get; set; }

        public float PaidInterest { get; set; }

        public float PaidCapital { get; set; }

        public float RemainingDebt { get; set; }
    }
}
