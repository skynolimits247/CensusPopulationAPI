using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nagarro.CensusPopulation.SharedModel
{
    /// <summary>
    /// Class for Business Exceptions
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {

        }
    }
    /// <summary>
    /// Class for DAL Exceptions
    /// </summary>
    public class DALException : Exception
    {
        public DALException(string message) : base(message)
        {

        }
    }
    /// <summary>
    /// Class for Controller Exceptions
    /// </summary>
    public class ControllerException : Exception
    {
        public ControllerException(string message) : base(message)
        {

        }
    }
}
