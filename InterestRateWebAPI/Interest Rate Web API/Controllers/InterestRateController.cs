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

        [HttpGet]
        public List<float> GetInterestRate(string LoanType, double LoanAmount, int Delay)
        {
            float InterestRate;
            double MonthlyPayment;
            float RepaymentAmount = 0f;
            float TotalInterest = 0f;


            if (LoanType == "sme loan") // esnaf kredisi
            {
                InterestRate = 0.0123f;
            }
            else if (LoanType == "housing loan")
            {
                InterestRate = 0.0099f;
            }

            else if (LoanType == "vehicle loan")
            {
                InterestRate = 0.0165f;
            }

            else if (LoanType == "student loan")
            {
                InterestRate = 0.0135f;
            }

            else
            {
                InterestRate = 0.02f;
            }


            MonthlyPayment = LoanAmount * Math.Pow((1 + InterestRate), Delay) * InterestRate / (Math.Pow((1 + InterestRate), Delay) - 1);
            RepaymentAmount = (float)MonthlyPayment * Delay;
            TotalInterest = (float)RepaymentAmount - (float)LoanAmount;


            List<float> Result = new List<float>()
            {

                TotalInterest, RepaymentAmount

            };

            return Result;

        }
    }
}
