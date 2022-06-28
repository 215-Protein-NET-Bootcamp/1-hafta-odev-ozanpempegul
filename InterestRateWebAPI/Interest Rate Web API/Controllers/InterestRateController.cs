using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace Interest_Rate_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class InterestRateController : Controller
    {

        [HttpGet("TotalRepayment")]
        public List<double> GetTotalRepayment(string LoanType, int LoanAmount, int Delay)
        {
            float InterestRate;
            float MonthlyPayment;
            float RepaymentAmount;
            float TotalInterest;


            if (LoanType == "sme") // esnaf kredisi
            {
                InterestRate = 0.0123f;
            }
            else if (LoanType == "housing")
            {
                InterestRate = 0.0099f;
            }

            else if (LoanType == "vehicle")
            {
                InterestRate = 0.0165f;
            }

            else if (LoanType == "student")
            {
                InterestRate = 0.0135f;
            }

            else
            {
                throw new Exception("Loan Types: sme, housing, vehicle, student");
            }

            if (LoanAmount <= 0 || LoanAmount > 1000000)
            {
                throw new Exception("Invalid Loan Amount");
            }

            if (Delay < 0 || Delay >= 240)
            {
                throw new Exception("Invalid Delay");
            }


            MonthlyPayment = (float)(LoanAmount * Math.Pow((1 + InterestRate), Delay) * InterestRate / (Math.Pow((1 + InterestRate), Delay) - 1));
            RepaymentAmount = (float)MonthlyPayment * Delay;
            TotalInterest = (float)RepaymentAmount - (float)LoanAmount;


            List<double> Result = new List<double>()
            {

                Math.Round(TotalInterest, 2), Math.Round(RepaymentAmount, 2)

            };

            return Result;

        }

        [HttpGet("PaymentPlan")]

        public List<MonthlyInfo> GetPaymentPlan(string LoanType, int LoanAmount, int Delay)

        {
            float InterestRate;
            double MonthlyPayment;        


            if (LoanType == "sme") // esnaf kredisi
            {
                InterestRate = 0.0123f;
            }
            else if (LoanType == "housing")
            {
                InterestRate = 0.0099f;
            }

            else if (LoanType == "vehicle")
            {
                InterestRate = 0.0165f;
            }

            else if (LoanType == "student")
            {
                InterestRate = 0.01f;
            }

            else
            {
                throw new Exception("Loan Types: sme, housing, vehicle, student");
            }

            if (LoanAmount <= 0 || LoanAmount > 1000000)
            {
                throw new Exception("Invalid Loan Amount");
            }

            if (Delay < 0 || Delay > 240)
            {
                throw new Exception("Invalid Delay");
            }

            MonthlyPayment = LoanAmount * Math.Pow((1 + InterestRate), Delay) * InterestRate / (Math.Pow((1 + InterestRate), Delay) - 1);


            List<MonthlyInfo> Result = new List<MonthlyInfo>();

            for (int i = 1; i <= Delay; i++)
            {
                float MonthlyInterest = (float)LoanAmount * (float)InterestRate;
                float PaidCapital = (float)MonthlyPayment - MonthlyInterest;                
                Result.Add(new MonthlyInfo
                {
                    Month = i,
                    MonthlyPayment = (int)Math.Round(MonthlyPayment),
                    PaidInterest = (int)Math.Round(MonthlyInterest),
                    PaidCapital = (int)Math.Round(MonthlyPayment - MonthlyInterest),
                    RemainingDebt = (int)Math.Round(LoanAmount - PaidCapital)
                });
                LoanAmount -= (int)PaidCapital;
            }

            return Result;

        }
    }
}
