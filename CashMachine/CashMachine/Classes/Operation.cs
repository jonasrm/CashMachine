using CashMachine.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine.Classes
{
    /// <summary>
    /// Cash Machine Operations
    /// </summary>
    public class Operation
    {
        private readonly List<decimal> _optionsValues = new List<decimal> { 100.00m, 50.00m, 20.00m, 10.00m };
        private const decimal _maxValuePerOperation = 5000.00m;

        /// <summary>
        /// Get the max value per operation
        /// </summary>
        /// <returns>Max value decimal</returns>
        public decimal getMaxValuePerOperation()
        {
            return _maxValuePerOperation;
        }

        /// <summary>
        /// Get avaliable options values
        /// </summary>
        /// <returns>Decimal list of options value</returns>
        public List<decimal> getOptionsValues()
        {
            return _optionsValues;
        }

        /// <summary>
        /// Cash Out
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public Dictionary<decimal, int> CashOut(decimal value)
        {
            Dictionary<decimal, int> result = new Dictionary<decimal, int>();

            try
            {
                if (value < _optionsValues.Min())
                    throw new CashOutException("Não é possível sacar o valor informado, solicite um valor igual ou maior que R$" + _optionsValues.Min().ToString());

                if (value > _maxValuePerOperation)
                    throw new CashOutException("Não é possível sacar o valor informado, limite por operação de R$" + _maxValuePerOperation.ToString());

                bool validValue = false;
                foreach (decimal item in _optionsValues.OrderBy(o => o))
                {
                    if (value % item == 0)
                    {
                        validValue = true;
                        break;
                    }
                }
                
                if (validValue == false)
                    throw new CashOutException("Não é possível sacar o valor informado devido a limitação de notas.");

                decimal remainder = value;

                while (remainder != 0)
                {
                    decimal bestMatch = _optionsValues.Where(q => q <= remainder).Max();

                    int quantity =  Convert.ToInt32(Math.Truncate(remainder / bestMatch));
                    result.Add(bestMatch, quantity);

                    remainder -= quantity * bestMatch;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}
