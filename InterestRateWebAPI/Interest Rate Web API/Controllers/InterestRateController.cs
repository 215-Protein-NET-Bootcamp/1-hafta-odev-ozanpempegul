using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Interest_Rate_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class InterestRateController : Controller
    {

        [HttpGet("TotalRepayment")]
        public List<float> GetTotalRepayment(string LoanType, float LoanAmount, int Delay)
        {
            float InterestRate;
            float MonthlyPayment;
            float RepaymentAmount = 0f;
            float TotalInterest = 0f;


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
                InterestRate = 0.02f;
            }


            MonthlyPayment = (float)(LoanAmount * Math.Pow((1 + InterestRate), Delay) * InterestRate / (Math.Pow((1 + InterestRate), Delay) - 1));
            RepaymentAmount = (float)MonthlyPayment * Delay;
            TotalInterest = (float)RepaymentAmount - (float)LoanAmount;


            List<float> Result = new List<float>()
            {

                TotalInterest, RepaymentAmount

            };

            return Result;

        }

        [HttpGet("PaymentPlan")]
        public List<MonthlyInfo> GetPaymentPlan(string LoanType, float LoanAmount, int Delay)
        {
            float InterestRate;
            float MonthlyPayment;        


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
                InterestRate = 0.02f;
            }

            MonthlyPayment = (float)(LoanAmount * Math.Pow((1 + InterestRate), Delay) * InterestRate / (Math.Pow((1 + InterestRate), Delay) - 1));

            List<MonthlyInfo> Result = new List<MonthlyInfo>();

            for (int i = 1; i <= Delay; i++)
            {
                float MonthlyInterest = (float)LoanAmount * InterestRate;
                float PaidCapital = (float)MonthlyPayment - MonthlyInterest;                
                Result.Add(new MonthlyInfo
                {
                    Month = i,
                    MonthlyPayment = (float)MonthlyPayment,
                    PaidInterest = MonthlyInterest,
                    PaidCapital = (float)MonthlyPayment - MonthlyInterest,
                    RemainingDebt = (float)LoanAmount - (float)PaidCapital
                });
                LoanAmount -= PaidCapital;
            }

            return Result;

        }
    }
}
