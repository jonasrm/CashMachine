using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CashMachine.Classes;
using CashMachine.CustomException;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        /// <summary>
        /// Auxiliary function for the tests
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private decimal SumValues(Dictionary<decimal, int> values)
        {
            decimal result = 0;

            try
            {
                foreach (var item in values)
                    result += item.Key * item.Value;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        [TestMethod]
        public void TestInvalidInputs()
        {
            Operation operation = new Operation();
            decimal invalidInputMin = operation.getOptionsValues().Min() - 1;
            decimal invalidInputMax = operation.getMaxValuePerOperation() + 1;

            try
            {
                operation.CashOut(invalidInputMin);
                Assert.Fail();
            }
            catch (CashOutException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            try
            {
                operation.CashOut(invalidInputMax);
                Assert.Fail();
            }
            catch (CashOutException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            try
            {
                operation.CashOut(0);
                Assert.Fail();
            }
            catch (CashOutException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            try
            {
                operation.CashOut(-10);
                Assert.Fail();
            }
            catch (CashOutException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestValidInputs()
        {
            try
            {
                Operation operation = new Operation();
                Dictionary<decimal, int> result = new Dictionary<decimal, int>();

                result.Clear();
                result.Add(100.00m, 1);
                result.Add(50.00m, 1);
                result.Add(20.00m, 1);
                result.Add(10.00m, 1);
                CollectionAssert.AreEqual(operation.CashOut(180.00m), result);

                result.Clear();
                result.Add(100.00m, 1);
                CollectionAssert.AreEqual(operation.CashOut(100.00m), result);

                result.Clear();
                result.Add(100.00m, 1);
                result.Add(20.00m, 2);
                CollectionAssert.AreEqual(operation.CashOut(140.00m), result);

                result.Clear();
                result.Add(100.00m, 50);
                CollectionAssert.AreEqual(operation.CashOut(5000.00m), result);

                result.Clear();
                result.Add(100.00m, 3);
                result.Add(50.00m, 1);
                result.Add(20.00m, 1);
                result.Add(10.00m, 1);
                CollectionAssert.AreEqual(operation.CashOut(380.00m), result);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestStress()
        {
            try
            {
                Operation operation = new Operation();
                decimal minValue = operation.getOptionsValues().Min();
                decimal maxValue = operation.getMaxValuePerOperation();
                Dictionary<decimal, int> result = new Dictionary<decimal, int>();

                for (decimal inputValue = minValue; inputValue < maxValue; inputValue += minValue)
                {
                    result = operation.CashOut(inputValue);
                    decimal total = SumValues(result);
                    Assert.AreEqual(total, inputValue);
                }
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }    
    }
}
